using Microsoft.EntityFrameworkCore;
using MyIhsan.Common.Core;
using MyIhsan.Common.OptionModel;
using MyIhsan.Common.Parameters;
using MyIhsan.Identity.Entities.Views;
using MyIhsan.Identity.Service.Core;
using MyIhsan.Identity.Service.Dtos;
using MyIhsan.Identity.Service.Interfaces;
using MyIhsan.Identity.Service.UnitOfWork;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyIhsan.Common.Extensions;
using AutoMapper;
using System.Linq.Expressions;
using LinqKit;

namespace MyIhsan.Identity.Service.Services
{
    public class UserServices : BaseService<AspNetUsers, UserDto>,IUserServices
    {
        protected internal UserServices(IServiceBaseParameter<AspNetUsers> businessBaseParameter) : base(businessBaseParameter)
        {

        }

        public async Task<IDataPagging> GetUsers(GetAllUserParameters parameters)
        {
            int limit = parameters.PageSize;
            int offset = (parameters.PageNumber * parameters.PageSize);
            var users = await _unitOfWork.Repository.FindPaggedAsync(predicate: PredicateBuilderFunction(parameters), skip: offset, take: limit, parameters.OrderByValue);

            var usesrPagging = users.Item2;

            if (!usesrPagging.Any())
            {
                var res = ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                return new DataPagging(0, 0, 0, res);
            }

            var usersDto = Mapper.Map<IEnumerable<UserDto>>(usesrPagging);
            
            var repoResult = ResponseResult.GetRepositoryActionResult(usersDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            return new DataPagging(parameters.PageNumber, parameters.PageSize, users.Item1, repoResult);
        }
        public async override Task<IResponseResult> GetByIdAsync(object id)
        {
            var user = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Id ==(long) id);
            if (user == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var userDto = Mapper.Map<UserDto>(user);
            return ResponseResult.GetRepositoryActionResult(userDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async override Task<IResponseResult> AddAsync(UserDto model)
        {
            if (model == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var isExist = await _unitOfWork.Repository.IsExists(q => (q.UserName == model.UserName || q.Email == model.Email || (q.PhoneNumber == model.PhoneNumber && (model.PhoneNumber != "" && model.PhoneNumber != null))) && q.IsDeleted != 1);
            if (isExist) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotAcceptable, message: HttpStatusCode.NotAcceptable.ToString());
            var data = await _unitOfWork.Repository.FindAsync(q => q != null);
            model.Id = data == null ? 1000 : (1000 + data.Count());
            model.SecurityStamp = Guid.NewGuid().ToString();
            var user = Mapper.Map<AspNetUsers>(model);
            _unitOfWork.Repository.Add(user);
            await _unitOfWork.SaveChanges();
            return ResponseResult.GetRepositoryActionResult(model, status: HttpStatusCode.Created, message: HttpStatusCode.Created.ToString());
        }

        public async override Task<IResponseResult> UpdateAsync(UserDto model)
        {
            if (model == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var isExist = await _unitOfWork.Repository.FirstOrDefaultAsync(q => (q.UserName == model.UserName || q.Email == model.Email || (q.PhoneNumber == model.PhoneNumber && (model.PhoneNumber != "" && model.PhoneNumber != null))) && q.Id != model.Id && q.IsDeleted != 1) != null;
            if (isExist) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotAcceptable, message: HttpStatusCode.NotAcceptable.ToString());
            var original = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == model.Id);
            model.SecurityStamp = Guid.NewGuid().ToString();
            var user = Mapper.Map(model, original);
            _unitOfWork.Repository.Update(user, user.Id);
            await _unitOfWork.SaveChanges();
            return ResponseResult.GetRepositoryActionResult(model, status: HttpStatusCode.Accepted, message: HttpStatusCode.Accepted.ToString());
        }
        public async override Task<IResponseResult> DeleteAsync(object id)
        {
            if (id == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var user = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Id ==(long) id);
            user.IsDeleted = 1;
            _unitOfWork.Repository.Update(user, user.Id);
            await _unitOfWork.SaveChanges();
            return ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        

        public async Task<IResponseResult> IsUsernameExists(string name, long id)
        {
            var res = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.UserName == name && q.Id != id && q.IsDeleted!=1);
            return ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<IResponseResult> IsEmailExists(string email, long id)
        {
            var res = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Email == email && q.Id != id && q.IsDeleted!=1);
            return ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<IResponseResult> IsPhoneExists(string phone, long id)
        {
            var res = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.PhoneNumber == phone && q.Id != id && q.IsDeleted!=1);
            return ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber)
        {
            var users = !string.IsNullOrEmpty(searchTerm) ? await _unitOfWork.Repository.FindAsync(n => n.IsDeleted!=1 && n.UserName.ToLower().Contains(searchTerm.ToLower())) : await _unitOfWork.Repository.FindAsync(q => q.IsDeleted!=1);
            var result = users.OrderBy(q => q.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(q => new Select2OptionModel { id = q.Id.ToString(), text = q.UserName }).ToList();
            var select2pagedResult = new Select2PagedResult();
            select2pagedResult.Total = users.Count();
            select2pagedResult.Results = result;
            return select2pagedResult;
        }
        public async Task<IEnumerable<Select2OptionModel>> GetUserAssignedSelect2(long id)
        {
            //var role = await _role_unitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == id, include: source => source.Include(a => a.AspNetUsersRole).Include(b => b.AspNetUsersRole), disableTracking: false);
            //var userIdList = role.AspNetUsersRole.Select(q => q.UserId).ToList();
            //var userassignQuery = await __unitOfWork.Repository.FindAsync(q => userIdList.Contains(q.Id) && !q.IsDeleted);
            //var userassign = userassignQuery.Select(q => new Select2OptionModel { id = q.Id, text = q.UserName }).ToList();
            //return userassign;
            throw new Exception();
        }
        public async Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters)
        {
            //var role = await _role_unitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == parameters.RoleId, include: source => source.Include(a => a.AspNetUsersRole), disableTracking: false);
            //if (parameters.AssignedUser != null)
            //{
            //    foreach (var item in parameters.AssignedUser)
            //    {
            //        var isExist = await _usersRole_unitOfWork.Repository.FirstOrDefaultAsync(q => q.UserId == item && q.RoleId == parameters.RoleId) != null;
            //        if (!isExist)
            //        {
            //            var userRole = new AspNetUsersRole() { Id = Guid.NewGuid().ToString(), UserId = item, RoleId = parameters.RoleId };
            //            _usersRole_unitOfWork.Repository.Add(userRole);
            //        }
            //    }
            //}

            //var userRemove = parameters.AssignedUser is null ? role.AspNetUsersRole : role.AspNetUsersRole.Where(q => !parameters.AssignedUser.Contains(q.UserId));
            //_usersRole_unitOfWork.Repository.RemoveRange(userRemove);
            //await _usersRole_unitOfWork.SaveChanges();
            //return ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.Created, message: HttpStatusCode.Created.ToString());
            throw new Exception();
        }
        static Expression<Func<AspNetUsers, bool>> PredicateBuilderFunction(GetAllUserParameters filter)
        {
            var predicate = PredicateBuilder.New<AspNetUsers>(true);
            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                predicate = predicate.And(b => b.UserName.ToLower().StartsWith(filter.UserName));
            }
            return predicate;
        }


    }
}

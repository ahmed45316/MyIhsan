using Microsoft.EntityFrameworkCore;
using MyIhsan.Common.Core;
using MyIhsan.Common.OptionModel;
using MyIhsan.Common.Parameters;
using MyIhsan.Entities.Views;
using MyIhsan.Service.Core;
using MyIhsan.Service.Dtos;
using MyIhsan.Service.Interfaces;
using MyIhsan.Service.UnitOfWork;
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
using MyIhsan.Entities.Entities;

namespace MyIhsan.Service.Services
{
    public class UserServices : BaseService<AspNetUsers, UserDto>,IUserServices
    {
        private readonly IIdentityUnitOfWork<AspNetRoles> _roleUnitOfWork;
        private readonly IIdentityUnitOfWork<AspNetUsersRoles> _userRolesUnitOfWork;
        public UserServices(IServiceBaseParameter<AspNetUsers> businessBaseParameter, IIdentityUnitOfWork<AspNetRoles> roleUnitOfWork, IIdentityUnitOfWork<AspNetUsersRoles> userRolesUnitOfWork) : base(businessBaseParameter)
        {
            _roleUnitOfWork = roleUnitOfWork;
            _userRolesUnitOfWork = userRolesUnitOfWork;
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
            var ids = usersDto.Select(q => Convert.ToString(q?.Id)).ToList();
            var userRoles = await _userRolesUnitOfWork.Repository.FindAsync(q => ids.Contains(q.UserId));
            var roleIds = userRoles.Select(q => q.RoleId).ToList();
            var roles = await _roleUnitOfWork.Repository.FindAsync(q => roleIds.Contains(q.Id));
            foreach (var user in usersDto)
            {
                var userRoleIds = userRoles.Where(q => q.UserId == user.Id.ToString()).Select(q=>q.RoleId).ToList();
                var userRolesData = roles.Where(q => userRoleIds.Contains(q.Id)).ToList();
                var rolesString = userRolesData.Select(q => q.Name).ToList();
                user.Roles = (!rolesString.Any()) ? null : String.Join(",", rolesString);
            }
            var repoResult = ResponseResult.GetRepositoryActionResult(usersDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            return new DataPagging(parameters.PageNumber, parameters.PageSize, users.Item1, repoResult);
        }
        public async override Task<IResponseResult> GetByIdAsync(object id)
        {
            var user = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Id ==(long) id);
            if (user == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var userDto = Mapper.Map<UserDto>(user);
            userDto.CountryName = "Egypt";
            userDto.CityName = "Cairo";
            userDto.GenderName = userDto.Gender == true ? "Male" : "Female";
            return ResponseResult.GetRepositoryActionResult(userDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async override Task<IResponseResult> AddAsync(UserDto model)
        {
            if (model == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var isExist = await _unitOfWork.Repository.IsExists(q => (q.UserName == model.UserName || q.Email == model.Email || (q.PhoneNumber == model.PhoneNumber && (model.PhoneNumber != "" && model.PhoneNumber != null))) && q.IsDeleted != true);
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
            var isExist = await _unitOfWork.Repository.FirstOrDefaultAsync(q => (q.UserName == model.UserName || q.Email == model.Email || (q.PhoneNumber == model.PhoneNumber && (model.PhoneNumber != "" && model.PhoneNumber != null))) && q.Id != model.Id && q.IsDeleted != true) != null;
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
            user.IsDeleted = true;
            _unitOfWork.Repository.Update(user, user.Id);
            await _unitOfWork.SaveChanges();
            return ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        

        public async Task<IResponseResult> IsUsernameExists(string name, long id)
        {
            var res = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.UserName.ToLower() == name.ToLower() && q.Id != id && q.IsDeleted!=true);
            return ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<IResponseResult> IsEmailExists(string email, long id)
        {
            var res = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Email.ToLower() == email.ToLower() && q.Id != id && q.IsDeleted!=true);
            return ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<IResponseResult> IsPhoneExists(string phone, long id)
        {
            var res = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.PhoneNumber == phone && q.Id != id && q.IsDeleted!=true);
            return ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber)
        {
            var users = !string.IsNullOrEmpty(searchTerm) ? await _unitOfWork.Repository.FindAsync(n => n.IsDeleted!=true && n.UserName.ToLower().Contains(searchTerm.ToLower())) : await _unitOfWork.Repository.FindAsync(q => q.IsDeleted!=true);
            var result = users.OrderBy(q => q.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(q => new Select2OptionModel { id = q.Id.ToString(), text = q.UserName }).ToList();
            var select2pagedResult = new Select2PagedResult();
            select2pagedResult.Total = users.Count();
            select2pagedResult.Results = result;
            return select2pagedResult;
        }
        public async Task<IEnumerable<Select2OptionModel>> GetUserAssignedSelect2(string id)
        {
            var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == id, include: source => source.Include(a => a.AspNetUsersRoles), disableTracking: false);
            var userIdList = role.AspNetUsersRoles.Select(q =>long.Parse(q.UserId)).ToList();
            var userassignQuery = await _unitOfWork.Repository.FindAsync(q => userIdList.Contains(q.Id??0) && q.IsDeleted !=true);
            var userassign = userassignQuery.Select(q => new Select2OptionModel { id = q.Id.ToString(), text = q.UserName }).ToList();
            return userassign;
        }
        public async Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters)
        {
            var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == parameters.RoleId, include: source => source.Include(a => a.AspNetUsersRoles), disableTracking: false);
            if (parameters.AssignedUser != null)
            {
                foreach (var user in parameters.AssignedUser)
                {
                    var isExist = role.AspNetUsersRoles.Any(q=>q.UserId == user);
                    if (!isExist)
                    {
                        var userRole = new AspNetUsersRoles() { Id = Guid.NewGuid().ToString(), UserId = user, RoleId = parameters.RoleId };
                        role.AspNetUsersRoles.Add(userRole);
                    }
                }
            }
            if (role.AspNetUsersRoles.Any()) await _roleUnitOfWork.SaveChanges();

            var userRemove = parameters.AssignedUser is null ? role.AspNetUsersRoles : role.AspNetUsersRoles.Where(q => !parameters.AssignedUser.Contains(q.UserId));
            if (userRemove.Any())
            {
                _userRolesUnitOfWork.Repository.RemoveRange(userRemove);
                await _userRolesUnitOfWork.SaveChanges();
            }

            return ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.Created, message: HttpStatusCode.Created.ToString());
        }
        static Expression<Func<AspNetUsers, bool>> PredicateBuilderFunction(GetAllUserParameters filter)
        {
            var predicate = PredicateBuilder.New<AspNetUsers>(true);
            predicate = predicate.And(b => b.IsDeleted!=true);
            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                predicate = predicate.And(b => b.UserName.ToLower().StartsWith(filter.UserName));
            }
            return predicate;
        }


    }
}

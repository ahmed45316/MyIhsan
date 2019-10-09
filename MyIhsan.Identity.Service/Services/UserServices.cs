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

namespace MyIhsan.Identity.Service.Services
{
    public class UserServices : IUserServices
    {
        private readonly IServiceBaseParameter<AspNetUsers> _serviceBaseParameter;
        public UserServices(IServiceBaseParameter<AspNetUsers> serviceBaseParameter)
        {
            _serviceBaseParameter = serviceBaseParameter;
        }

        public async Task<IDataPagging> GetUsers(GetAllUserParameters parameters)
        {
            var users = await _serviceBaseParameter.UnitOfWork.Repository.FindAsync(q =>q!=null && q.IsDeleted !=1);
            users = !string.IsNullOrWhiteSpace(parameters.UserName) ? users.Where(q => q.UserName.ToLower().Contains(parameters.UserName.ToLower())) : users;
           
            var usesrPagging = users.AsQueryable().OrderBy(parameters.OrderByValue).Skip(parameters.PageNumber).Take(parameters.PageSize).ToList();

            if (!usesrPagging.Any())
            {
                var res = _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                return new DataPagging(0, 0, 0, res);
            }

            var usersDto = _serviceBaseParameter.Mapper.Map<IEnumerable<UserDto>>(usesrPagging);
            
            var repoResult = _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(usersDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            return new DataPagging(parameters.PageNumber, parameters.PageSize, users.Count(), repoResult);
        }
        public async Task<IResponseResult> GetUser(long Id)
        {
            var user = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == Id);
            if (user == null) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var userDto = Mapper.Map<UserDto>(user);
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(userDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async Task<IResponseResult> AddUser(UserDto userDto)
        {
            if (userDto == null) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var isExist = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => (q.UserName == userDto.UserName || q.Email == userDto.Email || (q.PhoneNumber == userDto.PhoneNumber && (userDto.PhoneNumber != "" && userDto.PhoneNumber != null))) && q.IsDeleted!=1) != null;
            if (isExist) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotAcceptable, message: HttpStatusCode.NotAcceptable.ToString());
            var data = await _serviceBaseParameter.UnitOfWork.Repository.FindAsync(q=>q!=null);
            userDto.Id = data ==null?1000:(1000+data.Count());
            userDto.SecurityStamp = Guid.NewGuid().ToString();        
            var user = Mapper.Map<AspNetUsers>(userDto);
             _serviceBaseParameter.UnitOfWork.Repository.Add(user);
            await _serviceBaseParameter.UnitOfWork.SaveChanges();
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(userDto, status: HttpStatusCode.Created, message: HttpStatusCode.Created.ToString());
        }

        public async Task<IResponseResult> UpdateUser(UserDto userDto)
        {
            if (userDto == null) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var isExist = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => (q.UserName == userDto.UserName || q.Email == userDto.Email || (q.PhoneNumber == userDto.PhoneNumber && (userDto.PhoneNumber != "" && userDto.PhoneNumber != null))) && q.Id != userDto.Id && q.IsDeleted!=1) != null;
            if (isExist) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotAcceptable, message: HttpStatusCode.NotAcceptable.ToString());
            var original = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == userDto.Id);
            userDto.SecurityStamp = Guid.NewGuid().ToString();         
            var user = Mapper.Map(userDto,original);
            _serviceBaseParameter.UnitOfWork.Repository.Update(user,user.Id);
            await _serviceBaseParameter.UnitOfWork.SaveChanges();
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(userDto, status: HttpStatusCode.Accepted, message: HttpStatusCode.Accepted.ToString());
        }

        public async Task<IResponseResult> RemoveUserById(long id)
        {
            if (id == null) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var user = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == id);
            user.IsDeleted = 1;
            _serviceBaseParameter.UnitOfWork.Repository.Update(user, user.Id);
            await _serviceBaseParameter.UnitOfWork.SaveChanges();
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<IResponseResult> IsUsernameExists(string name, long id)
        {
            var res = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.UserName == name && q.Id != id && q.IsDeleted!=1);
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<IResponseResult> IsEmailExists(string email, long id)
        {
            var res = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.Email == email && q.Id != id && q.IsDeleted!=1);
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<IResponseResult> IsPhoneExists(string phone, long id)
        {
            var res = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.PhoneNumber == phone && q.Id != id && q.IsDeleted!=1);
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public async Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber)
        {
            var users = !string.IsNullOrEmpty(searchTerm) ? await _serviceBaseParameter.UnitOfWork.Repository.FindAsync(n => n.IsDeleted!=1 && n.UserName.ToLower().Contains(searchTerm.ToLower())) : await _serviceBaseParameter.UnitOfWork.Repository.FindAsync(q => q.IsDeleted!=1);
            var result = users.OrderBy(q => q.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(q => new Select2OptionModel { id = q.Id.ToString(), text = q.UserName }).ToList();
            var select2pagedResult = new Select2PagedResult();
            select2pagedResult.Total = users.Count();
            select2pagedResult.Results = result;
            return select2pagedResult;
        }
        public async Task<IEnumerable<Select2OptionModel>> GetUserAssignedSelect2(long id)
        {
            //var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == id, include: source => source.Include(a => a.AspNetUsersRole).Include(b => b.AspNetUsersRole), disableTracking: false);
            //var userIdList = role.AspNetUsersRole.Select(q => q.UserId).ToList();
            //var userassignQuery = await _unitOfWork.Repository.FindAsync(q => userIdList.Contains(q.Id) && !q.IsDeleted);
            //var userassign = userassignQuery.Select(q => new Select2OptionModel { id = q.Id, text = q.UserName }).ToList();
            //return userassign;
            throw new Exception();
        }
        public async Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters)
        {
            //var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == parameters.RoleId, include: source => source.Include(a => a.AspNetUsersRole), disableTracking: false);
            //if (parameters.AssignedUser != null)
            //{
            //    foreach (var item in parameters.AssignedUser)
            //    {
            //        var isExist = await _usersRoleUnitOfWork.Repository.FirstOrDefaultAsync(q => q.UserId == item && q.RoleId == parameters.RoleId) != null;
            //        if (!isExist)
            //        {
            //            var userRole = new AspNetUsersRole() { Id = Guid.NewGuid().ToString(), UserId = item, RoleId = parameters.RoleId };
            //            _usersRoleUnitOfWork.Repository.Add(userRole);
            //        }
            //    }
            //}

            //var userRemove = parameters.AssignedUser is null ? role.AspNetUsersRole : role.AspNetUsersRole.Where(q => !parameters.AssignedUser.Contains(q.UserId));
            //_usersRoleUnitOfWork.Repository.RemoveRange(userRemove);
            //await _usersRoleUnitOfWork.SaveChanges();
            //return ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.Created, message: HttpStatusCode.Created.ToString());
            throw new Exception();
        }



    }
}

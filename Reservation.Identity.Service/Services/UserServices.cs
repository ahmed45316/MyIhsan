using Microsoft.EntityFrameworkCore;
using MyIhsan.Common.Core;
using MyIhsan.Common.OptionModel;
using MyIhsan.Common.Parameters;
using MyIhsan.Identity.Entities.Views;
using MyIhsan.Identity.Service.Core;
using MyIhsan.Identity.Service.Dtos;
using MyIhsan.Identity.Service.Extensions;
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
            var users = await _serviceBaseParameter.UnitOfWork.Repository.FindAsync(q =>q!=null && q.ISDELETED !=1);
            users = !string.IsNullOrEmpty(parameters.UserName) ? users.Where(q => q.USERNAME.Contains(parameters.UserName)) : users;
           
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
            var user = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.ID == Id);
            if (user == null) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var userDto = Mapper.Map<UserDto>(user);
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(userDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async Task<IResponseResult> AddUser(UserDto userDto)
        {
            if (userDto == null) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var isExist = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => (q.USERNAME == userDto.UserName || q.EMAIL == userDto.Email || (q.PHONENUMBER == userDto.PhoneNumber && (userDto.PhoneNumber != "" && userDto.PhoneNumber != null))) && q.ISDELETED!=1) != null;
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
            var isExist = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => (q.USERNAME == userDto.UserName || q.EMAIL == userDto.Email || (q.PHONENUMBER == userDto.PhoneNumber && (userDto.PhoneNumber != "" && userDto.PhoneNumber != null))) && q.ID != userDto.Id && q.ISDELETED!=1) != null;
            if (isExist) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotAcceptable, message: HttpStatusCode.NotAcceptable.ToString());
            var original = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.ID == userDto.Id);
            userDto.SecurityStamp = Guid.NewGuid().ToString();         
            var user = Mapper.Map(userDto,original);
            _serviceBaseParameter.UnitOfWork.Repository.Update(user,user.ID);
            await _serviceBaseParameter.UnitOfWork.SaveChanges();
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(userDto, status: HttpStatusCode.Accepted, message: HttpStatusCode.Accepted.ToString());
        }

        public async Task<IResponseResult> RemoveUserById(long id)
        {
            if (id == null) return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
            var user = await _serviceBaseParameter.UnitOfWork.Repository.FirstOrDefaultAsync(q => q.ID == id);
            user.ISDELETED = 1;
            _serviceBaseParameter.UnitOfWork.Repository.Update(user, user.ID);
            await _serviceBaseParameter.UnitOfWork.SaveChanges();
            return _serviceBaseParameter.ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }

        public Task<IResponseResult> IsUsernameExists(string name, long id)
        {
            throw new NotImplementedException();
        }

        public Task<IResponseResult> IsEmailExists(string email, long id)
        {
            throw new NotImplementedException();
        }

        public Task<IResponseResult> IsPhoneExists(string phone, long id)
        {
            throw new NotImplementedException();
        }

        public Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Select2OptionModel>> GetUserAssignedSelect2(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters)
        {
            throw new NotImplementedException();
        }

        

    }
}

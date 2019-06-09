using Reservation.Common.Core;
using Reservation.Common.IdentityInterfaces;
using Reservation.Common.OptionModel;
using Reservation.Common.Parameters;
using Reservation.Identity.Entities.Entities;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Interfaces;
using Reservation.Identity.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Reservation.Common.Extensions;
using Reservation.Common.Hasher;

namespace Reservation.Identity.Service.Services
{
    public class UserServices : BaseService<AspNetUser, IUserDto>, IUserServices
    {
        private readonly IIdentityUnitOfWork<AspNetRole> _roleUnitOfWork;
        private readonly IIdentityUnitOfWork<AspNetUsersRole> _usersRoleUnitOfWork;
        public UserServices(IBusinessBaseParameter<AspNetUser> businessBaseParameter, IIdentityUnitOfWork<AspNetRole> roleUnitOfWork, IIdentityUnitOfWork<AspNetUsersRole> usersRoleUnitOfWork) : base(businessBaseParameter)
        {
            _roleUnitOfWork = roleUnitOfWork;
            _usersRoleUnitOfWork = usersRoleUnitOfWork;
        }

        public async Task<IDataPagging> GetUsers(GetAllUserParameters parameters)
        {
            var users = await _unitOfWork.Repository.Find(q => !q.IsDeleted && q.Id != AdmistratorId, q => q.AspNetUsersRole);
            var roleIdList = users.SelectMany(q => q.AspNetUsersRole.Select(p => p.RoleId)).ToList();
            var rolesHave = await _roleUnitOfWork.Repository.Find(q => roleIdList.Contains(q.Id));
            users = !string.IsNullOrEmpty(parameters.UserName) ? users.Where(q => q.UserName.Contains(parameters.UserName)) : users;
            var usesrPagging = users.AsQueryable().OrderBy(parameters.OrderByValue).Skip(parameters.PageNumber).Take(parameters.PageSize).ToList();

            if (!usesrPagging.Any())
            {
                var res = ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                return new DataPagging(0, 0, 0, res);
            }
            var usersDto = Mapper.Map<IEnumerable<AspNetUser>, IEnumerable<IUserDto>>(usesrPagging);
            foreach (var item in usersDto)
            {
                var rolesUserHave = await _usersRoleUnitOfWork.Repository.Find(q => roleIdList.Contains(q.RoleId) && q.UserId == item.Id);
                var userRoleList = rolesUserHave.Select(q => q.RoleId).ToList();
                var role = rolesHave.Where(q => userRoleList.Contains(q.Id)).Select(p => p.Name).ToList();
                item.Roles = (role == null || role.Count == 0) ? null : String.Join(",", role.ToArray());
            }
            var repoResult = ResponseResult.GetRepositoryActionResult(usersDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            return new DataPagging(parameters.PageNumber, parameters.PageSize, users.Count(), repoResult);
        }
        public async Task<IResponseResult> GetUser(string lang, string Id)
        {
                var user =await _unitOfWork.Repository.FirstOrDefault(q => q.Id == Id);
                if (user == null) return ResponseResult.GetRepositoryActionResult(status:HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                var userDto = Mapper.Map<AspNetUser, IUserDto>(user);
                return ResponseResult.GetRepositoryActionResult(userDto,status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());           
        }
        public async Task<IResponseResult> AddUser(IUserDto userDto)
        {
                if (userDto == null) return ResponseResult.GetRepositoryActionResult(status:HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                var isExist =await _unitOfWork.Repository.FirstOrDefault(q => (q.UserName == userDto.UserName || q.Email == userDto.Email || (q.PhoneNumber == userDto.PhoneNumber && (userDto.PhoneNumber != "" && userDto.PhoneNumber != null))) && !q.IsDeleted) != null;
                if (isExist) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotAcceptable, message: HttpStatusCode.NotAcceptable.ToString());
                var user = Mapper.Map<IUserDto, AspNetUser>(userDto);
                user.Id = Guid.NewGuid().ToString();
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.PasswordHash = CreptoHasher.HashPassword(user.PasswordHash);
                var userAdded = _unitOfWork.Repository.Add(user);
                var userAddedDto = Mapper.Map<AspNetUser, IUserDto>(userAdded);
                await _unitOfWork.SaveChanges();
                return ResponseResult.GetRepositoryActionResult(userAddedDto,status: HttpStatusCode.Created, message: HttpStatusCode.Created.ToString());
        }
        public async Task<IResponseResult> UpdateUser(IUserDto userDto)
        {
                if (userDto == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                var isExist = await _unitOfWork.Repository.FirstOrDefault(q => (q.UserName == userDto.UserName || q.Email == userDto.Email || (q.PhoneNumber == userDto.PhoneNumber && (userDto.PhoneNumber != "" && userDto.PhoneNumber != null))) && q.Id != userDto.Id && !q.IsDeleted) != null;
                if (isExist) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NotAcceptable, message: HttpStatusCode.NotAcceptable.ToString());
                var original = await _unitOfWork.Repository.Get(userDto.Id);
                var user = Mapper.Map<IUserDto, AspNetUser>(userDto);
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.PasswordHash = CreptoHasher.HashPassword(user.PasswordHash);
                _unitOfWork.Repository.Update(original, user);
                var userAddedDto = Mapper.Map<AspNetUser, IUserDto>(original);
                await _unitOfWork.SaveChanges();
                return ResponseResult.GetRepositoryActionResult(userAddedDto, status: HttpStatusCode.Accepted, message: HttpStatusCode.Accepted.ToString());
        }
        public async Task<IResponseResult> RemoveUserById(string id)
        {
                if (id == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                var user = await _unitOfWork.Repository.FirstOrDefault(q => q.Id == id);
                user.IsDeleted = true;
                _unitOfWork.Repository.Update(user, user.Id);
                await _unitOfWork.SaveChanges();
                return ResponseResult.GetRepositoryActionResult(true,status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async Task<IResponseResult> IsUsernameExists(string name, string id)
        {
            var res =await _unitOfWork.Repository.FirstOrDefault(q => q.UserName == name && q.Id != id && !q.IsDeleted);
            return ResponseResult.GetRepositoryActionResult(res != null, status:HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async Task<IResponseResult> IsEmailExists(string email, string id)
        {
            var res = await _unitOfWork.Repository.FirstOrDefault(q => q.Email == email && q.Id != id && !q.IsDeleted);
            return ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async Task<IResponseResult> IsPhoneExists(string phone, string id)
        {
            var res = await _unitOfWork.Repository.FirstOrDefault(q => q.PhoneNumber == phone && q.Id != id && !q.IsDeleted);
            return ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        //=======================================
        public async Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber)
        {
            var users = !string.IsNullOrEmpty(searchTerm) ? await _unitOfWork.Repository.Find(n => !n.IsDeleted && n.Id != AdmistratorId && n.UserName.ToLower().Contains(searchTerm.ToLower())) : await _unitOfWork.Repository.Find(q => !q.IsDeleted && q.Id != AdmistratorId);
            var result = users.OrderBy(q => q.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(q => new Select2OptionModel { id = q.Id, text = q.UserName }).ToList();
            var select2pagedResult = new Select2PagedResult();
            select2pagedResult.Total = users.Count();
            select2pagedResult.Results = result;
            return select2pagedResult;
        }
        public async Task<IEnumerable<Select2OptionModel>> GetUserAssignedSelect2(string id)
        {
            var role = await _roleUnitOfWork.Repository.FirstOrDefault(q => q.Id == id, q => q.AspNetUsersRole, q => q.AspNetUsersRole);
            var userIdList = role.AspNetUsersRole.Select(q => q.UserId).ToList();
            var userassignQuery = await _unitOfWork.Repository.Find(q => userIdList.Contains(q.Id) && !q.IsDeleted);
            var userassign = userassignQuery.Select(q => new Select2OptionModel { id = q.Id, text = q.UserName }).ToList();
            return userassign;
        }
        public async Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters)
        {
            var role = await _roleUnitOfWork.Repository.FirstOrDefault(q => q.Id == parameters.RoleId, q => q.AspNetUsersRole);
            if (parameters.AssignedUser != null)
            {
                foreach (var item in parameters.AssignedUser)
                {
                    var isExist = await _usersRoleUnitOfWork.Repository.FirstOrDefault(q => q.UserId == item && q.RoleId == parameters.RoleId) != null;
                    if (!isExist)
                    {
                        var userRole = new AspNetUsersRole() { Id = Guid.NewGuid().ToString(), UserId = item, RoleId = parameters.RoleId };
                        _usersRoleUnitOfWork.Repository.Add(userRole);
                    }
                }
            }

            var userRemove = parameters.AssignedUser is null ? role.AspNetUsersRole : role.AspNetUsersRole.Where(q => !parameters.AssignedUser.Contains(q.UserId));
            _usersRoleUnitOfWork.Repository.RemoveRange(userRemove);
            await _usersRoleUnitOfWork.SaveChanges();
            return ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.Created, message: HttpStatusCode.Created.ToString());
        }
    }
}

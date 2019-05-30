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

namespace Reservation.Identity.Service.Services
{
    public class UserServices: BaseService<AspNetUser, IUserDto>, IUserServices
    {
        private readonly IIdentityUnitOfWork<AspNetRole> _roleUnitOfWork;
        private readonly IIdentityUnitOfWork<AspNetUsersRole> _usersRoleUnitOfWork;
        public UserServices(IBusinessBaseParameter<AspNetUser> businessBaseParameter, IIdentityUnitOfWork<AspNetRole> roleUnitOfWork, IIdentityUnitOfWork<AspNetUsersRole> usersRoleUnitOfWork) : base(businessBaseParameter)
        {
            _roleUnitOfWork = roleUnitOfWork;
            _usersRoleUnitOfWork = usersRoleUnitOfWork;
        }

        public async Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber)
        {
            var users = !string.IsNullOrEmpty(searchTerm) ? await _unitOfWork.Repository.Find(n => !n.IsDeleted && n.Id != AdmistratorId && n.UserName.ToLower().Contains(searchTerm.ToLower())) : await _unitOfWork.Repository.Find(q => !q.IsDeleted && q.Id != AdmistratorId);
            var result = users.OrderBy(q => q.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(q => new Select2OptionModel { id = q.Id, text = q.UserName }).ToList();
            var select2pagedResult = new Select2PagedResult();
            select2pagedResult.Total = users.Count();
            select2pagedResult.Results = result;
            return select2pagedResult;
        }
        public async Task<IEnumerable<Select2OptionModel>>  GetUserAssignedSelect2(string id)
        {
            var role =await _roleUnitOfWork.Repository.FirstOrDefault(q => q.Id == id, q => q.AspNetUsersRole, q => q.AspNetUsersRole);
            var userIdList = role.AspNetUsersRole.Select(q => q.UserId).ToList();
            var userassignQuery = await _unitOfWork.Repository.Find(q => userIdList.Contains(q.Id) && !q.IsDeleted);
            var userassign= userassignQuery.Select(q => new Select2OptionModel { id = q.Id, text = q.UserName }).ToList();
            return userassign;
        }
        public async Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters)
        {
                var role = await _roleUnitOfWork.Repository.FirstOrDefault(q => q.Id == parameters.RoleId, q => q.AspNetUsersRole);
                if (parameters.AssignedUser != null)
                {
                    foreach (var item in parameters.AssignedUser)
                    {
                        var isExist =await _usersRoleUnitOfWork.Repository.FirstOrDefault(q => q.UserId == item && q.RoleId == parameters.RoleId) != null;
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
                return ResponseResult.GetRepositoryActionResult(true,status: HttpStatusCode.Created,message:HttpStatusCode.Created.ToString());
        }
    }
}

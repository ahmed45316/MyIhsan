using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reservation.Common.Core;
using Reservation.Common.Extensions;
using Reservation.Common.IdentityInterfaces;
using Reservation.Common.Parameters;
using Reservation.Identity.Data.Context;
using Reservation.Identity.Data.SeedData;
using Reservation.Identity.Entities.Entities;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Services
{
    public class RoleServices : BaseService<AspNetRole, IRoleDto>, IRoleServices
    {
        public RoleServices(IBusinessBaseParameter<AspNetRole> businessBaseParameter) : base(businessBaseParameter)
        {

        }
        public async Task<IDataPagging> GetRoles(GetAllRoleParameters parameters)
        {
                var roles = string.IsNullOrEmpty(parameters.RoleName) ?await _unitOfWork.Repository.Find(q => !q.IsDeleted && q.Id != "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee", q => q.AspNetUsersRole) :await _unitOfWork.Repository.Find(q => !q.IsDeleted && q.Id != "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee" && q.Name.Contains(parameters.RoleName), q => q.AspNetUsersRole);
                var rolesPagging = roles.AsQueryable().OrderBy(parameters.OrderByValue).Skip(parameters.PageNumber).Take(parameters.PageSize).ToList();
                if (!rolesPagging.Any())
                {
                    var res = ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                    return new DataPagging(0,0,0,res);
                };
                var RolesDto = Mapper.Map<IEnumerable<AspNetRole>, IEnumerable<IGetRoleDto>>(rolesPagging);
                var repoResult = ResponseResult.GetRepositoryActionResult(RolesDto,status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
                return new DataPagging(parameters.PageNumber, parameters.PageSize, roles.Count(), repoResult);           
        }
        public async Task<IResponseResult> GetRole(string Id)
        {
           
                var role =await _unitOfWork.Repository.Get(Id);
                if (role == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                var roleDto = Mapper.Map<AspNetRole, IGetRoleDto>(role);
                return ResponseResult.GetRepositoryActionResult(roleDto,status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async Task<IResponseResult> AddRole(IGetRoleDto model)
        {
                if (model == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                var role = Mapper.Map<IGetRoleDto, AspNetRole>(model);
                var isExist = await _unitOfWork.Repository.FirstOrDefault(q => q.Name == model.Name && !q.IsDeleted) != null;
                if (isExist) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.BadRequest, message: HttpStatusCode.BadRequest.ToString());
                role.Id = Guid.NewGuid().ToString();
                var roleAdded = _unitOfWork.Repository.Add(role);
                await _unitOfWork.SaveChanges();
                var roleDto = Mapper.Map<AspNetRole, IGetRoleDto>(roleAdded);
                return ResponseResult.GetRepositoryActionResult(roleDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());           
        }
        public async Task<IResponseResult> UpdateRole(IUpdateRoleDto model)
        {
                if (model == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                var role = Mapper.Map<IUpdateRoleDto, AspNetRole>(model);
                var isExist = await _unitOfWork.Repository.FirstOrDefault(q => q.Name == model.Name && q.Id != model.Id && !q.IsDeleted) != null;
                if (isExist) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.BadRequest, message: HttpStatusCode.BadRequest.ToString());
                _unitOfWork.Repository.Update(role, role.Id);
                await _unitOfWork.SaveChanges();
                var RoleDto =await GetRole(role.Id);
                return RoleDto;
        }
        public async Task<IResponseResult> RemoveRoleById(string id)
        {
                if (id == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                var role = await _unitOfWork.Repository.FirstOrDefault(q => q.Id == id, q => q.AspNetUsersRole, q => q.Menu);
                if (role.AspNetUsersRole.Count > 0 || role.Menu.Count > 0)
                    return ResponseResult.GetRepositoryActionResult(false,status: HttpStatusCode.Forbidden, message: HttpStatusCode.Forbidden.ToString());
                role.IsDeleted = true;
                _unitOfWork.Repository.Update(role, role.Id);
                await _unitOfWork.SaveChanges();
                return ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.Accepted, message: HttpStatusCode.Accepted.ToString());
        }
        public IEnumerable<IRoleDto> GetRoleFromStored(string Name)
        {       
            var role = _unitOfWork.IdentityDbContext.AspNetRoles.FromSql($"GetRoles {Name}").ToList();
            var roleDto = Mapper.Map<List<AspNetRole>, List<IRoleDto>>(role);
            return roleDto;
        }
    }
}

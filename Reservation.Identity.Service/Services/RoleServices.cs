using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyIhsan.Identity.Service.Core;
using MyIhsan.Identity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyIhsan.Identity.Service.Dtos;
using MyIhsan.Common.Parameters;
using MyIhsan.Common.Core;
using MyIhsan.Identity.Entities.Entities;
using MyIhsan.Common.Extensions;

namespace MyIhsan.Identity.Service.Services
{
    public class RoleServices : BaseService<AspNetRoles, RoleDto>, IRoleServices
    {
        public RoleServices(IServiceBaseParameter<AspNetRoles> businessBaseParameter) : base(businessBaseParameter)
        {

        }
        public async Task<IDataPagging> GetRoles(GetAllRoleParameters parameters)
        {
            var roles = string.IsNullOrEmpty(parameters.RoleName) ? await _unitOfWork.Repository.FindAsync(q => !(q.IsDeleted??false)&& q.Id != AdmistratorRoleId, include: source => source.Include(a => a.AspNetUsersRoles), disableTracking: false) : await _unitOfWork.Repository.FindAsync(q => !(q.IsDeleted??false) && q.Id != AdmistratorRoleId && q.Name.Contains(parameters.RoleName), include: source => source.Include(a => a.AspNetUsersRoles), disableTracking: false);
            var rolesPagging = roles.AsQueryable().OrderBy(parameters.OrderByValue).Skip(parameters.PageNumber).Take(parameters.PageSize).ToList();
            if (!rolesPagging.Any())
            {
                var res = ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                return new DataPagging(0, 0, 0, res);
            };
            var RolesDto = Mapper.Map<IEnumerable<AspNetRoles>, IEnumerable<RoleDto>>(rolesPagging);
            var repoResult = ResponseResult.GetRepositoryActionResult(RolesDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            return new DataPagging(parameters.PageNumber, parameters.PageSize, roles.Count(), repoResult);
        }
        //public async Task<IResponseResult> GetRole(string Id)
        //{
           
        //        var role =await _unitOfWork.Repository.GetAsync(Id);
        //        if (role == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
        //        var roleDto = Mapper.Map<GetRoleDto>(role);
        //        return ResponseResult.GetRepositoryActionResult(roleDto,status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        //}
        //public async Task<IResponseResult> AddRole(GetRoleDto model)
        //{
        //        if (model == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
        //        var role = Mapper.Map<AspNetRoles>(model);
        //        var isExist = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Name == model.Name && !(q.IsDeleted?? false)) != null;
        //        if (isExist) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.BadRequest, message: HttpStatusCode.BadRequest.ToString());
        //        role.Id = Guid.NewGuid().ToString();
        //        var roleAdded = _unitOfWork.Repository.Add(role);
        //        await _unitOfWork.SaveChanges();
        //        var roleDto = Mapper.Map<GetRoleDto>(roleAdded);
        //        return ResponseResult.GetRepositoryActionResult(roleDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());           
        //}
        //public async Task<IResponseResult> UpdateRole(UpdateRoleDto model)
        //{
        //        if (model == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
        //        var role = Mapper.Map<AspNetRoles>(model);
        //        var isExist = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Name == model.Name && q.Id != model.Id && !(q.IsDeleted?? false)) != null;
        //        if (isExist) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.BadRequest, message: HttpStatusCode.BadRequest.ToString());
        //        _unitOfWork.Repository.Update(role, role.Id);
        //        await _unitOfWork.SaveChanges();
        //        var RoleDto =await GetRole(role.Id);
        //        return RoleDto;
        //}
        //public async Task<IResponseResult> RemoveRoleById(string id)
        //{
        //    if (id == null) return ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
        //    var role = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == id);
        //    role.IsDeleted = true;
        //    _unitOfWork.Repository.Update(role, role.Id);
        //    await _unitOfWork.SaveChanges();
        //    return ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.Accepted, message: HttpStatusCode.Accepted.ToString());
        //}
        // Check IsExists
        public async Task<IResponseResult> IsNameExists(string name, string id)
        {
            var res = await _unitOfWork.Repository.FirstOrDefaultAsync(q => q.Name == name && q.Id != id && !(q.IsDeleted?? false));
            return ResponseResult.GetRepositoryActionResult(res != null, status: HttpStatusCode.Accepted, message: HttpStatusCode.Accepted.ToString());
        }
        //for test using stored procedure
        public IEnumerable<RoleDto> GetRoleFromStored(string Name)
        {
            //another way to pass parameters
            //context.Database.ExecuteSqlCommand("GetRoles @p0, @p1", parameters: new[] { Name, "b" })
            //var role = _unitOfWork.IdentityDbContext.AspNetRoles.FromSql($"GetRoles {Name}").ToList();
            //var roleDto = Mapper.Map<List<RoleDto>>(role);
            //return roleDto;
            return null;
        }

    }
}

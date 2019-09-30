using MyIhsan.Common.Core;
using MyIhsan.Common.Parameters;
using MyIhsan.Identity.Entities.Entities;
using MyIhsan.Identity.Service.Core;
using MyIhsan.Identity.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Identity.Service.Interfaces
{
    public interface IRoleServices : IBaseService<AspNetRoles, RoleDto>
    {
        Task<IDataPagging> GetRoles(GetAllRoleParameters parameters);
        //Task<IResponseResult> GetRole(string Id);
        //Task<IResponseResult> AddRole(GetRoleDto model);
        //Task<IResponseResult> UpdateRole(UpdateRoleDto model);
        //Task<IResponseResult> RemoveRoleById(string id);
        IEnumerable<RoleDto> GetRoleFromStored(string Name);
        Task<IResponseResult> IsNameExists(string name, string id);
    }
}

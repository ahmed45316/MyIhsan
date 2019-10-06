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
        Task<IResponseResult> IsNameExists(string name, string id);
    }
}

using MyIhsan.Common.Core;
using MyIhsan.Common.Parameters;
using MyIhsan.Entities.Entities;
using MyIhsan.Service.Core;
using MyIhsan.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Service.Interfaces
{
    public interface IRoleServices : IBaseService<AspNetRoles, RoleDto>
    {
        Task<IDataPagging> GetRoles(GetAllRoleParameters parameters);
        Task<IResponseResult> IsNameExists(string name, string id);
    }
}

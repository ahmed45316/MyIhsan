using Reservation.Common.Core;
using Reservation.Common.IdentityInterfaces;
using Reservation.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Interfaces
{
    public interface IRoleServices
    {
        Task<IDataPagging> GetRoles(GetAllRoleParameters parameters);
        Task<IResponseResult> GetRole(string Id);
        Task<IResponseResult> AddRole(IGetRoleDto model);
        Task<IResponseResult> UpdateRole(IUpdateRoleDto model);
        Task<IResponseResult> RemoveRoleById(string id);
        IEnumerable<IRoleDto> GetRoleFromStored(string Name);
        Task<IResponseResult> IsNameExists(string name, string id);
    }
}

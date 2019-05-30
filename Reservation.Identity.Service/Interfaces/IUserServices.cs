using Reservation.Common.Core;
using Reservation.Common.IdentityInterfaces;
using Reservation.Common.OptionModel;
using Reservation.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Interfaces
{
    public interface IUserServices
    {
        Task<IDataPagging> GetUsers(GetAllUserParameters parameters);
        Task<IResponseResult> AddUser(IUserDto userDto);
        Task<IResponseResult> GetUser(string lang, string Id);
        Task<IResponseResult> UpdateUser(IUserDto userDto);
        Task<IResponseResult> RemoveUserById(string id);
        Task<IResponseResult> IsUsernameExists(string name, string id);
        Task<IResponseResult> IsEmailExists(string email, string id);
        Task<IResponseResult> IsPhoneExists(string phone, string id);
        Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber);
        Task<IEnumerable<Select2OptionModel>> GetUserAssignedSelect2(string id);
        Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters);
    }
}

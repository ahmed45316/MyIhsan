using MyIhsan.Common.Core;
using MyIhsan.Common.OptionModel;
using MyIhsan.Common.Parameters;
using MyIhsan.Identity.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Identity.Service.Interfaces
{
    public interface IUserServices
    {
        Task<IDataPagging> GetUsers(GetAllUserParameters parameters);
        Task<IResponseResult> AddUser(UserDto userDto);
        Task<IResponseResult> GetUser(string lang, string Id);
        Task<IResponseResult> UpdateUser(UserDto userDto);
        Task<IResponseResult> RemoveUserById(string id);
        Task<IResponseResult> IsUsernameExists(string name, string id);
        Task<IResponseResult> IsEmailExists(string email, string id);
        Task<IResponseResult> IsPhoneExists(string phone, string id);
        Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber);
        Task<IEnumerable<Select2OptionModel>> GetUserAssignedSelect2(string id);
        Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters);
    }
}

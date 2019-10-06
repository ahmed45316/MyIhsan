using MyIhsan.Common.Core;
using MyIhsan.Common.OptionModel;
using MyIhsan.Common.Parameters;
using MyIhsan.Identity.Service.Core;
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
        Task<IResponseResult> GetUser(long Id);
        Task<IResponseResult> UpdateUser(UserDto userDto);
        Task<IResponseResult> RemoveUserById(long id);
        Task<IResponseResult> IsUsernameExists(string name, long id);
        Task<IResponseResult> IsEmailExists(string email, long id);
        Task<IResponseResult> IsPhoneExists(string phone, long id);
        Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber);
        Task<IEnumerable<Select2OptionModel>> GetUserAssignedSelect2(long id);
        Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters);
    }
}

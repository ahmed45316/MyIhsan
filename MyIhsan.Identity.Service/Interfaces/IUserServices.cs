using MyIhsan.Common.Core;
using MyIhsan.Common.OptionModel;
using MyIhsan.Common.Parameters;
using MyIhsan.Entities.Views;
using MyIhsan.Service.Core;
using MyIhsan.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Service.Interfaces
{
    public interface IUserServices : IBaseService<AspNetUsers, UserDto>
    {
        Task<IDataPagging> GetUsers(GetAllUserParameters parameters);
        Task<IResponseResult> IsUsernameExists(string name, long id);
        Task<IResponseResult> IsEmailExists(string email, long id);
        Task<IResponseResult> IsPhoneExists(string phone, long id);
        Task<Select2PagedResult> GetUsersSelect2(string searchTerm, int pageSize, int pageNumber);
        Task<IEnumerable<Select2OptionModel>> GetUserAssignedSelect2(string id);
        Task<IResponseResult> SaveUserAssigned(AssignUserOnRoleParameters parameters);
    }
}

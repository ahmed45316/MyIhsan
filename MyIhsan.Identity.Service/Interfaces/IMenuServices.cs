using MyIhsan.Common.Core;
using MyIhsan.Common.OptionModel;
using MyIhsan.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Service.Interfaces
{
    public interface IMenuServices
    {
        Task<IResponseResult> GetMenu(string userId);
        Task<Select2PagedResult> GetScreensSelect2(string searchTerm, int pageSize, int pageNumber, string lang);
        Task<Select2PagedResult> GetChildScreensSelect2(string searchTerm, int pageSize, int pageNumber, string parentId, string lang);
        Task<IResponseResult> GetScreens(string roleId, string menuId, string childId);
        Task<IResponseResult> GetScreensSelected(string roleId, string menuId, string childId);
        Task<IResponseResult> SaveScreens(ScreensAssignedParameters parameters);
    }
}

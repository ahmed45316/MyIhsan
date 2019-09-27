using MyIhsan.Common.Core;
using MyIhsan.Common.OptionModel;
using MyIhsan.Common.Parameters;
using MyIhsan.Identity.Service.Core;
using MyIhsan.Identity.Service.Dtos;
using MyIhsan.Identity.Service.Interfaces;
using MyIhsan.Identity.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Identity.Service.Services
{
    public class MenuServices: BaseService<MenuDto, MenuDto>, IMenuServices
    {
        private readonly IIdentityUnitOfWork<UserDto> _userUnitOfWork; // user
        private readonly IIdentityUnitOfWork<RoleDto> _roleUnitOfWork; //role
        private readonly IIdentityUnitOfWork<MenuDto> _menuRoleUnitOfWork; //menurole
        private readonly List<string> _menuIdList;
        private readonly List<string> _menuIdSelectedList;
        public MenuServices(IBusinessBaseParameter<MenuDto> businessBaseParameter, IIdentityUnitOfWork<UserDto> userUnitOfWork, IIdentityUnitOfWork<RoleDto> roleUnitOfWork, IIdentityUnitOfWork<MenuDto> menuRoleUnitOfWork) : base(businessBaseParameter)
        {
            _userUnitOfWork = userUnitOfWork;
            _roleUnitOfWork = roleUnitOfWork;
            _menuRoleUnitOfWork = menuRoleUnitOfWork;
            _menuIdList = new List<string>();
            _menuIdSelectedList = new List<string>();
        }

        public async Task<IResponseResult> GetMenu(string userId)
        {
            return null;
        }
        //Screens
        public async Task<Select2PagedResult> GetScreensSelect2(string searchTerm, int pageSize, int pageNumber, string lang)
        {
            //var entityData = !string.IsNullOrEmpty(searchTerm) ? await _unitOfWork.Repository.Find(n => !n.IsStop && n.ParentId == null && n.ScreenNameAr.ToLower().Contains(searchTerm.ToLower())) :await _unitOfWork.Repository.Find(n => !n.IsStop && n.ParentId == null);
            //var result = entityData.OrderBy(q => q.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(q => new Select2OptionModel { id = q.Id, text = lang == "ar-EG" ? q.ScreenNameAr : q.ScreenNameEn }).ToList();
            //var select2pagedResult = new Select2PagedResult();
            //select2pagedResult.Total = entityData.Count();
            //select2pagedResult.Results = result;
            //return select2pagedResult;
            return null;
        }
        public async Task<Select2PagedResult> GetChildScreensSelect2(string searchTerm, int pageSize, int pageNumber, string parentId, string lang)
        {
            //var entityData = !string.IsNullOrEmpty(searchTerm) ?await _unitOfWork.Repository.Find(n => !n.IsStop && n.ParentId == parentId && n.ScreenNameAr.ToLower().Contains(searchTerm.ToLower())) :await _unitOfWork.Repository.Find(n => !n.IsStop && n.ParentId == parentId);
            //var result = entityData.OrderBy(q => q.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(q => new Select2OptionModel { id = q.Id, text = lang == "ar-EG" ? q.ScreenNameAr : q.ScreenNameEn }).ToList();
            //var select2pagedResult = new Select2PagedResult();
            //select2pagedResult.Total = entityData.Count();
            //select2pagedResult.Results = result;
            //return select2pagedResult;
            return null;
        }
        public async Task<IResponseResult> GetScreens(string roleId, string menuId, string childId)
        {
            return null;
        }
        public async Task<IResponseResult> GetScreensSelected(string roleId, string menuId, string childId)
        {
            return null;
        }
        public async Task<IResponseResult> SaveScreens(ScreensAssignedParameters parameters)
        {
            //if (parameters.ScreenAssigned != null)
            //{
            //    foreach (var ScreenId in parameters.ScreenAssigned)
            //    {
            //        var isExists = await _menuRoleUnitOfWork.Repository.FirstOrDefault(q => q.MenuId == ScreenId && q.RoleId == parameters.RoleId) != null;
            //        if (!isExists)
            //        {
            //            //var obj =new MenuRole() { Id = Guid.NewGuid().ToString(), RoleId = parameters.RoleId, MenuId = ScreenId };
            //           // _menuRoleUnitOfWork.Repository.Add(obj);
            //        }
            //    }
            //}
            //if (parameters.ScreenAssignedRemove != null)
            //{
            //    var dataRemoved = await _menuRoleUnitOfWork.Repository.Find(q => parameters.ScreenAssignedRemove.Contains(q.MenuId) && q.RoleId == parameters.RoleId);
            //    _menuRoleUnitOfWork.Repository.RemoveRange(dataRemoved);
            //}

            //await _menuRoleUnitOfWork.SaveChanges();
            return ResponseResult.GetRepositoryActionResult(true,status: HttpStatusCode.Created,message: HttpStatusCode.Created.ToString());
        }
        private async Task GetChilds(List<string> menuId)
        {
            var child =await _unitOfWork.Repository.Find(q => menuId.Contains(q.Id), q => q.Children);
            var res = child.SelectMany(p => p.Children.Select(q => q.Id)).ToList();
            _menuIdList.AddRange(res);
            var dd = child.Select(q => q.Children.Any()).Where(d => d == true).ToList();
            if (dd.Any()) await GetChilds(res);
        }
        private async Task GetChildsSelected(List<string> menuId)
        {
            var child = await _unitOfWork.Repository.Find(q => menuId.Contains(q.Id), q => q.Children);
            var res = child.SelectMany(p => p.Children.Select(q => q.Id)).ToList();
            _menuIdSelectedList.AddRange(res);
            var dd = child.Select(q => q.Children.Any()).Where(d => d == true).ToList();
            if (dd.Any())await GetChildsSelected(res);
        }
        
    }
}

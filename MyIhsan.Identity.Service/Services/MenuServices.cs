using Microsoft.EntityFrameworkCore;
using MyIhsan.Common.Core;
using MyIhsan.Common.OptionModel;
using MyIhsan.Common.Parameters;
using MyIhsan.Entities.Entities;
using MyIhsan.Service.Core;
using MyIhsan.Service.Dtos;
using MyIhsan.Service.Interfaces;
using MyIhsan.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Service.Services
{
    public class MenuServices : BaseService<Menus, MenuDto>, IMenuServices
    {
        private readonly IIdentityUnitOfWork<AspNetUsersRoles> _userRoleUnitOfWork;
        private readonly IIdentityUnitOfWork<AspNetRoles> _roleUnitOfWork;
        private readonly IIdentityUnitOfWork<MenuRoles> _menuRoleUnitOfWork;
        private readonly List<string> _menuIdList;
        private readonly List<string> _menuIdSelectedList;
        public MenuServices(IServiceBaseParameter<Menus> businessBaseParameter, IIdentityUnitOfWork<AspNetRoles> roleUnitOfWork, IIdentityUnitOfWork<MenuRoles> menuRoleUnitOfWork, IIdentityUnitOfWork<AspNetUsersRoles> userRoleUnitOfWork) : base(businessBaseParameter)
        {
            _userRoleUnitOfWork = userRoleUnitOfWork;
            _roleUnitOfWork = roleUnitOfWork;
            _menuRoleUnitOfWork = menuRoleUnitOfWork;
            _menuIdList = new List<string>();
            _menuIdSelectedList = new List<string>();
        }

        public async Task<IResponseResult> GetMenu(string userId)
        {
            var userRoles = await _userRoleUnitOfWork.Repository.FindAsync(q => q.UserId == userId);
            var roles = userRoles.Select(q => q.RoleId).ToList();
            var data = new List<Menus>();
            if (roles.Any())
            {
                if (roles[0] == "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee")
                {
                    var menuData = await _unitOfWork.Repository.FindAsync(q => !(q.IsStop??false) && q.ParentId == null, include: source => source.Include(a => a.Children).ThenInclude(b => b.Children), disableTracking: false);
                    data = menuData.ToList();
                }
                else
                {
                    var roleMenu = await _roleUnitOfWork.Repository.FindAsync(q => roles.Contains(q.Id), include: source => source.Include(a => a.Menu).ThenInclude(b => b.Menus), disableTracking: false);
                    var menuIds = roleMenu.SelectMany(q => q.Menu.Select(m => m.MenuId)).ToList();
                    var userMenu = await _unitOfWork.Repository.FindAsync(q => !(q.IsStop??false) && menuIds.Contains(q.Id), include: source => source.Include(a => a.Parent).ThenInclude(b => b.Parent).ThenInclude(q => q.Parent), disableTracking: false);
                    var menu = new List<Menus>();
                    foreach (var item in userMenu)
                    {

                        if (item.Parent == null)
                        {
                            menu.Add(item);
                        }
                        else if (item.Parent.Parent == null)
                        {
                            var parent = menu.Where(q => q.Id == item.Parent.Id).FirstOrDefault();
                            bool parentExist = parent != null;
                            if (!parentExist)
                            {
                                item.Parent.Children.ToList().ForEach(attr => item.Parent.Children.Remove(attr));
                                var m = item.Parent;
                                m.Children.Add(item);
                                menu.Add(m);
                            }
                            else
                            {
                                parent.Children.Add(item);
                            }
                        }
                        else if (item.Parent.Parent.Parent == null)
                        {
                            var parent = menu.Where(q => q.Id == item.Parent.Parent.Id).FirstOrDefault();
                            bool parentExist = parent != null;
                            if (!parentExist)
                            {
                                item.Parent.Parent.Children.ToList().ForEach(attr => item.Parent.Parent.Children.Remove(attr));
                                item.Parent.Children.ToList().ForEach(attr => item.Parent.Children.Remove(attr));
                                var m = item.Parent.Parent;
                                item.Parent.Children.Add(item);
                                m.Children.Add(item.Parent);
                                menu.Add(m);
                            }
                            else
                            {
                                var parentChild = parent.Children.Where(q => q.Id == item.Parent.Id).FirstOrDefault();
                                bool parentChildExist = parentChild != null;
                                if (!parentChildExist)
                                {
                                    item.Parent.Children.ToList().ForEach(attr => item.Parent.Children.Remove(attr));
                                    item.Parent.Children.Add(item);
                                    parentChild.Children.Add(item.Parent);
                                }
                                else
                                {
                                    parentChild.Children.Add(item);
                                }
                            }
                        }
                        else
                        {
                            var parent = menu.Where(q => q.Id == item.Parent.Parent.Parent.Id).FirstOrDefault();
                            bool parentExist = parent != null;
                            if (!parentExist)
                            {
                                item.Parent.Parent.Parent.Children.ToList().ForEach(attr => item.Parent.Parent.Children.Remove(attr));
                                item.Parent.Parent.Children.ToList().ForEach(attr => item.Parent.Parent.Children.Remove(attr));
                                item.Parent.Children.ToList().ForEach(attr => item.Parent.Children.Remove(attr));
                                var m = item.Parent.Parent.Parent;
                                item.Parent.Children.Add(item);
                                item.Parent.Parent.Children.Add(item.Parent);
                                m.Children.Add(item.Parent.Parent);
                                menu.Add(m);
                            }
                            else
                            {
                                var parentChild = parent.Children.Where(q => q.Id == item.Parent.Parent.Id).FirstOrDefault();
                                bool parentChildExist = parentChild != null;
                                if (!parentChildExist)
                                {
                                    item.Parent.Parent.Children.ToList().ForEach(attr => item.Parent.Parent.Children.Remove(attr));
                                    item.Parent.Children.ToList().ForEach(attr => item.Parent.Children.Remove(attr));
                                    item.Parent.Children.Add(item);
                                    item.Parent.Parent.Children.Add(item.Parent);
                                    parent.Children.Add(item.Parent.Parent);
                                }
                                else
                                {
                                    var parentChild1 = parentChild.Children.Where(q => q.Id == item.Parent.Id).FirstOrDefault();
                                    bool parentChild1Exist = parentChild1 != null;
                                    if (!parentChild1Exist)
                                    {
                                        item.Parent.Children.ToList().ForEach(attr => item.Parent.Children.Remove(attr));
                                        item.Parent.Children.Add(item);
                                        parentChild.Children.Add(item.Parent);
                                    }
                                    else
                                    {
                                        parentChild1.Children.Add(item);
                                    }
                                }
                            }
                        }
                    }

                    data = menu;
                }
            }
            var menuDto = Mapper.Map<List<Menus>, List<MenuDto>>(data);
            return ResponseResult.GetRepositoryActionResult(menuDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()); ;
        }
        //Screens
        public async Task<Select2PagedResult> GetScreensSelect2(string searchTerm, int pageSize, int pageNumber, string lang)
        {
            var entityData = !string.IsNullOrEmpty(searchTerm) ? await _unitOfWork.Repository.FindAsync(n => !(n.IsStop??false) && n.ParentId == null && n.ScreenNameAr.ToLower().Contains(searchTerm.ToLower())) : await _unitOfWork.Repository.FindAsync(n => !(n.IsStop??false) && n.ParentId == null);
            var result = entityData.OrderBy(q => q.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(q => new Select2OptionModel { id = q.Id, text = lang == "ar-EG" ? q.ScreenNameAr : q.ScreenNameEn }).ToList();
            var select2pagedResult = new Select2PagedResult();
            select2pagedResult.Total = entityData.Count();
            select2pagedResult.Results = result;
            return select2pagedResult;
        }
        public async Task<Select2PagedResult> GetChildScreensSelect2(string searchTerm, int pageSize, int pageNumber, string parentId, string lang)
        {
            var entityData = !string.IsNullOrEmpty(searchTerm) ? await _unitOfWork.Repository.FindAsync(n => !(n.IsStop ?? false) && n.ParentId == parentId && n.ScreenNameAr.ToLower().Contains(searchTerm.ToLower())) : await _unitOfWork.Repository.FindAsync(n => !(n.IsStop ?? false) && n.ParentId == parentId);
            var result = entityData.OrderBy(q => q.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(q => new Select2OptionModel { id = q.Id, text = lang == "ar-EG" ? q.ScreenNameAr : q.ScreenNameEn }).ToList();
            var select2pagedResult = new Select2PagedResult();
            select2pagedResult.Total = entityData.Count();
            select2pagedResult.Results = result;
            return select2pagedResult;
        }
        public async Task<IResponseResult> GetScreens(string roleId, string menuId, string childId)
        {
            var screenDto = new List<ScreenDto>();
            var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == roleId, include: source => source.Include(a => a.Menu), disableTracking: false);
            var dataAssigned = role.Menu.Select(q => q.MenuId).ToList();
            if (string.IsNullOrEmpty(menuId) || menuId == "null")
            {
                var menuu = await _unitOfWork.Repository.FindAsync(q => !(q.IsStop ?? false) && !dataAssigned.Contains(q.Id), include: source => source.Include(a => a.Children).Include(b => b.Parent), disableTracking: false);
                var dtAll = menuu.Where(s => s.Children.Count == 0).Distinct().ToList();
                foreach (var item in dtAll)
                {
                    var screen = new ScreenDto()
                    {
                        Id = item.Id,
                        ScreenNameAr = item.Parent == null ? item.ScreenNameAr : item.Parent.ScreenNameAr + ">" + item.ScreenNameAr,
                        ScreenNameEn = item.Parent == null ? item.ScreenNameEn : item.Parent.ScreenNameEn + ">" + item.ScreenNameEn
                    };
                    screenDto.Add(screen);
                }
                return ResponseResult.GetRepositoryActionResult(screenDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            }
            var parent = new Menus();
            if (string.IsNullOrEmpty(childId) || childId == "null")
            {
                parent = await _unitOfWork.Repository.FirstOrDefaultAsync(q => !(q.IsStop ?? false) && q.Id == menuId, include: source => source.Include(a => a.Children));
            }
            else
            {
                parent = await _unitOfWork.Repository.FirstOrDefaultAsync(q => !(q.IsStop ?? false) && q.Id == childId, include: source => source.Include(a => a.Children), disableTracking: false);
            }
            var idss = parent.Children.Select(q => q.Id).ToList();
            _menuIdList.Add(parent.Id); _menuIdList.AddRange(idss);
            await GetChilds(idss);
            var MenuIdsPass = _menuIdList.Distinct().ToList();
            var dataRes = await _unitOfWork.Repository.FindAsync(q => !(q.IsStop ?? false) && !dataAssigned.Contains(q.Id) && MenuIdsPass.Contains(q.Id), include: source => source.Include(a => a.Children).Include(b => b.Parent), disableTracking: false);
            var data = dataRes.Where(s => s.Children.Count == 0).Distinct().ToList();

            foreach (var item in data)
            {
                var screen = new ScreenDto()
                {
                    Id = item.Id,
                    ScreenNameAr = item.Parent == null ? item.ScreenNameAr : item.Parent.ScreenNameAr + ">" + item.ScreenNameAr,
                    ScreenNameEn = item.Parent == null ? item.ScreenNameEn : item.Parent.ScreenNameEn + ">" + item.ScreenNameEn
                };
                screenDto.Add(screen);
            }
            return ResponseResult.GetRepositoryActionResult(screenDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async Task<IResponseResult> GetScreensSelected(string roleId, string menuId, string childId)
        {
            var screenDto = new List<ScreenDto>();
            if (string.IsNullOrEmpty(menuId) || menuId == "null")
            {
                var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == roleId, include: source => source.Include(a => a.Menu), disableTracking: false);
                var dataAssigned = role.Menu.Select(q => q.MenuId).ToList();
                var dataRes = await _unitOfWork.Repository.FindAsync(q => !(q.IsStop ?? false) && dataAssigned.Contains(q.Id), include: source => source.Include(a => a.Children).Include(b => b.Parent), disableTracking: false);
                var data = dataRes.Where(s => s.Children.Count == 0).Distinct().ToList();
                foreach (var item in data)
                {
                    var screen = new ScreenDto()
                    {
                        Id = item.Id,
                        ScreenNameAr = item.Parent == null ? item.ScreenNameAr : item.Parent.ScreenNameAr + ">" + item.ScreenNameAr,
                        ScreenNameEn = item.Parent == null ? item.ScreenNameEn : item.Parent.ScreenNameEn + ">" + item.ScreenNameEn
                    };
                    screenDto.Add(screen);
                }
            }
            else
            {
                var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(q => q.Id == roleId, include: source => source.Include(a => a.Menu));
                var parent = new Menus();
                if (string.IsNullOrEmpty(childId) || childId == "null")
                {
                    parent = await _unitOfWork.Repository.FirstOrDefaultAsync(q => !(q.IsStop ?? false) && q.Id == menuId, include: source => source.Include(a => a.Children), disableTracking: false);
                }
                else
                {
                    parent = await _unitOfWork.Repository.FirstOrDefaultAsync(q => !(q.IsStop ?? false) && q.Id == childId, include: source => source.Include(a => a.Children), disableTracking: false);
                }
                var idss = parent.Children.Select(q => q.Id).ToList();
                _menuIdSelectedList.Add(parent.Id); _menuIdSelectedList.AddRange(idss);
                await GetChildsSelected(idss);
                var dataAssigned = role.Menu.Select(q => q.MenuId).ToList();
                var dataQuery = await _unitOfWork.Repository.FindAsync(q => !(q.IsStop ?? false) && dataAssigned.Contains(q.Id), include: source => source.Include(a => a.Children).Include(b => b.Parent), disableTracking: false);
                var data = dataQuery.Where(s => s.Children.Count == 0).Distinct().ToList();
                foreach (var item in data)
                {
                    if (_menuIdSelectedList.Contains(item.Id))
                    {
                        var screen = new ScreenDto()
                        {
                            Id = item.Id,
                            ScreenNameAr = item.Parent == null ? item.ScreenNameAr : item.Parent.ScreenNameAr + ">" + item.ScreenNameAr,
                            ScreenNameEn = item.Parent == null ? item.ScreenNameEn : item.Parent.ScreenNameEn + ">" + item.ScreenNameEn
                        };
                        screenDto.Add(screen);
                    }
                }
            }

            return ResponseResult.GetRepositoryActionResult(screenDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
        }
        public async Task<IResponseResult> SaveScreens(ScreensAssignedParameters parameters)
        {
            if (parameters.ScreenAssigned != null)
            {
                foreach (var ScreenId in parameters.ScreenAssigned)
                {
                    var isExists = await _menuRoleUnitOfWork.Repository.FirstOrDefaultAsync(q => q.MenuId == ScreenId && q.RoleId == parameters.RoleId) != null;
                    if (!isExists)
                    {
                        var obj = new MenuRoles() { Id = Guid.NewGuid().ToString(), RoleId = parameters.RoleId, MenuId = ScreenId };
                        _menuRoleUnitOfWork.Repository.Add(obj);
                    }
                }
            }
            if (parameters.ScreenAssignedRemove != null)
            {
                var dataRemoved = await _menuRoleUnitOfWork.Repository.FindAsync(q => parameters.ScreenAssignedRemove.Contains(q.MenuId) && q.RoleId == parameters.RoleId);
                _menuRoleUnitOfWork.Repository.RemoveRange(dataRemoved);
            }

            await _menuRoleUnitOfWork.SaveChanges();
            return ResponseResult.GetRepositoryActionResult(true, status: HttpStatusCode.Created, message: HttpStatusCode.Created.ToString());
        }
        private async Task GetChilds(List<string> menuId)
        {
            var child = await _unitOfWork.Repository.FindAsync(q => menuId.Contains(q.Id), include: source => source.Include(a => a.Children), disableTracking: false);
            var res = child.SelectMany(p => p.Children.Select(q => q.Id)).ToList();
            _menuIdList.AddRange(res);
            var dd = child.Select(q => q.Children.Any()).Where(d => d == true).ToList();
            if (dd.Any()) await GetChilds(res);
        }
        private async Task GetChildsSelected(List<string> menuId)
        {
            var child = await _unitOfWork.Repository.FindAsync(q => menuId.Contains(q.Id), include: source => source.Include(a => a.Children), disableTracking: false);
            var res = child.SelectMany(p => p.Children.Select(q => q.Id)).ToList();
            _menuIdSelectedList.AddRange(res);
            var dd = child.Select(q => q.Children.Any()).Where(d => d == true).ToList();
            if (dd.Any()) await GetChildsSelected(res);
        }

    }
}

using Reservation.Common.Core;
using Reservation.Common.IdentityInterfaces;
using Reservation.Identity.Entities.Entities;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Interfaces;
using Reservation.Identity.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Services
{
    public class MenuServices: BaseService<Menu, IMenuDto>, IMenuServices
    {
        private readonly IIdentityUnitOfWork<AspNetUser> _userUnitOfWork;
        private readonly IIdentityUnitOfWork<AspNetRole> _roleUnitOfWork;
        public MenuServices(IBusinessBaseParameter<Menu> businessBaseParameter, IIdentityUnitOfWork<AspNetUser> userUnitOfWork, IIdentityUnitOfWork<AspNetRole> roleUnitOfWork) : base(businessBaseParameter)
        {
            _userUnitOfWork = userUnitOfWork;
            _roleUnitOfWork = roleUnitOfWork;
        }

        public async Task<IResponseResult> GetMenu(string userId)
        {
            var roleData =await _userUnitOfWork.Repository.FirstOrDefault(q => q.Id == userId, q => q.AspNetUsersRole);
            var roles = roleData.AspNetUsersRole.Select(s => s.RoleId).ToList();
            var data = new List<Menu>();
            if (roles[0] == "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee")
            {
                var menuData = await _unitOfWork.Repository.Find(q => !q.IsStop && q.ParentId == null,q=>q.Children);
                data = menuData.ToList();
            }
            else
            {
                var roleMenu = await _roleUnitOfWork.Repository.Find(q => roles.Contains(q.Id), q => q.Menu.Select(m => m.Menu));
                var menuIds = roleMenu.SelectMany(q => q.Menu.Select(m => m.MenuId)).ToList();
                var userMenu = await _unitOfWork.Repository.Find(q => !q.IsStop && menuIds.Contains(q.Id), q => q.Parent.Parent.Parent);
                var menu = new List<Menu>();
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
            var menuDto = Mapper.Map<List<Menu>, List<IMenuDto>>(data);
            return ResponseResult.GetRepositoryActionResult(menuDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()); ;
        }
        //private async IEnumerable<Menu> GetChildern(IEnumerable<Menu> menus)
        //{
        //    foreach (var item in menus)
        //    {
        //        var data =await _unitOfWork.Repository.FirstOrDefault(q => !q.IsStop, q => q.Children);
        //        if (data.Children.Any())GetChildern(data.Children);
        //    }
        //    return null;
        //}
    }
}

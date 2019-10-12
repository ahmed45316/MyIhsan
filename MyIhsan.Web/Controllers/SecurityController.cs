using Newtonsoft.Json;
using MyIhsan.Repositories.Core;
using MyIhsan.Repositories.Parameters;
using MyIhsan.Repositories.Repository;
using MyIhsan.Web.Controllers.Base;
using MyIhsan.Web.Models;
using MyIhsan.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using fillGrids=MyIhsan.Web.Models.FillGrid;
using System.Threading.Tasks;

namespace MyIhsan.Web.Controllers
{
    public class SecurityController : BaseController
    {
        // GET: Security
        //[PageAuthorize]
        private readonly RestClientContainer restClientContainer;
        public SecurityController()
        {
            restClientContainer = new RestClientContainer(ConfigurationManager.AppSettings["ApiUrl"]);
        }
        public ActionResult Users()
        {
            return View();
        }
        // [PageAuthorize]
        public ActionResult ManageRoles()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Save(string id)
        {
            if (string.IsNullOrEmpty(id)) return PartialView(new RoleViewModel());
            var response = await restClientContainer.SendRequest<ResponseResult>($"Role/Get/{id}",RestSharp.Method.GET);
            string json = JsonConvert.SerializeObject(response.Data);
            var data = !string.IsNullOrEmpty(id) ? Helper<RoleViewModel>.Convert(json) : new RoleViewModel();
            RoleViewModel Role = data;
            return PartialView(Role);
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<ActionResult> SaveRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                RoleViewModel role = new RoleViewModel();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    //Edit
                    await restClientContainer.SendRequest<ResponseResult>("Role/Update",RestSharp.Method.PUT, model);
                    TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.updated;
                }
                else
                {
                    //Save
                    await restClientContainer.SendRequest<ResponseResult>("Role/Add", RestSharp.Method.POST, model);
                    TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.save;
                }
            }
            return RedirectToAction("ManageRoles", "Security");
        }

        //Delete Role
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var response = await restClientContainer.SendRequest<ResponseResult>($"Role/Remove/{id}",RestSharp.Method.DELETE);
            var data = (bool)response.Data;
            if (data == true) TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.deleted;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("Users")]
        public async Task<ActionResult> SaveUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id.HasValue && model.Id!=0)
                {
                    //Edit 
                   await restClientContainer.SendRequest<ResponseResult>("User/Update",RestSharp.Method.PUT, model);
                    TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.updated;
                }
                else
                {
                    //Save
                    await restClientContainer.SendRequest<ResponseResult>("User/Add", RestSharp.Method.POST, model);
                    TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.save;
                }
            }
            return RedirectToAction("Users", "Security");
        }

        //Delete User
        [HttpGet]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var response = await restClientContainer.SendRequest<ResponseResult>($"User/Remove/{id}",RestSharp.Method.DELETE);
            var data = (bool)response.Data;
            if (data == true) TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.deleted;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //Load Role Grid
        [HttpPost]
        public async Task<ActionResult> LoadData(RoleSearchParameters searchParam)
        {

            var draw = Request.Form.GetValues("draw") == null ? "1" : Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start") == null ? "0" : Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length") == null ? "10" : Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column

            var sortColumn = Request.Form.GetValues("order[0][column]") == null ? "" : Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]") == null ? "asc" : Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            var sort =new ReturnObject().GetSortModels((sortColumn == "" ? "Id" : sortColumn), sortColumnDir);
            var parameters = new GetAllRoleParameters()
            {
                RoleName = searchParam.RoleName,
                OrderByValue = sort,
                PageNumber = skip,
                PageSize = pageSize
            };
            var returnedData = await restClientContainer.SendRequest<DataPagging>("Role/GetAll", RestSharp.Method.POST,parameters);
            int recordsTotal = returnedData?.TotalPage??0;
            var data = returnedData?.Result?.Data;
            if (returnedData == null || returnedData.Result == null|| data == null)
            {
                return Json(new { draw = draw, recordsFiltered = 0, recordsTotal = 0, data = new fillGrids.RoleViewModel() }, JsonRequestBehavior.AllowGet);
            }
            string json = JsonConvert.SerializeObject(data);
            var listSerialized = new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = Helper<List<fillGrids.RoleViewModel>>.Convert(json)
            };
            return Json(listSerialized, JsonRequestBehavior.AllowGet);

        }
        ////Load User Grid
        [HttpPost]
        public async Task<ActionResult> LoadUserData(UserSearchParameters searchParam)
        {

            var draw = Request.Form.GetValues("draw") == null ? "1" : Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start") == null ? "0" : Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length") == null ? "10" : Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column

            var sortColumn = Request.Form.GetValues("order[0][column]") == null ? "" : Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]") == null ? "asc" : Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var sort = new ReturnObject().GetSortModels((sortColumn == "" ? "Id" : sortColumn), sortColumnDir);
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            var parameters = new GetAllUserParameters()
            {
                UserName = searchParam.UserName,
                OrderByValue = sort,
                PageNumber = skip,
                PageSize = pageSize
            };
            var returnedData = await restClientContainer.SendRequest<DataPagging>("User/GetAll",RestSharp.Method.POST, parameters);
            int recordsTotal = returnedData?.TotalPage ?? 0;
            var data = returnedData?.Result?.Data;
            if (returnedData == null || returnedData.Result == null || data==null)
            {
                return Json(new { draw = draw, recordsFiltered = 0, recordsTotal = 0, data = new fillGrids.UsersViewModel() }, JsonRequestBehavior.AllowGet);
            }
            string json = JsonConvert.SerializeObject(data);
            var jsonDeserialize = Helper<List<fillGrids.UsersViewModel>>.Convert(json);
            var listSerialized = new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = jsonDeserialize
            };
            return Json(listSerialized, JsonRequestBehavior.AllowGet);

        }
        //For Role
        [HttpPost]
        public async Task<ActionResult> CheckExistingName(string Name, string Id)
        {
            try
            {
                return Json(!await IsNameExists(Name, Id));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        private async Task<bool> IsNameExists(string name, string id)
        {
            var result = await restClientContainer.SendRequest<ResponseResult>($"Role/IsNameExists/{name}/{id}", RestSharp.Method.GET);
            var checks = (bool)result.Data;
            return checks;
        }
        //For User
        [HttpPost]
        public async Task<ActionResult> CheckUserNameExist(string UserName, string Id)
        {
            try
            {
                return Json(!await IsExists(UserName, 1, Id));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CheckEmailExist(string Email, string Id)
        {
            try
            {
                return Json(!await IsExists(Email, 2, Id));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CheckPhoneExist(string PhoneNumber, string Id)
        {
            try
            {
                return Json(!await IsExists(PhoneNumber, 3, Id));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        private async Task<bool> IsExists(string name, byte type, string id)
        {
            var result = await restClientContainer.SendRequest<ResponseResult>($"User/IsUsernameExists/{name}/{type}/{id}", RestSharp.Method.GET);
            var checks =(bool)result.Data;
            return checks;
        }

    }
}
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

namespace MyIhsan.Web.Controllers
{
    public class SecurityController : BaseController
    {
        // GET: Security
        //[PageAuthorize]
        private readonly RestClientContainer<ResponseResult> restClientContainer;
        private readonly RestClientContainer<DataPagging> restClientContainerPagging;
        public SecurityController()
        {
            restClientContainer = new RestClientContainer<ResponseResult>(ConfigurationManager.AppSettings["ApiUrl"]);
            restClientContainerPagging = new RestClientContainer<DataPagging>(ConfigurationManager.AppSettings["ApiUrl"]);
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
        public ActionResult Save(string id)
        {
            if (string.IsNullOrEmpty(id)) return PartialView(new RoleViewModel());
            var response = restClientContainer.Get($"Role/Get/{id}",Request.Cookies["token"].Value.ToString()).Result.Data;
            string json = JsonConvert.SerializeObject(response);
            var data = !string.IsNullOrEmpty(id) ? Helper<RoleViewModel>.Convert(json) : new RoleViewModel();
            RoleViewModel Role = data;
            return PartialView(Role);
        }

        [HttpPost]
        [ActionName("Save")]
        public ActionResult SaveRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                RoleViewModel role = new RoleViewModel();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    //Edit
                    var response = restClientContainer.Put("Role/Update", model, Request.Cookies["token"].Value.ToString()).Result.Data;
                    TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.updated;
                }
                else
                {
                    //Save
                    var response = restClientContainer.Post("Role/Add", model, Request.Cookies["token"].Value.ToString()).Result.Data;
                    TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.save;
                }
            }
            return RedirectToAction("ManageRoles", "Security");
        }

        //Delete Role
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var response = restClientContainer.Delete($"Role/Remove/{id}", Request.Cookies["token"].Value.ToString()).Result.Data;
            var data = (bool)response;
            if (data == true) TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.deleted;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUser(string id)
        {
            var response = restClientContainer.Get($"User/Get/{id}", Request.Cookies["token"].Value.ToString()).Result.Data;
            string json = JsonConvert.SerializeObject(response);
            var data = !string.IsNullOrEmpty(id) ? Helper<UserViewModel>.Convert(json): new UserViewModel();
            data.PasswordHash = null;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("Users")]
        public ActionResult SaveUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id.HasValue && model.Id!=0)
                {
                    //Edit 
                   var response= restClientContainer.Put("User/Update", model, Request.Cookies["token"].Value.ToString()).Result.Data;
                    TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.updated;
                }
                else
                {
                    //Save
                    var response = restClientContainer.Post("User/Add", model, Request.Cookies["token"].Value.ToString()).Result.Data;
                    TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.save;
                }
            }
            return RedirectToAction("Users", "Security");
        }

        //Delete User
        [HttpGet]
        public ActionResult DeleteUser(string id)
        {
            var response = restClientContainer.Delete($"User/Remove/{id}", Request.Cookies["token"].Value.ToString()).Result.Data;
            var data = (bool)response;
            if (data == true) TempData["AlertMessages"] = MyIhsan.LanguageResources.Translate.deleted;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //Load Role Grid
        [HttpPost]
        public ActionResult LoadData(RoleSearchParameters searchParam)
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
            var returnedData = restClientContainerPagging.Post("Role/GetAll", parameters, Request.Cookies["token"].Value.ToString()).Result ;
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
        public ActionResult LoadUserData(UserSearchParameters searchParam)
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
            var returnedData = restClientContainerPagging.Post("User/GetAll", parameters, Request.Cookies["token"].Value.ToString()).Result;
            int recordsTotal = returnedData?.TotalPage ?? 0;
            var data = returnedData?.Result?.Data;
            if (returnedData == null || returnedData.Result == null || data==null)
            {
                return Json(new { draw = draw, recordsFiltered = 0, recordsTotal = 0, data = new fillGrids.UsersViewModel() }, JsonRequestBehavior.AllowGet);
            }
            string json = JsonConvert.SerializeObject(data);
            var listSerialized = new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = Helper<List<fillGrids.UsersViewModel>>.Convert(json)
            };
            return Json(listSerialized, JsonRequestBehavior.AllowGet);

        }
        //For Role
        [HttpPost]
        public ActionResult CheckExistingName(string Name, string Id)
        {
            try
            {
                return Json(!IsNameExists(Name, Id));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        private bool IsNameExists(string name, string id)
        {
            var checks = (bool)restClientContainer.Get($"Role/IsNameExists/{name}/{id}", Request.Cookies["token"].Value.ToString()).Result.Data;
            return checks;
        }
        //For User
        [HttpPost]
        public ActionResult CheckUserNameExist(string UserName, string Id)
        {
            try
            {
                return Json(!IsExists(UserName, 1, Id));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        [HttpPost]
        public ActionResult CheckEmailExist(string Email, string Id)
        {
            try
            {
                return Json(!IsExists(Email, 2, Id));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        [HttpPost]
        public ActionResult CheckPhoneExist(string PhoneNumber, string Id)
        {
            try
            {
                return Json(!IsExists(PhoneNumber, 3, Id));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        private bool IsExists(string name, byte type, string id)
        {
            var checks =(bool) restClientContainer.Get($"User/IsExists/{name}/{type}/{id}", Request.Cookies["token"].Value.ToString()).Result.Data;
            return checks;
        }

    }
}
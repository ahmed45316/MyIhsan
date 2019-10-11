using MyIhsan.Repositories.Repository;
using MyIhsan.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyIhsan.Repositories.Parameters;
using MyIhsan.Web.Models.ViewModels;
using MyIhsan.Repositories.Core;

namespace MyIhsan.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly RestClientContainer<ResponseResult> restClientContainer;
        public LoginController()
        {
            restClientContainer = new RestClientContainer<ResponseResult>(ConfigurationManager.AppSettings["ApiUrl"]);
        }
        // GET: Login
        public ActionResult Index()
        {
            if (Session["userId"] != null || (HttpContext.Request.Cookies["userId"] != null && HttpContext.Request.Cookies["userId"].Value.ToString().Trim() != ""))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginParameters model)
        {
            var data = restClientContainer.Post("Accounts/Login",model).Result;
            if (data.Data != null)
            {
                string json= JsonConvert.SerializeObject(data.Data);
                var dataRet = Helper<UserLoginReturn>.Convert(json);
                Session["userId"] = dataRet.UserId;
                if (model.IsSavedPassword)
                {
                    Response.Cookies["userId"].Value = dataRet.UserId;
                    Response.Cookies["userId"].Expires = DateTime.Now.AddDays(6);
                }
                Session["userName"] = model.Username;
                Response.Cookies["token"].Value = dataRet.Token;
                Response.Cookies["token"].Expires = DateTime.Now.AddDays(7); // add expiry time

                return RedirectToAction("Index", "Home");
            }

            return View(new LoginParameters() { Status = false });
        }
        public ActionResult LogOut()
        {
            Session.Remove("userId");
            Session.Remove("Menu");
            Session.Remove("MyMenu");
            Response.Cookies["userId"].Value = "";
            Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
            Session.Remove("userName");
            Response.Cookies["token"].Value = "";
            Response.Cookies["token"].Expires = DateTime.Now.AddDays(-1); // add expiry time
            return RedirectToAction("Index", "Login");
        }
        public JsonResult CheckUserSession()
        {
            var res = false;
            res = (Session["userId"] != null ||( HttpContext.Request.Cookies["userId"] != null && HttpContext.Request.Cookies["userId"].Value.ToString().Trim() != "")) ? true : false;
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        private void call(string Lang)
        {
            if (Lang != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lang);
            }

            HttpCookie cookie = new HttpCookie("Lang");
            cookie.Value = Lang;
            cookie.Expires = DateTime.Now.AddDays(90);
            Response.Cookies.Add(cookie);

        }
        public ActionResult ChangeLanguage()
        {
            var cookie = Request.Cookies["Lang"];
            var lang = (cookie == null || cookie.Value.Trim() == "") ? "ar-EG" : cookie.Value == "ar-EG" ? "en-US" : "ar-EG";
            call(lang);
            string url = this.Request.UrlReferrer.AbsoluteUri;
            Session.Remove("MyMenu");
            return Redirect(url);
        }
       
    }
}
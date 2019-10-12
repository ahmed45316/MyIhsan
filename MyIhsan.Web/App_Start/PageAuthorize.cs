using MyIhsan.Repositories.Core;
using MyIhsan.Repositories.Repository;
using MyIhsan.Web.Models;
using MyIhsan.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyIhsan.Web
{
    public class PageAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (filterContext.HttpContext.Request.Cookies["userId"] != null && filterContext.HttpContext.Request.Cookies["userId"].Value.ToString().Trim() != "") session["userId"] = filterContext.HttpContext.Request.Cookies["userId"].Value;

            if (session["userId"] == null||(filterContext.HttpContext.Request.Cookies["token"] == null || filterContext.HttpContext.Request.Cookies["token"].Value.ToString().Trim() == ""))
            {
                session.Remove("Menu");
                session.Remove("MyMenu");
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                return;
            }
            var language = filterContext.HttpContext.Request.Cookies["Lang"] == null ? "en-US" : filterContext.HttpContext.Request.Cookies["Lang"].Value.ToString();
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (actionName.ToLower () == "Index".ToLower () && controllerName.ToLower () == "Home".ToLower())
            {
                session["PageName"] = language=="ar-EG"?"الشاشة الرئيسية":"Main Page";
                return;
            }
            var querystring = filterContext.RequestContext.HttpContext.Request.QueryString;
            string parameter = querystring["ScreenType"] == null ? null : Decoder.Decode(querystring["ScreenType"]);
            //IRestfulApi<List<bool>, List<bool>> res = new RestfulApi<List<bool>, List<bool>>(ConfigurationManager.AppSettings["ApiUrl"]);
            var restClientContainer = new RestClientContainer(ConfigurationManager.AppSettings["ApiUrl"]);
            //var menu = res.GetAsyncByGetVerb($"Role/CanShowPage/{language}/{controllerName}/{actionName}/{parameter}", null, filterContext.HttpContext.Request.Cookies["token"].Value.ToString()).Result;
            //if (menu != null ) session["PageName"] = menu;
            //if (menu == null )
            //{
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "UnAuthorize", action = "Index" }));
           // }
        }
    }
}
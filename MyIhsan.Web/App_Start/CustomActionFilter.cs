using Newtonsoft.Json;
using MyIhsan.Repositories.Core;
using MyIhsan.Repositories.Repository;
using MyIhsan.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyIhsan.Web
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (filterContext.HttpContext.Request.Cookies["userId"] != null && filterContext.HttpContext.Request.Cookies["userId"].Value.ToString().Trim() != "") session["userId"] = filterContext.HttpContext.Request.Cookies["userId"].Value;

            if (session["userId"] == null || (filterContext.HttpContext.Request.Cookies["token"] == null || filterContext.HttpContext.Request.Cookies["token"].Value.ToString().Trim() == ""))
            {
                    session.Remove("Menu");
                    session.Remove("MyMenu");
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                        { "Controller", "Login" },
                                { "Action", "Index" }
                        });
                }
                else
                {
                    if (session["Menu"] == null)
                    {
                    var restClientContainer = new RestClientContainer(ConfigurationManager.AppSettings["ApiUrl"]);
                    var dataRet = restClientContainer.SendRequest<ResponseResult>("Accounts/GetMenu",RestSharp.Method.GET).Result.Data;
                    string json = JsonConvert.SerializeObject(dataRet);
                    var result=Helper<List<MenuViewModel>>.Convert(json);
                    session["Menu"] = result;
                    }
                }
        }
    }
}
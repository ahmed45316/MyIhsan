using MyIhsan.Repositories.Repository;
using MyIhsan.Web.Controllers.Base;
using MyIhsan.Web.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace MyIhsan.Web.Controllers
{
    public class HomeController : BaseController
    {
        //[PageAuthorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
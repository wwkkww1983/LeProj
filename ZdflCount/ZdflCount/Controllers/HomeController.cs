using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZdflCount.Controllers
{
    [ZdflCount.Filters.InitializeSimpleMembership]
    [App_Start.UserLoginAuthentication]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "纱布计数管理系统";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "系统使用说明";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "使用问题反馈对象";

            return View();
        }
    }
}

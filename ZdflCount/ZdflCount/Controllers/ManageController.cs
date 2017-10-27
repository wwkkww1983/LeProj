using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZdflCount.App_Start;
using System.Web.Mvc;

namespace ZdflCount.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /Setting/

        public ActionResult Index()
        {
            ViewData["ServerStatus"] = TcpProtocolClient.KeepListening;

            return View();
        }
        #region 服务器操作
        [HttpPost]
        public ActionResult StartServer()
        {
            if (!TcpProtocolClient.KeepListening)
            {
                TcpProtocolClient.StartServer();
            } 
            ViewData["ServerStatus"] = TcpProtocolClient.KeepListening;
            return View("Index");
        }
        #endregion
    }
}

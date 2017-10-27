using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZdflCount.App_Start;
using System.Web.Mvc;
using ZdflCount.Models;

namespace ZdflCount.Controllers
{
    public class ManageController : Controller
    {
        private DbTableDbContext db = new DbTableDbContext();
        //
        // GET: /Setting/

        public ActionResult Index()
        {
            ViewData["ServerStatus"] = TcpProtocolClient.KeepListening;

            return View();
        }

        #region 设备
        public ActionResult Machines()
        {
            var itemList = from item in db.Machines
                           orderby item.Status
                           select item;
            return View(itemList);
        }
        #endregion


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

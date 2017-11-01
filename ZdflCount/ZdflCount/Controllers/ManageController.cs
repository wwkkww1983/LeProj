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

        #region 设备
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult Machines()
        {
            var itemList = from item in db.Machines
                           orderby item.Status ascending, item.RoomID ascending
                           select item;
            return View(itemList);
        }
        #endregion

        #region 工厂车间
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult Rooms()
        {
            return View(db.FactoryRoom);
        }
        #endregion

        #region 服务器操作

        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult Index()
        {
            ViewData["ServerStatus"] = TcpProtocolClient.KeepListening;

            return View();
        }


        [HttpPost]
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult StartServer()
        {
            if (!TcpProtocolClient.KeepListening)
            {
                TcpProtocolClient.StartServer();
            } 
            return RedirectToAction("Index"); 
        }
        #endregion

        #region 统计信息

        [UserRoleAuthentication(Roles = "生产统计数据查看")]
        public ActionResult Statistics()
        {
            return View();
        }
        
        [UserRoleAuthentication(Roles = "生产统计数据查看")]
        public ActionResult Statistics(DateTime startDate,DateTime endDate)
        {
            return View();
        }
        #endregion
    }
}

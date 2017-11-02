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
        
        [HttpPost]
        [UserRoleAuthentication(Roles = "生产统计数据查看")]
        public JsonResult Statistics(DateTime startDate,DateTime endDate)
        {
            JsonResult result = new JsonResult();
            string horizon = Request.Form["horizon"], vertical = Request.Form["vertical"], userNumber = Request.Form["userNumber"] ,
                userName = Request.Form["userName"], machine = Request.Form["machine"],order = Request.Form["order"],room = Request.Form["room"];
            int roomId = room == null ? 0 : int.Parse(room);
            DateTime endDateQuery = endDate.Date.AddDays(1);
            IEnumerable<StatisticInfo> infoList = from item in db.Statistics
                                                  where item.Date >= startDate.Date && item.Date <= endDateQuery &&
                                                          (string.IsNullOrEmpty(userNumber) || item.StaffNumber == userNumber) &&
                                                          (string.IsNullOrEmpty(userName) || item.StaffName == userName) &&
                                                          (string.IsNullOrEmpty(machine) || item.MachineNumber == machine) &&
                                                          (string.IsNullOrEmpty(order) || item.OrderNumber == order) &&
                                                          (string.IsNullOrEmpty(room) || item.RoomID == roomId)
                                                   //group by (case 
                                                   //     when horizon="staff" then item.StaffName end) g
                                                  select item;
            Dictionary<string, int> tempDict = new Dictionary<string, int>();
            string strKey = string.Empty;
            int intValue = 0;
            foreach (StatisticInfo item in infoList)
            {
                switch (horizon)
                {
                    case "staff": strKey = item.StaffName; break;
                    case "machine": strKey = item.MachineNumber; break;
                    case "order": strKey = item.OrderNumber; break;
                    case "room": strKey = item.RoomName; break;
                    case "factory": strKey = item.Factory; break;
                    default: break;
                }
                intValue = vertical == "已完成数量" ? item.FinishCount : item.ExceptionCount;
                if (tempDict.ContainsKey(strKey))
                    tempDict[strKey] += intValue;
                else
                    tempDict.Add(strKey, intValue);
            }

            string tempTitle = startDate.ToString("yyyy年MM月dd日") + " 至 " + endDate.ToString("yyyy年MM月dd日，") + vertical;
            result.Data = new { chartTitle = tempTitle, dataKey=tempDict.Keys.ToArray(),dataValue = tempDict.Values.ToArray() };
            return result;
        }
        #endregion
    }
}

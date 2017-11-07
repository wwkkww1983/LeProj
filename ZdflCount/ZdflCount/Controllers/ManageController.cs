using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZdflCount.App_Start;
using System.Web.Mvc;
using ZdflCount.Models;

namespace ZdflCount.Controllers
{
    [App_Start.UserLoginAuthentication]
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

        #region 系统监控

        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult Index(string error)
        {
            ViewData["ServerStatus"] = TcpProtocolClient.KeepListening;
            ViewData["error"] = error;
            
            return View(db.ErrorInfo.OrderBy(item => item.ErrorType).OrderByDescending(item => item.ID).Take(20));
        }


        [HttpPost]
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult StartServer()
        {
            string strError = null;
            if (!TcpProtocolClient.KeepListening)
            {
                strError = TcpProtocolClient.StartServer(Convert.ToInt32(Session["UserID"]));
            }
            return RedirectToAction("Index", new { error = strError });
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult StopServer()
        {
            if (TcpProtocolClient.KeepListening)
            {
                TcpProtocolClient.StopListen();
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
        public ActionResult RoomsDownList()
        {
            List<SelectListItem> roomList = new List<SelectListItem>();
            roomList.Add(new SelectListItem()
            {
                Text = "请选择车间",
                Value = "",
                Selected = true
            });
            foreach (FactoryRoom room in db.FactoryRoom)
            {
                roomList.Add(new SelectListItem()
                {
                    Selected = false,
                    Text = room.RoomName,
                    Value = room.RoomID.ToString()
                });
            }
            return PartialView("_RoomsPartial", roomList);
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

        #region 设备状态

        private bool CheckValidTime(DateTime lastTime)
        {
            //有效期：5分钟
            return lastTime.AddMinutes(5) > DateTime.Now;
        }

        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult MachineStatus(bool refresh=false)
        {
            if (!refresh)
            {//数据自刷新
                foreach (DeviceStatus device in GlobalVariable.deviceStatusList)
                {
                    foreach (KeyValuePair<string, DateTime> item in device.MachineList)
                    {
                        if (CheckValidTime(item.Value))
                            continue;
                        device.MachineList.Remove(item.Key);
                    }
                }
            }
            else
            {//重新加载数据
                GlobalVariable.deviceStatusList.Clear();
                foreach (FactoryRoom room in db.FactoryRoom)
                {
                    DeviceStatus device = new DeviceStatus()
                    {
                        RoomID = room.RoomID,
                        RoomName = room.RoomName,
                        MachineCount = room.MachineCount,
                        FactoryName = room.FactoryName,
                        MachineList = new Dictionary<string, DateTime>()
                    };
                    GlobalVariable.deviceStatusList.Add(device);
                }
                foreach (DeviceStatus device in GlobalVariable.deviceStatusList)
                {
                    IEnumerable<LastHeartBreak> lastItemList = from tempMachine in db.LastHeartBreak
                                                               where tempMachine.RoomID == device.RoomID
                                                               select tempMachine;
                    foreach (LastHeartBreak item in lastItemList)
                    {
                        if (CheckValidTime(item.DateRefresh))
                            device.MachineList.Add(item.MachineName, item.DateRefresh);
                    }
                }
            }
            return View(GlobalVariable.deviceStatusList);
        }
        #endregion
    }
}

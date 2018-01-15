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
        private const string ONLINE_FACTRORY_ROOM = "ONLINEFACTORYROOM", PRE_ROOM_NAME_NUMBER = "PREROOMNAMENUMBER", PRE_ONLINE_MACHINE = "PREONLINEMACHINE",
                            PRE_MACHINE_NAME_NUMBER = "PREMACHINENAMENUMBER", PRE_ONLINE_TIME = "PREONLINETIME";
        private DbTableDbContext db = new DbTableDbContext();
        private static ServiceStack.Redis.IRedisClient client = Constants.RedisClient;

        #region 设备
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult Machines()
        {
            var itemList = from item in db.Machines
                           orderby item.Status ascending, item.RoomID ascending
                           select item;
            return View(itemList);
        }


        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult MachineEdit(int id)
        {
            return View(db.Machines.Find(id));
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult MachineEdit(int id, string Name, string RemarkInfo)
        {
            Machines machine = db.Machines.Find(id);
            db.Machines.Attach(machine);
            machine.Name = Name;
            machine.RemarkInfo = RemarkInfo;
            db.SaveChanges();

            string strName = client.Get<string>(PRE_MACHINE_NAME_NUMBER + machine.Number);
            if (strName != null && strName != string.Empty)
            {
                client.Set<string>(PRE_MACHINE_NAME_NUMBER + machine.Number, Name);
            }
                

            return RedirectToAction("Machines");
        }


        #endregion

        #region 工厂车间
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult Rooms()
        {
            return View(db.FactoryRoom);
        }

        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult RoomCreate()
        {
            return View();
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult RoomCreate(FactoryRoom room)
        {
            IEnumerable<FactoryRoom> tempRoom = from item in db.FactoryRoom
                                                where item.RoomNumber == room.RoomNumber || item.RoomName == room.RoomName
                                                select item;
            if (tempRoom.Count() > 0)
            {
                ViewData["error"] = "【车间编号】或【车间名称】 重复";
                return View(room);
            }
            //车间增加
            db.FactoryRoom.Add(room);
            db.SaveChanges();
            //添加超级管理员
            db.UsersInRooms.Add(new UsersInRooms()
            {
                RoomId = room.RoomID,
                UserId = 1
            });
            db.SaveChanges();

            return RedirectToAction("Rooms");
        }

        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult RoomEdit(int id)
        {
            return View(db.FactoryRoom.Find(id));
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult RoomEdit(FactoryRoom room)
        {
            FactoryRoom dbRoom = db.FactoryRoom.Find(room.RoomID);
            string oldRoomName = dbRoom.RoomName;
            dbRoom.ManagerName = room.ManagerName;
            dbRoom.ManagerPhone = room.ManagerPhone;
            dbRoom.Phone = room.Phone;
            dbRoom.Remarks = room.Remarks;
            dbRoom.RepairNumber = room.RepairNumber;
            dbRoom.Address = room.Address;
            if (dbRoom.RoomNumber != room.RoomNumber || dbRoom.RoomName != room.RoomName)
            {
                IEnumerable<Machines> machineList = from item in db.Machines
                                                    where item.RoomID == dbRoom.RoomID
                                                    select item;
                foreach (Machines machine in machineList)
                {
                    db.Machines.Attach(machine);
                    machine.RoomName = room.RoomName;
                    machine.RoomNumber = room.RoomNumber;
                }
                IEnumerable<StaffInfo> staffList = from item in db.StaffInfo
                                                   where item.DeptId == dbRoom.RoomID
                                                   select item;
                foreach (StaffInfo staff in staffList)
                {
                    db.StaffInfo.Attach(staff);
                    staff.DeptName = room.RoomName;
                    staff.DeptNumber = room.RoomNumber;
                }
                //移除原来车间名称的心跳包，新车间名称会在新的心跳包过来时
                dbRoom.RoomName = room.RoomName;
                dbRoom.RoomNumber = room.RoomNumber;                
            }
            db.SaveChanges(); 
            if (oldRoomName != room.RoomName)
            {
                client.Set<string>(PRE_ONLINE_MACHINE + dbRoom.RoomNumber, room.RoomName);
            }

            return RedirectToAction("Rooms");
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
                userName = Request.Form["userName"], strMachine = Request.Form["machine"],order = Request.Form["order"],room = Request.Form["room"];
            int roomId = room == null ? 0 : int.Parse(room), machineId = strMachine == null ? 0 : int.Parse(strMachine);
            DateTime endDateQuery = endDate.Date.AddDays(1);
            IEnumerable<StatisticInfo> infoList = from item in db.Statistics
                                                  where item.DateOut >= startDate.Date && item.DateOut <= endDateQuery &&
                                                          (string.IsNullOrEmpty(userNumber) || item.StaffNumber == userNumber) &&
                                                          (string.IsNullOrEmpty(userName) || item.StaffName == userName) &&
                                                          (string.IsNullOrEmpty(strMachine) || item.MachineId == machineId) &&
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
                    case "machine": strKey = item.MachineName; break;
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
            string fileName = string.Format("{0}-{1}-{2}", vertical, DateTime.Now.ToString("yyyyMMdd"),DateTime.Now.Millisecond);
            //Excel.CreateExcelFile(fileName, tempTitle, tempDict.Keys.ToArray(), tempDict.Values.ToArray());
            result.Data = new { chartTitle = tempTitle,fileName=fileName, dataKey = tempDict.Keys.ToArray(), dataValue = tempDict.Values.ToArray() };

            return result;
        }

        [UserRoleAuthentication(Roles = "生产统计数据查看")]
        public ActionResult DownloadExcel()
        {
            string fileName = Request["excel"]+ ".xlsx";
            string filePath = "/Statis/" + fileName;
            if (!System.IO.File.Exists(filePath))
            {
                return Content("无数据");
            }

            FilePathResult file = new FilePathResult("~/Statis/" + fileName , "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            file.FileDownloadName = fileName + ".xlsx";

            return file;
        }
        #endregion

        #region 生产流水记录
        public ActionResult ProductRecord()
        {
            object model = null;
            string strStartDate = Common.getStartDate(Request["startDate"]), strEndDate = Common.getEndDate(Request["endDate"]);
            DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);

            model = (from item in db.ProductInfo
                     where item.DateCreate >= startDate && item.DateCreate <= endDate
                     orderby item.ID descending
                     select item).Take(Constants.ITEM_COUNT_EACH_PAGE_DEFAULT);

            ViewData["startDate"] = strStartDate;
            ViewData["endDate"] = strEndDate;
            return View(model);
        }
        #endregion

        #region 统计基础数据
        public ActionResult StatisticRecord()
        {
            object model = null;
            string strStartDate = Common.getStartDate(Request["startDate"]), strEndDate = Common.getEndDate(Request["endDate"]);
            DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);

            model = (from item in db.Statistics
                    where item.DateOut >= startDate && item.DateOut <= endDate
                    orderby item.ID descending
                     select item).Take(Constants.ITEM_COUNT_EACH_PAGE_DEFAULT);

            ViewData["startDate"] = strStartDate;
            ViewData["endDate"] = strEndDate;
            return View(model);
        }
        #endregion 

        #region 设备状态

        private bool CheckValidTime(DateTime lastTime)
        {
            //有效期：5分钟
            return lastTime.AddMinutes(5) > DateTime.Now;
        }

        private List<DeviceStatus> RefreshMachineStatus(bool refresh = false)
        {            
            List<DeviceStatus> deviceStatusList = new List<DeviceStatus>();            
            if (!refresh)
            {//数据自刷新
                HashSet<string> roomList = client.GetAllItemsFromSet(ONLINE_FACTRORY_ROOM);
                foreach (string room in roomList)
                {
                    FactoryRoom roomItem = db.FactoryRoom.FirstOrDefault(item => item.RoomNumber == room);
                    DeviceStatus tempStatus = new DeviceStatus()
                    {
                        RoomName = client.Get<string>(PRE_ROOM_NAME_NUMBER + room),
                        MachineList = new Dictionary<string, DateTime>()
                    };
                    HashSet<string> machineList = client.GetAllItemsFromSet(PRE_ONLINE_MACHINE + room);
                    foreach (string machine in machineList)
                    {
                        DateTime lastTime = new DateTime(client.Get<long>(PRE_ONLINE_TIME + machine));
                        if (CheckValidTime(lastTime))
                        {
                            string strName = client.Get<string>(PRE_MACHINE_NAME_NUMBER + machine);
                            tempStatus.MachineList.Add(strName ?? machine, lastTime);
                        }
                        else
                        {
                            client.Remove(PRE_ONLINE_TIME + machine);
                            client.RemoveItemFromSet(PRE_ONLINE_MACHINE + room, machine);
                        }
                    }
                    if (tempStatus.MachineList.Count < 1)
                    {
                        client.RemoveItemFromSet(ONLINE_FACTRORY_ROOM, room);
                    }
                    IEnumerable<Machines> roomMachines = from item in db.Machines
                                                         where item.RoomID == roomItem.RoomID
                                                         select item;
                    List<string> freeMachineList = new List<string>();
                    foreach (Machines item in roomMachines)
                    {
                        if (!tempStatus.MachineList.Keys.Contains(item.Name) && !tempStatus.MachineList.Keys.Contains(item.Number))
                            freeMachineList.Add(item.Name);
                    }
                    tempStatus.OfflineMachines = freeMachineList.ToArray();
                    tempStatus.MachineCount = tempStatus.OfflineMachines.Count() + tempStatus.MachineList.Count;
                    deviceStatusList.Add(tempStatus);
                }
            }
            else
            {//重新加载数据
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
                    deviceStatusList.Add(device);
                }
                foreach (DeviceStatus device in GlobalVariable.deviceStatusList)
                {
                    IEnumerable<LastHeartBreak> lastItemList = from tempMachine in db.LastHeartBreak
                                                               where tempMachine.RoomID == device.RoomID
                                                               select tempMachine;
                    List<string> freeMachineList = new List<string>();
                    foreach (LastHeartBreak item in lastItemList)
                    {
                        if (CheckValidTime(item.DateRefresh))
                            device.MachineList.Add(item.MachineName, item.DateRefresh);
                        else
                            freeMachineList.Add(item.MachineName);
                    }
                    device.OfflineMachines = freeMachineList.ToArray();
                }
            }
            return deviceStatusList;
        }

        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult MachineStatusContent()
        {
            return PartialView("_StatusPartial", this.RefreshMachineStatus(false));
        }


        [UserRoleAuthentication(Roles = "系统管理员")]
        public ActionResult MachineStatus(bool refresh=false)
        {
            return View(this.RefreshMachineStatus(refresh));
        }
        #endregion

        #region 设备报料
        [UserRoleAuthentication(Roles = "报料管理员")]
        public ActionResult MachineMaterial()
        {
            IEnumerable<MachineCallMaterial> materialList = from item in db.MachineCallMaterial
                                                            orderby item.Status ascending, item.ID descending
                                                            select item;
            return View(materialList);
        }

        [UserRoleAuthentication(Roles = "报料管理员")]
        public ActionResult MachineMaterialContent()
        {
            IEnumerable<MachineCallMaterial> materialList = from item in db.MachineCallMaterial
                                                            orderby item.Status ascending, item.ID descending
                                                            select item;
            return PartialView("_MaterialPartial", materialList);
        }

        [UserRoleAuthentication(Roles = "报料管理员")]
        public ActionResult MaterialGetInfo(int id)
        {
            MachineCallMaterial material = db.MachineCallMaterial.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }

            db.MachineCallMaterial.Attach(material);
            material.Status = enumDeviceWarnningStatus.GetInfo;
            material.DateGetInfo = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("MachineMaterial");
        }
        #endregion

        #region 设备报修
        [UserRoleAuthentication(Roles = "报修管理员")]
        public ActionResult MachineReport()
        {
            IEnumerable<MachineReport> reportList = from item in db.MachineReport
                                                    orderby item.Status ascending, item.ID descending
                                                    select item;
            return View(reportList);
        }

        [UserRoleAuthentication(Roles = "报修管理员")]
        public ActionResult MachineReportContent()
        {
            IEnumerable<MachineReport> reportList = from item in db.MachineReport
                                                    orderby item.Status ascending, item.ID descending
                                                    select item;
            return PartialView("_ReportPartial", reportList);
        }

        [UserRoleAuthentication(Roles = "报修管理员")]
        public ActionResult ReportGetInfo(int id)
        {
            MachineReport report = db.MachineReport.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }

            db.MachineReport.Attach(report);
            report.Status = enumDeviceWarnningStatus.GetInfo;
            report.DateGetInfo = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("MachineReport");
        }
        #endregion

        #region 设备启停
        private object getMachineStartEndData(string start, string end)
        {
            object model = null;
            string strStartDate = Common.getStartDate(start), strEndDate = Common.getEndDate(end);
            DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);

            model = (from item in db.MachineStartEnd
                     where item.DateEnd >= startDate && item.DateEnd <= endDate
                     orderby item.ID descending
                     select item).Take(Constants.ITEM_COUNT_EACH_PAGE_DEFAULT);

            ViewData["startDate"] = strStartDate;
            ViewData["endDate"] = strEndDate;
            return model;
        }

        [UserRoleAuthentication(Roles = "启停管理员")]
        public ActionResult MachineStartEnd()
        {
            string strStartDate = Request["startDate"], strEndDate = Request["endDate"];
            object model = this.getMachineStartEndData(strStartDate, strEndDate);

            return View(model);
        }

        [UserRoleAuthentication(Roles = "启停管理员")]
        public ActionResult MachineStartEndContent()
        {
            object model = this.getMachineStartEndData(null, null);

            return PartialView("_StartEndPartial", model);
        }
        #endregion
    }
}

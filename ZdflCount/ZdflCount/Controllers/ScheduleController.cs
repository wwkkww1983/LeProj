using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZdflCount.Models;
using ZdflCount.App_Start;

namespace ZdflCount.Controllers
{
    [App_Start.UserLoginAuthentication]
    public class ScheduleController : Controller
    {
        private const string PAGE_SEND_CONTENT = "PAGESENDCONTENT", PRE_DOWN_INFO_MACHINE = "PREDOWNMACIHNE", PRE_DOWN_INFO = "PREDOWNINFO";
        private const string PRE_INFO_TYPE_CREATE = "INFOTYPECREATE", PRE_INFO_TYPE_CLOSE = "INFOTYPECLOSE", PRE_INFO_TYPE_DISCARD = "INFOTYPEDISCARD";
        private DbTableDbContext db = new DbTableDbContext();
        private ScheduleOrder modelSchOrder = new ScheduleOrder();
        private RoomController roomControl = new RoomController();
        private static ServiceStack.Redis.IRedisClient client = Constants.RedisClient;

        #region 列表页

        [UserRoleAuthentication(Roles = "施工单管理员,施工单查看,施工单创建,施工单修改,施工单下派,施工单关闭,施工单报废")]
        public ActionResult Index()
        {
            string machine = Request["machine"], order = Request["order"], room = Request["room"],
                   strStartDate = Request["startDate"], strEndDate = Request["endDate"];
            //设备列表 显示
            int[] rooms = roomControl.GetRoomsForUser(Convert.ToInt32(Session["UserID"]));
            List<SelectListItem> machineSelectList = new List<SelectListItem>();
            IEnumerable<Machines> machineList = from item in db.Machines
                                                where rooms.Contains(item.RoomID)
                                                select item;
            machineSelectList.Add(new SelectListItem() { Text = "请选择车间设备", Value = "", Selected = true });
            foreach (Machines item in machineList)
            {
                machineSelectList.Add(new SelectListItem() { Text = item.Name + "(" + item.RoomName + ")", Value = item.ID.ToString(), Selected = false });
            }
            ViewData["machines"] = machineSelectList;
            //整理查询条件（起止时间）
            DateTime dateStart, dateEnd;
            if (strStartDate == null || strStartDate == string.Empty)
            {
                strStartDate = DateTime.Now.AddDays(-1 * (DateTime.Now.Day - 1)).ToString(App_Start.Constants.DATE_FORMAT);
            }
            if (strEndDate == null || strEndDate == string.Empty)
            {
                strEndDate = DateTime.Now.ToString(App_Start.Constants.DATE_FORMAT);
            }
            ViewData["startDate"] = strStartDate;
            ViewData["endDate"] = strEndDate;
            dateStart = DateTime.Parse(strStartDate);
            dateEnd = DateTime.Parse(strEndDate).AddDays(1);
            int intRoom = Convert.ToInt32(room), intMachine = Convert.ToInt32(machine);
            //执行查询
            var schedules = from item in db.Schedules
                            where rooms.Contains(item.RoomId) &&
                                item.DateLastUpdate >= dateStart && item.DateLastUpdate <= dateEnd &&
                                (order == null || item.OrderNumber == order) &&
                                (room == null || item.RoomId == intRoom) &&
                                (machine == null || item.MachineId == intMachine)
                            orderby item.Status ascending, item.DateLastUpdate descending
                            select item;

            if (schedules == null)
            {
                return HttpNotFound();
            }

            return View(schedules);
        }

        public ActionResult RoomsDownList()
        {
            int[] rooms = roomControl.GetRoomsForUser(Convert.ToInt32(Session["UserID"]));
            List<SelectListItem> roomList = new List<SelectListItem>();
            roomList.Add(new SelectListItem()
            {
                Text = "请选择车间",
                Value = "",
                Selected = true
            });
            IEnumerable<FactoryRoom> selfRooms = from item in db.FactoryRoom
                                             where rooms.Contains(item.RoomID)
                                             select item;
            foreach (FactoryRoom room in selfRooms)
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

        //
        // GET: /Schedule/
        [UserRoleAuthentication(Roles = "施工单管理员,施工单查看,施工单创建,施工单修改,施工单下派,施工单关闭,施工单报废")]
        public ActionResult OrderIndex(int id = 0)
        {
            int[] rooms = roomControl.GetRoomsForUser(Convert.ToInt32(Session["UserID"]));
            var schedules = from item in db.Schedules
                            where item.OrderId == id && rooms.Contains(item.RoomId)
                            orderby item.Status ascending, item.DateLastUpdate descending
                            select item;
            if (schedules == null)
            {
                return HttpNotFound();
            }
            Orders orderInfo = db.Orders.Find(id);
            ViewData["OrderId"] = id;
            ViewData["OrderNumber"] = orderInfo.OrderNumber;

            return View(schedules);
        }
        #endregion 

        #region 详情页

        //
        // GET: /Schedule/Details/5
        [UserRoleAuthentication(Roles = "施工单管理员,施工单查看,施工单创建,施工单修改,施工单下派,施工单关闭,施工单报废")]
        public ActionResult Details(int id = 0, enumErrorCode error = enumErrorCode.NONE)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            } 
            int userId = Convert.ToInt32(Session["UserID"]);
            if (!roomControl.CheckUserInRoom(userId, schedules.RoomId))
            {
                return RedirectToAction("Login", "Account");
            }
            ViewData["error"] = Constants.GetErrorString(error);
            return View(schedules);
        }
        #endregion

        #region 下派施工单
        [HttpPost]
        [UserRoleAuthentication(Roles = "施工单管理员,施工单下派")]
        public ActionResult Assign(int id = 0)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }
            int userId = Convert.ToInt32(Session["UserID"]);
            if (!roomControl.CheckUserInRoom(userId, schedules.RoomId))
            {
                return RedirectToAction("Login", "Account");
            }
            db.Schedules.Attach(schedules);
            schedules.DetailInfo = schedules.DetailInfo == null ? string.Empty : schedules.DetailInfo;
            schedules.NoticeInfo = schedules.NoticeInfo == null ? string.Empty : schedules.NoticeInfo;
            byte[] buff=null;
            App_Start.Coder.EncodeSchedule(schedules, out buff);
            enumErrorCode result = this.SendScheduleHandler(schedules.MachineId, buff, userId, PRE_INFO_TYPE_CREATE);
            if (result == enumErrorCode.HandlerSuccess)
            {
                schedules.Status = enumStatus.Assigned;
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = id, error = result });
        }
        #endregion 

        #region 新建
        //
        // GET: /Schedule/Create/1
        [UserRoleAuthentication(Roles = "施工单管理员,施工单创建")]
        public ActionResult Create(int id=0)
        {
            this.modelSchOrder.GetOrderById(id);
            this.modelSchOrder.GetMachineByUser(Convert.ToInt32(Session["UserID"]));
            ViewData["CreatorName"] = User.Identity.Name;

            return View(this.modelSchOrder);
        }

        //
        // POST: /Schedule/Create

        private string KeepLength_CutFillStart(int number, int length,char fillChar)
        {
            string result = number.ToString();
            int curLen = result.Length;
            if (curLen > length)
            {
                result = result.Substring(0, length);
            }
            for (int i = curLen; i < length; i++)
            {
                result = fillChar.ToString () + result;
            }
            return result;
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "施工单管理员,施工单创建")]
        public ActionResult Create(Schedules schedules, int machine, int temporary = 0)
        {
            if (!(ModelState.IsValid && machine > 0))
            {
                return Content("输入创建施工单数据无效");
            }
            int userId = Convert.ToInt32(Session["UserID"]);
            Orders orderInfo = db.Orders.Find(schedules.OrderId);
            if (temporary == 0 && orderInfo.ProductFreeCount < schedules.ProductCount)
            {
                this.modelSchOrder.Schedules = schedules;
                this.modelSchOrder.Orders = orderInfo;
                this.modelSchOrder.SelectMachine(machine);
                ViewData["error"] = "分派数量超过订单剩余总数";
                return View(this.modelSchOrder);
            }
            Machines currentMachine = this.db.Machines.Find(machine);
            schedules.MachineId = machine;
            schedules.MachineName = currentMachine.Name;
            schedules.RoomId = currentMachine.RoomID;
            if (!roomControl.CheckUserInRoom(userId, schedules.RoomId))
            {
                return RedirectToAction("Login", "Account");
            }
            schedules.OrderNumber = orderInfo.OrderNumber;
            schedules.ProductCode = orderInfo.ProductCode;
            schedules.ProductUnit = orderInfo.ProductUnit;
            schedules.ProductInfo = orderInfo.ProductInfo;
            schedules.RoomNumber = orderInfo.RoomNumber;
            schedules.DateCreate = DateTime.Now;
            schedules.DateLastUpdate = DateTime.Now;
            schedules.FinishCount = 0;
            schedules.CreatorID = userId;
            schedules.CreatorName = User.Identity.Name;
            schedules.LastUpdatePersonID = schedules.CreatorID;
            schedules.LastUpdatePersonName = User.Identity.Name;
            string strMaterial = string.Empty;
            foreach (string tempKey in Request.Form.AllKeys)
            {
                if (tempKey.StartsWith("cm") && Request.Form[tempKey].Contains("true"))
                    strMaterial += tempKey.Substring(2) + ";";
            }
            if (strMaterial.Length > 0)
            {
                schedules.MaterialInfo = strMaterial.Substring(0, strMaterial.Length - 1);
                string strMatDetail = string.Empty;
                this.modelSchOrder.GetOrderMaterial(schedules.MaterialInfo);
                foreach (KeyValuePair<int, string> tempItem in this.modelSchOrder.MaterialList)
                    strMatDetail += tempItem.Value + "；";
                schedules.MaterialDetail = strMatDetail;
            }
            string tempNumberStr = KeepLength_CutFillStart(orderInfo.AssignedCount + 1, 4, '0');
            schedules.Number = string.Format("{0}-{1}", schedules.OrderNumber, tempNumberStr);
            schedules.Status = temporary > 0 ? enumStatus.Temporary : enumStatus.Unhandle;
            db.Schedules.Add(schedules);

            if (temporary == 0)
            {
                orderInfo.ProductFreeCount -= schedules.ProductCount;
                orderInfo.ProductAssignedCount += schedules.ProductCount;
                orderInfo.AssignedCount += 1;
                db.Entry(orderInfo).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { id = schedules.OrderId });
        }
        #endregion 

        #region 编辑
        //
        // GET: /Schedule/Edit/5
        [UserRoleAuthentication(Roles = "施工单管理员,施工单修改")]
        public ActionResult Edit(int id = 0)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }
            if (!roomControl.CheckUserInRoom(userId, schedules.RoomId))
            {
                return RedirectToAction("Login", "Account");
            }         
            this.modelSchOrder.Schedules = schedules;
            this.modelSchOrder.GetOrderById(schedules.OrderId);
            this.modelSchOrder.GetMachineByUser(userId);
            this.modelSchOrder.SelectMachine(schedules.MachineId);

            return View(this.modelSchOrder);
        }

        //
        // POST: /Schedule/Edit/5

        [HttpPost]
        [UserRoleAuthentication(Roles = "施工单管理员,施工单修改")]
        public ActionResult Edit(Schedules schedules, int machine)
        {
            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                db.Schedules.Attach(schedules);
                var tempEntity = db.Entry(schedules);

                Machines currentMachine = this.db.Machines.FirstOrDefault(item => item.ID == machine);
                if (!roomControl.CheckUserInRoom(userId, currentMachine.RoomID))
                {
                    return RedirectToAction("Login", "Account");
                }   
                schedules.MachineId = machine;
                schedules.MachineName = currentMachine.Name;
                schedules.RoomId = currentMachine.RoomID;
                schedules.LastUpdatePersonID = userId;
                schedules.LastUpdatePersonName = User.Identity.Name;
                schedules.DateLastUpdate = DateTime.Now;

                tempEntity.Property(item => item.FinishCount).IsModified = true;
                tempEntity.Property(item => item.DetailInfo).IsModified = true;
                tempEntity.Property(item => item.NoticeInfo).IsModified = true;
                tempEntity.Property(item => item.LastUpdatePersonID).IsModified = true;
                tempEntity.Property(item => item.LastUpdatePersonName).IsModified = true;
                tempEntity.Property(item => item.DateLastUpdate).IsModified = true;
                tempEntity.Property(item => item.MachineId).IsModified = true;
                tempEntity.Property(item => item.MachineName).IsModified = true;
                tempEntity.Property(item => item.RoomId).IsModified = true;

                if (schedules.ProductCount > 0)
                {
                    //临时保存情况下，会应用施工单的数量
                    tempEntity.Property(item => item.ProductCount).IsModified = true;
                    tempEntity.Property(item => item.Status).IsModified = true;
                    schedules.Status = enumStatus.Unhandle;

                    Orders orderInfo = db.Orders.Find(schedules.OrderId);
                    orderInfo.ProductFreeCount -= schedules.ProductCount;
                    orderInfo.ProductAssignedCount += schedules.ProductCount;
                    orderInfo.AssignedCount += 1;

                    db.Entry(orderInfo).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Details", new { id = schedules.ID });
            }
            return Content("输入修改施工单数据无效");
        }
        #endregion

        #region 关闭
        //
        // GET: /Schedule/Delete/5
        [UserRoleAuthentication(Roles = "施工单管理员,施工单关闭")]
        public ActionResult Delete(int id = 0, enumErrorCode error = enumErrorCode.NONE)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }
            int userId = Convert.ToInt32(Session["UserID"]);
            if (!roomControl.CheckUserInRoom(userId, schedules.RoomId))
            {
                return RedirectToAction("Login", "Account");
            }
            ViewData["error"] = Constants.GetErrorString(error);

            return View(schedules);
        }

        //
        // POST: /Schedule/Delete/5

        [HttpPost, ActionName("Delete")]
        [UserRoleAuthentication(Roles = "施工单管理员,施工单关闭")]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedules schedules = db.Schedules.Find(id);
            //db.Schedules.Remove(schedules);
            int userId = Convert.ToInt32(Session["UserID"]);
            if (!roomControl.CheckUserInRoom(userId, schedules.RoomId))
            {
                return RedirectToAction("Login", "Account");
            }
            enumErrorCode result = enumErrorCode.HandlerSuccess;
            if (schedules.Status == enumStatus.Assigned || schedules.Status == enumStatus.Working)
            {
                byte[] buff = null;
                App_Start.Coder.EncodeScheHandler(schedules, enumCommandType.DOWN_SHEDULE_CLOSE_SEND, out buff);
                result = this.SendScheduleHandler(schedules.MachineId, buff, userId, PRE_INFO_TYPE_CLOSE);
            }
            if (result == enumErrorCode.HandlerSuccess)
            {
                db.Schedules.Attach(schedules);
                var tempEntity = db.Entry(schedules);
                schedules.Status = enumStatus.Closed;
                schedules.LastUpdatePersonID = userId;
                schedules.LastUpdatePersonName = User.Identity.Name;
                schedules.DateLastUpdate = DateTime.Now;

                Orders orderInfo = db.Orders.Find(schedules.OrderId);
                orderInfo.ProductFreeCount += (schedules.ProductCount - schedules.FinishCount);
                db.Entry(orderInfo).State = EntityState.Modified;
                
                db.SaveChanges();
            }

            return RedirectToAction("Delete", new { id = id, error = result });
        }
        #endregion

        #region 报废
        //
        // GET: /Schedule/Discard/5

        [UserRoleAuthentication(Roles = "施工单管理员,施工单报废")]
        public ActionResult Discard(int id = 0, enumErrorCode error = enumErrorCode.NONE)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }
            int userId = Convert.ToInt32(Session["UserID"]);
            if (!roomControl.CheckUserInRoom(userId, schedules.RoomId))
            {
                return RedirectToAction("Login", "Account");
            }
            ViewData["error"] = Constants.GetErrorString(error);

            return View(schedules);
        }

        //
        // POST: /Schedule/Delete/5

        [HttpPost, ActionName("Discard")]
        [UserRoleAuthentication(Roles = "施工单管理员,施工单报废")]
        public ActionResult DiscardConfirmed(int id)
        {
            Schedules schedules = db.Schedules.Find(id);
            int userId = Convert.ToInt32(Session["UserID"]);
            if (!roomControl.CheckUserInRoom(userId, schedules.RoomId))
            {
                return RedirectToAction("Login", "Account");
            }

            enumErrorCode result = enumErrorCode.HandlerSuccess;
            if (schedules.Status == enumStatus.Assigned || schedules.Status == enumStatus.Working)
            {
                byte[] buff = null;
                App_Start.Coder.EncodeScheHandler(schedules, enumCommandType.DOWN_SHEDULE_DISCARD_SEND, out buff);
                result = this.SendScheduleHandler(schedules.MachineId, buff, userId, PRE_INFO_TYPE_DISCARD);
            }
            if (result == enumErrorCode.HandlerSuccess)
            {
                db.Schedules.Attach(schedules);
                var tempEntity = db.Entry(schedules);
                schedules.Status = enumStatus.Discard;
                schedules.LastUpdatePersonID = userId;
                schedules.LastUpdatePersonName = User.Identity.Name;
                schedules.DateLastUpdate = DateTime.Now;

                Orders orderInfo = db.Orders.Find(schedules.OrderId);
                orderInfo.ProductFreeCount += schedules.ProductCount;
                db.Entry(orderInfo).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Discard", new { id = id, error = result });
        }
        #endregion

        #region 通信部分
        private enumErrorCode SendScheduleHandler(int machineId, byte[] buff, int userId, string infoType)
        {
            string strUserKey = string.Format("{0}-{1}", userId, DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
            client.EnqueueItemOnList(PAGE_SEND_CONTENT, strUserKey);
            client.Set<int>(PRE_DOWN_INFO_MACHINE + strUserKey, machineId);
            client.Set(PRE_DOWN_INFO + strUserKey, buff);
            client.Set<string>(infoType + machineId.ToString(), strUserKey);

            enumErrorCode result = App_Start.TcpProtocolClient.WaittingSendForResp(strUserKey);
            
            client.Remove(PRE_DOWN_INFO_MACHINE + strUserKey);
            client.Remove(PRE_DOWN_INFO + strUserKey);
            client.Remove(infoType + machineId.ToString());

            return result;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
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
        private DbTableDbContext db = new DbTableDbContext();
        private ScheduleOrder modelSchOrder = new ScheduleOrder();

        #region 列表页
        //
        // GET: /Schedule/
        [UserRoleAuthentication(Roles = "施工单管理员,施工单查看,施工单创建,施工单修改,施工单下派,施工单关闭,施工单报废")]
        public ActionResult Index(int id = 0)
        {
            var schedules = from item in db.Schedules
                            where item.OrderId == id
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
            db.Schedules.Attach(schedules);
            schedules.DetailInfo = schedules.DetailInfo == null ? string.Empty : schedules.DetailInfo;
            schedules.NoticeInfo = schedules.NoticeInfo == null ? string.Empty : schedules.NoticeInfo;
            if (schedules == null)
            {
                return HttpNotFound();
            }
            byte[] buff=null;
            App_Start.Coder.EncodeSchedule(schedules, out buff);
            enumErrorCode result = App_Start.TcpProtocolClient.SendScheduleInfo(schedules.MachineId, buff, Convert.ToInt32(Session["UserID"]));
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
            ViewData["CreatorName"] = User.Identity.Name;

            return View(this.modelSchOrder);
        }

        //
        // POST: /Schedule/Create

        [HttpPost]
        [UserRoleAuthentication(Roles = "施工单管理员,施工单创建")]
        public ActionResult Create(Schedules schedules,int machine,int temporary=0)
        {
            if (ModelState.IsValid)
            {
                Orders orderInfo = db.Orders.Find(schedules.OrderId);
                if (temporary == 0 && orderInfo.ProductFreeCount < schedules.ProductCount)
                {
                    this.modelSchOrder.Schedules = schedules;
                    this.modelSchOrder.Orders = orderInfo;
                    this.modelSchOrder.SelectMachine(machine);
                    ViewData["error"] = "分派数量超过订单剩余总数";
                    return View(this.modelSchOrder);
                }

                schedules.MachineId = machine;
                schedules.MachineName = this.modelSchOrder.MachineList.Find(item => int.Parse(item.Value) == machine).Text;
                schedules.OrderNumber = orderInfo.OrderNumber;
                schedules.DateCreate = DateTime.Now;
                schedules.DateLastUpdate = DateTime.Now;
                schedules.FinishCount = 0;
                schedules.CreatorID = Convert.ToInt32(Session["UserID"]);
                schedules.CreatorName = User.Identity.Name;
                schedules.LastUpdatePersonID = schedules.CreatorID;
                schedules.LastUpdatePersonName = User.Identity.Name;
                schedules.Number = string.Format("gd{0}{1}", schedules.CreatorID,DateTime.Now.ToString("yyyyMMddHHmmssffff"));
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

            return Content("输入创建施工单数据无效");
        }
        #endregion 

        #region 编辑
        //
        // GET: /Schedule/Edit/5
        [UserRoleAuthentication(Roles = "施工单管理员,施工单修改")]
        public ActionResult Edit(int id = 0)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }

            this.modelSchOrder.Schedules = schedules;
            this.modelSchOrder.GetOrderById(schedules.OrderId);
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
                //db.Entry(schedules).State = EntityState.Modified;
                db.Schedules.Attach(schedules);
                var tempEntity = db.Entry(schedules);
                tempEntity.Property(item => item.FinishCount).IsModified = true;
                tempEntity.Property(item => item.DetailInfo).IsModified = true;
                tempEntity.Property(item => item.NoticeInfo).IsModified = true;
                tempEntity.Property(item => item.LastUpdatePersonID).IsModified = true;
                tempEntity.Property(item => item.LastUpdatePersonName).IsModified = true;
                tempEntity.Property(item => item.DateLastUpdate).IsModified = true;
                tempEntity.Property(item => item.MachineId).IsModified = true;
                tempEntity.Property(item => item.MachineName).IsModified = true;

                schedules.MachineId = machine;
                schedules.MachineName = this.modelSchOrder.MachineList.Find(item => int.Parse(item.Value) == machine).Text;
                schedules.LastUpdatePersonID = Convert.ToInt32(Session["UserID"]);
                schedules.LastUpdatePersonName = User.Identity.Name;
                schedules.DateLastUpdate = DateTime.Now;

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

            enumErrorCode result = enumErrorCode.HandlerSuccess;
            if (schedules.Status == enumStatus.Assigned || schedules.Status == enumStatus.Working)
            {
                byte[] buff = null;
                App_Start.Coder.EncodeScheHandler(schedules, enumCommandType.DOWN_SHEDULE_CLOSE_SEND, out buff);
                result = App_Start.TcpProtocolClient.SendScheduleClose(schedules.MachineId, buff, Convert.ToInt32(Session["UserID"]));
            }
            if (result == enumErrorCode.HandlerSuccess)
            {
                db.Schedules.Attach(schedules);
                var tempEntity = db.Entry(schedules);
                schedules.Status = enumStatus.Closed;
                schedules.LastUpdatePersonID = Convert.ToInt32(Session["UserID"]);
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
            
            enumErrorCode result = enumErrorCode.HandlerSuccess;
            if (schedules.Status == enumStatus.Assigned || schedules.Status == enumStatus.Working)
            {
                byte[] buff = null;
                App_Start.Coder.EncodeScheHandler(schedules, enumCommandType.DOWN_SHEDULE_DISCARD_SEND, out buff);
                result = App_Start.TcpProtocolClient.SendScheduleDiscard(schedules.MachineId, buff, Convert.ToInt32(Session["UserID"]));
            }
            if (result == enumErrorCode.HandlerSuccess)
            {
                db.Schedules.Attach(schedules);
                var tempEntity = db.Entry(schedules);
                schedules.Status = enumStatus.Discard;
                schedules.LastUpdatePersonID = Convert.ToInt32(Session["UserID"]);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
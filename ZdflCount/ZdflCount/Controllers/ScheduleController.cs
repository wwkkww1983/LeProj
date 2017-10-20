using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZdflCount.Models;

namespace ZdflCount.Controllers
{
    //[App_Start.UserLoginAuthentication]
    public class ScheduleController : Controller
    {
        private ScheduleDbContext db = new ScheduleDbContext();
        private OrderDbContext dbOrder = new OrderDbContext();
        private ScheduleOrder modelSchOrder = new ScheduleOrder();

        #region 列表页
        //
        // GET: /Schedule/

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
            Orders orderInfo = dbOrder.Orders.Find(id);
            ViewData["OrderId"] = id;
            ViewData["OrderNumber"] = orderInfo.OrderNumber;

            return View(schedules);
        }
        #endregion 

        #region 详情页

        //
        // GET: /Schedule/Details/5

        public ActionResult Details(int id = 0)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }
            return View(schedules);
        }
        #endregion

        #region 下派施工单
        [HttpPost]
        public ActionResult Assign(int id = 0)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }
            return View("Details",  schedules );
        }
        #endregion 

        #region 新建
        //
        // GET: /Schedule/Create/1

        public ActionResult Create(int id=0)
        {
            this.modelSchOrder.GetOrderById(id);
            ViewData["CreatorName"] = User.Identity.Name;

            return View(this.modelSchOrder);
        }

        //
        // POST: /Schedule/Create

        [HttpPost]
        public ActionResult Create(Schedules schedules,int machine,int temporary=0)
        {
            if (ModelState.IsValid)
            {
                Orders orderInfo = dbOrder.Orders.Find(schedules.OrderId);
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
                db.SaveChanges();

                if (temporary == 0)
                {
                    orderInfo.ProductFreeCount -= schedules.ProductCount;
                    dbOrder.Entry(orderInfo).State = EntityState.Modified;
                    dbOrder.SaveChanges();
                }
                return RedirectToAction("Index", new { id = schedules.OrderId });
            }

            return Content("输入创建施工单数据无效");
        }
        #endregion 

        #region 编辑
        //
        // GET: /Schedule/Edit/5

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
                    tempEntity.Property(item => item.ProductCount).IsModified = true;
                    tempEntity.Property(item => item.Status).IsModified = true;
                    schedules.Status = enumStatus.Unhandle;

                    Orders orderInfo = dbOrder.Orders.Find(schedules.OrderId);
                    orderInfo.ProductFreeCount -= schedules.ProductCount;
                    dbOrder.Entry(orderInfo).State = EntityState.Modified;
                    dbOrder.SaveChanges();
                }

                db.SaveChanges();
                return RedirectToAction("Details", new { id = schedules.ID });
            }
            return Content("输入修改施工单数据无效");;
        }
        #endregion

        #region 停止
        //
        // GET: /Schedule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }
            return View(schedules);
        }

        //
        // POST: /Schedule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedules schedules = db.Schedules.Find(id);
            //db.Schedules.Remove(schedules);

            db.Schedules.Attach(schedules);
            var tempEntity = db.Entry(schedules);
            schedules.Status = enumStatus.Closed;
            schedules.LastUpdatePersonID = Convert.ToInt32(Session["UserID"]);
            schedules.LastUpdatePersonName = User.Identity.Name;

            db.SaveChanges();
            return RedirectToAction("Index", new { id = schedules.OrderId });
        }
        #endregion

        #region 报废
        //
        // GET: /Schedule/Discard/5

        public ActionResult Discard(int id = 0)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }
            return View(schedules);
        }

        //
        // POST: /Schedule/Delete/5

        [HttpPost, ActionName("Discard")]
        public ActionResult DiscardConfirmed(int id)
        {
            Schedules schedules = db.Schedules.Find(id);
            //db.Schedules.Remove(schedules);

            db.Schedules.Attach(schedules);
            var tempEntity = db.Entry(schedules);
            schedules.Status = enumStatus.Discard;
            schedules.LastUpdatePersonID = Convert.ToInt32(Session["UserID"]);
            schedules.LastUpdatePersonName = User.Identity.Name;

            db.SaveChanges();

            Orders orderInfo = dbOrder.Orders.Find(schedules.OrderId);
            orderInfo.ProductFreeCount += schedules.ProductCount;
            dbOrder.Entry(orderInfo).State = EntityState.Modified;
            dbOrder.SaveChanges();

            return RedirectToAction("Index", new { id = schedules.OrderId });
        }
        #endregion 

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
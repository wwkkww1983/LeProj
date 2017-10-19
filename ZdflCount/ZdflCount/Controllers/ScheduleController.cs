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

        //
        // GET: /Schedule/

        public ActionResult Index(int id = 0)
        {
            var schedules = from item in db.Schedules
                            where item.OrderId == id && item.Status != enumOrderStatus.Deleted
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
        public ActionResult Create(Schedules schedules)
        {
            if (ModelState.IsValid)
            {
                Orders orderInfo = dbOrder.Orders.Find(schedules.OrderId);
                if (orderInfo.ProductFreeCount < schedules.ProductCount)
                {
                    this.modelSchOrder.Schedules = schedules;
                    this.modelSchOrder.Orders = orderInfo;
                    ViewData["error"] = "分派数量超过订单剩余总数";
                    return View(this.modelSchOrder);
                }

                schedules.DateCreate = DateTime.Now;
                schedules.DateLastUpdate = DateTime.Now;
                schedules.FinishCount = 0;
                schedules.CreatorID = Convert.ToInt32(Session["UserID"]);
                schedules.CreatorName = User.Identity.Name;
                schedules.LastUpdatePersonID = schedules.CreatorID;
                schedules.LastUpdatePersonName = User.Identity.Name;
                schedules.Number = string.Format("gd{0}{1}", schedules.CreatorID,DateTime.Now.ToString("yyyyMMddHHmmssffff"));
                db.Schedules.Add(schedules);
                db.SaveChanges();

                orderInfo.ProductFreeCount -= schedules.ProductCount;
                dbOrder.Entry(orderInfo).State = EntityState.Modified;
                dbOrder.SaveChanges();
                return RedirectToAction("Index", new { id = schedules.OrderId });
            }

            return Content("输入数据无效");
        }

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

            return View(this.modelSchOrder);
        }

        //
        // POST: /Schedule/Edit/5

        [HttpPost]
        public ActionResult Edit(Schedules schedules)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(schedules).State = EntityState.Modified;
                db.Schedules.Attach(schedules);
                var tempEntity = db.Entry(schedules);
                tempEntity.Property(item => item.ProductCount).IsModified = true;
                tempEntity.Property(item => item.FinishCount).IsModified = true;
                tempEntity.Property(item => item.DetailInfo).IsModified = true;
                tempEntity.Property(item => item.NoticeInfo).IsModified = true;
                tempEntity.Property(item => item.LastUpdatePersonID).IsModified = true;
                tempEntity.Property(item => item.LastUpdatePersonName).IsModified = true;
                tempEntity.Property(item => item.DateLastUpdate).IsModified = true;

                schedules.LastUpdatePersonID = Convert.ToInt32(Session["UserID"]);
                schedules.LastUpdatePersonName = User.Identity.Name;
                schedules.DateLastUpdate = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Details", new { id = schedules.ID });
            }
            return View(schedules);
        }

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
            schedules.Status = enumOrderStatus.Deleted;
            schedules.LastUpdatePersonID = Convert.ToInt32(Session["UserID"]);
            schedules.LastUpdatePersonName = User.Identity.Name;

            db.SaveChanges();
            return RedirectToAction("Index", new { id = schedules.OrderId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
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
    public class ScheduleController : Controller
    {
        private ScheduleDbContext db = new ScheduleDbContext();
        private ScheduleOrder modelSchOrder = new ScheduleOrder();

        //
        // GET: /Schedule/

        public ActionResult Index(int id)
        {
            //string strSql = string.Format("selet * from Schedules where orderId={0}",orderId);
            //IEnumerable<Schedules> scheduleList = db.Schedules.SqlQuery(strSql);
            //if (scheduleList == null)
            //{
            //    return HttpNotFound();
            //}

            //return View(scheduleList);
            var schedules = from item in db.Schedules
                            where item.OrderId == id
                            select item;
            if (schedules == null)
            {
                return HttpNotFound();
            }

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

            return View(this.modelSchOrder);
        }

        //
        // POST: /Schedule/Create

        [HttpPost]
        public ActionResult Create(Schedules schedules)
        {
            if (ModelState.IsValid)
            {
                schedules.DateCreate = DateTime.Now;
                schedules.FinishCount = 0;
                schedules.CreatorID = Convert.ToInt32(Session["UserID"]);
                schedules.CreatorName = User.Identity.Name;
                schedules.LastUpdatePersonID = schedules.CreatorID;
                schedules.LastUpdatePersonName = User.Identity.Name;
                db.Schedules.Add(schedules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedules);
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
                db.Entry(schedules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            db.Schedules.Remove(schedules);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
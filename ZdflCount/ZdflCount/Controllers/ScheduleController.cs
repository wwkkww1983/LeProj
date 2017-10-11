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

        //
        // GET: /Schedule/

        public ActionResult Index(int orderId)
        {
            Schedules schedules = db.Schedules.Find(id);
            if (schedules == null)
            {
                return HttpNotFound();
            }

            return View(db.schedules.ToList());
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
        // GET: /Schedule/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Schedule/Create

        [HttpPost]
        public ActionResult Create(Schedules schedules)
        {
            if (ModelState.IsValid)
            {
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
            return View(schedules);
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
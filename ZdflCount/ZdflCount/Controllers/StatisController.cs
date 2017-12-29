using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZdflCount.Models;

namespace ZdflCount.Controllers
{
    public class StatisController : Controller
    {

        private DbViewDbContext db = new DbViewDbContext(); 

        /// <summary>
        /// 员工产量表
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffProduction()
        {
            object model = null;
            string strStartDate = Request["startDate"], strEndDate = Request["endDate"];
            if (strStartDate != null && strEndDate != null)
            {
                //DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);
                //model = from item in db.StaffProduction
                //        where item.DateOut >= startDate && item.DateOut <= endDate
                //        select item;
            }
            else
            {
                model = db.StaffProduction.OrderByDescending(item => item.Finish).Take(50);
            }
            return View(model);
        }

        /// <summary>
        /// 机台产量表
        /// </summary>
        /// <returns></returns>
        public ActionResult MachineProduction()
        {
            object model = null;
            string strStartDate = Request["startDate"], strEndDate = Request["endDate"];
            if (strStartDate != null && strEndDate != null)
            {
                //DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);
                //model = from item in db.StaffProduction
                //        where item.DateOut >= startDate && item.DateOut <= endDate
                //        select item;
            }
            else
            {
                model = db.MachineProduction.OrderByDescending(item => item.Finish).Take(50);
            }
            return View(model);
        }

        /// <summary>
        /// 机台工作时间表
        /// </summary>
        /// <returns></returns>
        public ActionResult MachineWorking()
        {
            object model = null;
            string strStartDate = Request["startDate"], strEndDate = Request["endDate"];
            if (strStartDate != null && strEndDate != null)
            {
                //DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);
                //model = from item in db.StaffProduction
                //        where item.DateOut >= startDate && item.DateOut <= endDate
                //        select item;
            }
            else
            {
                model = db.MachineWorking.OrderByDescending(item => item.MachineNumber).Take(50);
            }
            return View(model);
        }
    }
}

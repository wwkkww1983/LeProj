using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZdflCount.Models;
using System.Data.Entity;


namespace ZdflCount.Controllers
{
    public class StaffController : Controller
    {
        private DbTableDbContext db = new DbTableDbContext();

        #region 列表
        public ActionResult Index()
        {
            var itemList = from item in db.StaffInfo
                           where item.Status != enumStaffStatus.Deleted
                           orderby item.Status
                           select item;
            return View(itemList);
        }
        #endregion

        #region 详情
        public ActionResult Detail(int id)
        {
            StaffInfo staffInfo = db.StaffInfo.Find(id);
            if (staffInfo == null)
            {
                return HttpNotFound();
            }
            return View(staffInfo);
        }

        #endregion

        #region 新建
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StaffInfo staffInfo)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<StaffInfo> tempStaff = from item in db.StaffInfo
                                                   where item.Number == staffInfo.Number || item.Phone==staffInfo.Phone
                                                   select item;
                if (tempStaff.Count() > 0)
                {
                    ViewData["error"] = "【工号】 或【手机号码】重复";
                    return View(staffInfo);
                }
                //存入数据库
                db.StaffInfo.Add(staffInfo); 
                int a = db.SaveChanges();
            }
            return View("Detail",staffInfo);
        }

        #endregion

        #region 修改
        public ActionResult Edit(int id = 0)
        {
            StaffInfo staffInfo = db.StaffInfo.Find(id);
            if (staffInfo == null)
            {
                return HttpNotFound();
            }
            return View(staffInfo);
        }

        [HttpPost]
        public ActionResult Edit(StaffInfo staff)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(staff).State = EntityState.Modified;
                db.StaffInfo.Attach(staff);
                var tempEntity = db.Entry(staff);
                tempEntity.Property(item => item.Phone).IsModified = true;
                tempEntity.Property(item => item.telPhone).IsModified = true;
                tempEntity.Property(item => item.Address).IsModified = true;
                tempEntity.Property(item => item.HomeAddress).IsModified = true;
                tempEntity.Property(item => item.BirthDate).IsModified = true;
                tempEntity.Property(item => item.JoinInDate).IsModified = true;
                tempEntity.Property(item => item.EmergencyName).IsModified = true;
                tempEntity.Property(item => item.EmergencyPhone).IsModified = true;
                tempEntity.Property(item => item.Remarks).IsModified = true;
                
                db.SaveChanges();
                return RedirectToAction("Detail", staff);
            }
            return Content("输入修改施工单数据无效"); ;
        }

        #endregion

        #region 删除
        public ActionResult Delete(int id = 0)
        {
            StaffInfo staffInfo = db.StaffInfo.Find(id);
            if (staffInfo == null)
            {
                return HttpNotFound();
            }
            return View(staffInfo);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffInfo staffInfo = db.StaffInfo.Find(id);
            //db.Schedules.Remove(schedules);

            db.StaffInfo.Attach(staffInfo);
            var tempEntity = db.Entry(staffInfo);
            staffInfo.Status = enumStaffStatus.Deleted;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}

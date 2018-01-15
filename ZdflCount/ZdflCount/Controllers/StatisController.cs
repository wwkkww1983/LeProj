using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZdflCount.Models;
using ZdflCount.App_Start;

namespace ZdflCount.Controllers
{
    public class StatisController : Controller
    {
        private DbSqlDbContext db = new DbSqlDbContext();
        private RoomController roomControl = new RoomController();

        /// <summary>
        /// 员工产量表
        /// </summary>
        /// <returns></returns>
        [App_Start.UserRoleAuthentication(Roles = "生产统计数据查看")]
        public ActionResult StaffProduction()
        {
            int[] rooms = roomControl.GetRoomsForUser(Convert.ToInt32(Session["UserID"]));
            string strStartDate = Common.getStartDate(Request["startDate"]), strEndDate = Common.getEndDate(Request["endDate"]);
            DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);
            string strSql = @"SELECT RoomName, StaffName, OrderNumber, SUM(FinishCount) AS Finish, SUM(ValidCount) AS Valid, SUM(ExceptionCount) AS Exception
FROM  dbo.StatisticInfoes
WHERE DateOut >= {0} AND DateOut <= {1}
GROUP BY RoomName, StaffName, OrderNumber";
            object model = db.Database.SqlQuery<VS_StaffProduction>(strSql, startDate, endDate); ;

            ViewData["startDate"] = strStartDate;
            ViewData["endDate"] = strEndDate;
            return View(model);
        }

        /// <summary>
        /// 机台产量表
        /// </summary>
        /// <returns></returns>
        [App_Start.UserRoleAuthentication(Roles = "生产统计数据查看")]
        public ActionResult MachineProduction()
        {
            int[] rooms = roomControl.GetRoomsForUser(Convert.ToInt32(Session["UserID"]));
            string strStartDate = Common.getStartDate(Request["startDate"]), strEndDate = Common.getEndDate(Request["endDate"]);
            DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);
            string strRooms = this.GetRoomsStr(rooms);
            string strSql = @"SELECT RoomName, MachineName, OrderNumber, SUM(FinishCount) AS Finish, SUM(ValidCount) AS Valid, SUM(ExceptionCount) AS Exception
FROM  dbo.StatisticInfoes
WHERE DateOut >= {0} AND DateOut <= {1} AND RoomID in (" + strRooms + ") GROUP BY RoomName, MachineName, OrderNumber";
            object model = db.Database.SqlQuery<VS_MachineProduction>(strSql, startDate, endDate); ;

            ViewData["startDate"] = strStartDate;
            ViewData["endDate"] = strEndDate;
            return View(model);
        }

        /// <summary>
        /// 订单产量表
        /// </summary>
        /// <returns></returns>
        [App_Start.UserRoleAuthentication(Roles = "生产统计数据查看")]
        public ActionResult OrderProduction()
        {
            int[] rooms = roomControl.GetRoomsForUser(Convert.ToInt32(Session["UserID"]));
            string strStartDate = Common.getStartDate(Request["startDate"]), strEndDate = Common.getEndDate(Request["endDate"]);
            DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);
            string strRooms = this.GetRoomsStr(rooms);
            string strSql = @"SELECT t1.*,t2.ProductCount, (t2.ProductCount-t1.Valid) AS ProductLeft
FROM (SELECT RoomName, OrderNumber, SUM(FinishCount) AS Finish, SUM(ValidCount) AS Valid, SUM(ExceptionCount) AS Exception
FROM  dbo.StatisticInfoes
WHERE DateOut >= {0} AND DateOut <= {1} AND RoomID in (" +
                                                         strRooms 
                                                         +
@") GROUP BY OrderNumber,RoomName) AS t1
LEFT JOIN dbo.Orders AS t2 on t1.OrderNumber = t2.OrderNumber";
            object model = db.Database.SqlQuery<VS_OrderProduction>(strSql, startDate, endDate); ;

            ViewData["startDate"] = strStartDate;
            ViewData["endDate"] = strEndDate;
            return View(model);
        }

        /// <summary>
        /// 机台工作时间表
        /// </summary>
        /// <returns></returns>
        [App_Start.UserRoleAuthentication(Roles = "生产统计数据查看")]
        public ActionResult MachineWorking()
        {
            int[] rooms = roomControl.GetRoomsForUser(Convert.ToInt32(Session["UserID"]));
            string strStartDate = Common.getStartDate(Request["startDate"]), strEndDate = Common.getEndDate(Request["endDate"]);
            DateTime startDate = DateTime.Parse(strStartDate), endDate = DateTime.Parse(strEndDate).AddDays(1);
            string strRooms = this.GetRoomsStr(rooms);
            string strSql = @"SELECT RoomNumber, MachineName, SUM(DATEDIFF(MI, DateStart, DateEnd)) AS WorkingMinute,COUNT(1) AS WorkingCount
FROM  dbo.MachineStartEnds
WHERE DateEnd >= {0} AND DateEnd <= {1} AND DateStart is not NULL AND RoomID in (" + strRooms + ") GROUP BY RoomNumber, MachineName";
            object model = db.Database.SqlQuery<VS_MachineWorking>(strSql, startDate, endDate); ;


            ViewData["startDate"] = strStartDate;
            ViewData["endDate"] = strEndDate;
            return View(model);
        }


        private string GetRoomsStr(int[] roomId)
        {
            string strRooms = null;
            switch (roomId.Length)
            {
                case 0: strRooms = string.Empty; break;
                case 1: strRooms = roomId[0].ToString(); break;
                default:
                    foreach (int item in roomId)
                    {
                        strRooms += (item + ",");
                    }
                    strRooms = strRooms.Substring(0, strRooms.Length - 1);
                    break;
            }
            return strRooms;
        }
    }
}

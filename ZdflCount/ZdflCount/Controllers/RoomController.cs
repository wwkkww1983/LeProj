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
    public class RoomController : Controller
    {
        private DbTableDbContext db = new DbTableDbContext();
   
        public bool CheckUserInRoom(int userId, int roomId)
        {
            var userRole = from item in db.UsersInRooms
                           where item.UserId == userId && item.RoomId == roomId
                           select item;
            return userRole.Count() > 0;
        }

        public int[] GetRoomsForUser(int userId)
        {
            IEnumerable<UsersInRooms> rooms = from item in db.UsersInRooms
                                              where item.UserId == userId
                                              select item;
            int[] roomArray = new int[rooms.Count()];
            int idx = 0;
            foreach (UsersInRooms item in rooms)
            {
                roomArray[idx++] = item.RoomId;
            }
            return roomArray;
        }

        public List<SelectListItem> GetUserValidMachines(int userId)
        {
            int[] rooms = this.GetRoomsForUser(userId);
            IEnumerable<Machines> allMachines = from item in db.Machines
                                                where item.Status == enumMachineStatus.Normal && rooms.Contains(item.RoomID)
                                                select item;
            List<SelectListItem> machineList = new List<SelectListItem>(allMachines.Count<Machines>());
            foreach (Machines item in allMachines)
            {
                machineList.Add(new SelectListItem { Text = item.RoomName + " - " + item.Number, Value = item.ID.ToString() });
            }
            return machineList;
        }
    }
}
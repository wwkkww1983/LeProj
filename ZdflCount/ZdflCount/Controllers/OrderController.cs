using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using ZdflCount.Models;
using ZdflCount.App_Start;

namespace ZdflCount.Controllers
{
    [App_Start.UserLoginAuthentication]
    public class OrderController : Controller
    {
        [DataContract]
        public class OrderSystemDetail
        {
            /// <summary>
            /// 物料编码
            /// </summary>
            [DataMember]
            public string Childitemcode { get; set; }
            /// <summary>
            /// 物料详情
            /// </summary>
            [DataMember]
            public string Childitemname { get; set; }
            /// <summary>
            /// 物料单位
            /// </summary>
            [DataMember]
            public string ChildnvntryUom { get; set; }
            /// <summary>
            /// 产品单位
            /// </summary>
            [DataMember]
            public string FatherInvntryUom { get; set; }
            /// <summary>
            /// 产品编码
            /// </summary>
            [DataMember]
            public string Fatheritemcode { get; set; }
            /// <summary>
            /// 产品描述
            /// </summary>
            [DataMember]
            public string Fatheritemname { get; set; }
            /// <summary>
            /// 订单编号
            /// </summary>
            [DataMember]
            public string docnum { get; set; }
            /// <summary>
            /// 订单交期
            /// </summary>
            [DataMember]
            public DateTime duedate { get; set; }
            /// <summary>
            /// 部门编码
            /// </summary>
            [DataMember]
            public string ocrcode { get; set; }
            public int roomId { get; set; }
            public string roomName { get; set; }
            /// <summary>
            /// 计划生产数量
            /// </summary>
            [DataMember]
            public int plannedqty { get; set; }
            /// <summary>
            /// 详细说明
            /// </summary>
            [DataMember]
            public string U_htyq { get; set; }
            /// <summary>
            /// 包装方式
            /// </summary>
            [DataMember]
            public string U_WL_PACK { get; set; }

        }

        private const string ORDER_URL = "http://192.168.0.231:8096/Onlinewebxz.asmx/getOWORList";
        private const string ORDER_CODE_PARAM = "docnum";
        private const string ORDER_RIGHT_ERROR = "订单编码不存在 【或者】 没有权限查看", ORDER_EXISTS_ERROR = "该订单已经加载";
        private DbTableDbContext db = new DbTableDbContext();
        private RoomController roomControl = new RoomController();

        #region 订单列表
        //
        // GET: /Order/

        public ActionResult Index()
        {
            int[] rooms = roomControl.GetRoomsForUser(Convert.ToInt32(Session["UserID"]));
            using (var t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                var orders = (from item in db.Orders
                             where rooms.Contains(item.RoomId)
                             orderby item.Status ascending, item.OrderId descending
                             select item).ToList();

                return View(orders);
            }
        }
        #endregion 

        #region 详情
        //
        // GET: /Order/Details/5

        public ActionResult Detail(int id, enumErrorCode error = enumErrorCode.NONE)
        {
            Orders order = null;
            using (var t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                int userId = Convert.ToInt32(Session["UserID"]);
                if (!roomControl.CheckUserInRoom(userId, order.RoomId))
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            ScheduleOrder tempSchedule = new ScheduleOrder();
            tempSchedule.GetOrderMaterial(order.MaterialInfo);
            string[] materialArray = tempSchedule.MaterialList.Values.ToArray();
            ViewData["materials"] = materialArray;
            ViewData["error"] = Constants.GetErrorString(error);

            return View(order);
        }
        #endregion 

        #region 加载新订单
        //
        // GET: /Order/Create
        [UserRoleAuthentication(Roles = "订单加载")]
        public ActionResult Create()
        {
            return View();
        }

        private bool CheckOrder(string order,out List<OrderSystemDetail> orderDetails)
        {
            bool result = false;
            orderDetails = null;
            try
            {
                //获取订单数据
                Dictionary<string, string> orderParam = new Dictionary<string, string>();
                orderParam.Add(ORDER_CODE_PARAM, order);
                string strOrderInfo = WebInfo.PostPageInfo(ORDER_URL, orderParam);
                //订单解码
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<OrderSystemDetail>));
                MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(strOrderInfo));
                orderDetails = (List<OrderSystemDetail>)serializer.ReadObject(stream);
                if (orderDetails.Count < 1) return result;
                //校验车间权限
                OrderSystemDetail orderItem = orderDetails[0];
                FactoryRoom room = db.FactoryRoom.FirstOrDefault(item => item.RoomNumber == orderItem.ocrcode);
                if (room == null) return result;
                int userId = Convert.ToInt32(Session["UserID"]);
                UsersInRooms tempUserRoom = db.UsersInRooms.FirstOrDefault(item => item.RoomId == room.RoomID && item.UserId == userId);
                if (tempUserRoom == null) return result;

                orderItem.roomName = room.RoomName;
                orderItem.roomId = room.RoomID;

                result = true;
            }
            catch { }

            return result;
        }

        [UserRoleAuthentication(Roles = "订单加载")]
        public JsonResult GetOrderInfo(string order)
        {
            JsonResult result = new JsonResult();
            using (var t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                Orders tempCurrentOrder = db.Orders.FirstOrDefault(item => item.OrderNumber == order);
                if (tempCurrentOrder != null)
                {
                    result.Data = new { error = ORDER_EXISTS_ERROR };
                    return result;
                }
            }
            result.Data = new { error = ORDER_RIGHT_ERROR };
            //获取订单数据
            List<OrderSystemDetail> orderDetails;
            if (!this.CheckOrder(order, out orderDetails)) return result;
            //物料保存
            List<string> matList = new List<string>();
            bool insertFlag = false;
            foreach (OrderSystemDetail item in orderDetails)
            {
                matList.Add(string.Format(App_Start.Constants.MACHINE_MATERIAL_STRUCTURE, item.Childitemcode, item.ChildnvntryUom, item.Childitemname));
                Materials tempMat = db.Materials.FirstOrDefault(m => m.Code == item.Childitemcode);
                if (tempMat == null)
                {
                    db.Materials.Add(new Materials()
                    {
                        Code = item.Childitemcode,
                        Unit = item.ChildnvntryUom,
                        DetailInfo = item.Childitemname
                    });
                    insertFlag = true;
                }
            }
            if (insertFlag) db.SaveChanges();
            //返回前台显示
            OrderSystemDetail orderItem = orderDetails[0];
            result.Data = new
            {
                orderCode = orderItem.docnum,
                roomName = orderItem.roomName,
                roomNumber = orderItem.ocrcode,
                dateFinish = orderItem.duedate.ToString("yyyy年MM月dd日"),
                productCode = orderItem.Fatheritemcode,
                productUnit = orderItem.FatherInvntryUom,
                productInfo = orderItem.Fatheritemname,
                materialInfo = matList.ToArray(),
                detail = orderItem.U_WL_PACK == null ? "" : orderItem.U_WL_PACK,
                notice = orderItem.U_htyq
            };

            return result;
        }

        //
        // POST: /Order/Create

        [HttpPost]
        [UserRoleAuthentication(Roles = "订单加载")]
        public ActionResult Create(Orders order)
        {
            List<OrderSystemDetail> orderDetails;
            if (!this.CheckOrder(order.OrderNumber, out orderDetails))
            {
                ViewData["error"] = ORDER_EXISTS_ERROR;
                return View();
            }
            OrderSystemDetail orderItem = orderDetails[0];
            order.DateAssign = DateTime.Now;
            order.DateNeedFinish = orderItem.duedate;
            order.RoomId = orderItem.roomId;
            order.RoomName = orderItem.roomName;
            order.RoomNumber = orderItem.ocrcode;
            order.Status = enumStatus.Unhandle;
            order.ProductUnit = orderItem.FatherInvntryUom;
            order.ProductCode = orderItem.Fatheritemcode;
            order.ProductInfo = orderItem.Fatheritemname;
            order.OrderNumber = orderItem.docnum;
            order.ProductFreeCount = orderItem.plannedqty;
            order.ProductCount = orderItem.plannedqty;
            //物料信息
            List<string> mateCodes = new List<string> ();
            foreach (OrderSystemDetail item in orderDetails)
            {
                mateCodes.Add(item.Childitemcode);
            }
            IEnumerable<Materials> materialList = from item in db.Materials
                                                  where mateCodes.Contains(item.Code)
                                                  select item;
            string strIds = string.Empty;
            foreach (Materials item in materialList)
            {
                strIds += item.ID + ";";
            }
            order.MaterialInfo = strIds.Substring(0, strIds.Length - 1);
            
            db.Orders.Add(order);
            db.SaveChanges();            

            return RedirectToAction("Detail", new { id = order.OrderId, error = enumErrorCode.HandlerSuccess });
        }
        #endregion

        //
        // GET: /Order/Edit/5

        public ActionResult Edit(int id)
        {
            using (var t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                Orders order = db.Orders.Find(id);

                ScheduleOrder tempSchedule = new ScheduleOrder();
                tempSchedule.GetOrderMaterial(order.MaterialInfo);
                string[] materialArray = tempSchedule.MaterialList.Values.ToArray();
                ViewData["materials"] = materialArray;

                List<SelectListItem> materialInfos = new List<SelectListItem>();
                materialInfos.Add(new SelectListItem { Text = "不新增物料", Value = "0" });
                foreach (Materials item in db.Materials)
                {
                    materialInfos.Add(new SelectListItem { Text = string.Format(App_Start.Constants.MACHINE_MATERIAL_STRUCTURE, item.Code, item.Unit, item.DetailInfo), Value = item.ID.ToString() });
                }
                ViewData["materialInfos"] = materialInfos;

                return View(order);
            }
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        public ActionResult Edit(Orders orderNew, int material = 0)
        {
            Orders orderOld = null;
            using (var t = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                orderOld = db.Orders.Find(orderNew.OrderId);
                db.Orders.Attach(orderOld);

                orderOld.UpContinueCount = orderNew.UpContinueCount;
                orderOld.DownContinueCount = orderNew.DownContinueCount;
                orderOld.DetailInfo = orderNew.DetailInfo;
                orderOld.NoticeInfo = orderNew.NoticeInfo;
                if (material != 0)
                {
                    if (orderOld.MaterialInfo == null || orderOld.MaterialInfo == "")
                    {
                        orderOld.MaterialInfo = material.ToString();
                    }
                    else
                    {
                        orderOld.MaterialInfo = orderOld.MaterialInfo + ";" + material.ToString();
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Detail", new { id = orderOld.OrderId, error = enumErrorCode.HandlerSuccess });
        }

        //
        // GET: /Order/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Order/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

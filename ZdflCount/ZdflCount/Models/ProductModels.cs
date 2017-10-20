using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZdflCount.Models
{
    public enum enumStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        Unhandle = 0x00,

        /// <summary>
        /// 已分派
        /// </summary>
        [Description("已分派")]
        Assigned,

        /// <summary>
        /// 工作中
        /// </summary>
        [Description("工作中")]
        Working,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Finished,

        /// <summary>
        /// 临时保存
        /// </summary>
        [Description("临时保存")]
        Temporary,

        /// <summary>
        /// 已关闭
        /// </summary>
        [Description("已关闭")]
        Closed,

        /// <summary>
        /// 已报废
        /// </summary>
        [Description("已报废")]
        Discard
    }

    public enum enumMachineStatus
    {
        /// <summary>
        /// 设备正常
        /// </summary>
        [Description("设备正常")]
        Normal = 0x00,

        /// <summary>
        /// 设备故障
        /// </summary>
        [Description("设备故障")]
        Invalid,

        /// <summary>
        /// 维修中
        /// </summary>
        [Description("维修中")]
        Repair,

        /// <summary>
        /// 工作中
        /// </summary>
        [Description("工作中")]
        Working,

        /// <summary>
        /// 设备报废
        /// </summary>
        [Description("设备报废")]
        Discard
    }

    /// <summary>
    /// 订单模型
    /// </summary>
    public class Orders
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [StringLength(50)]
        [DisplayName("订单编号")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enumStatus Status { get; set; }

        /// <summary>
        /// 订单创建日期（订单自身属性）
        /// </summary>
        public string DateCreate { get; set; }

        /// <summary>
        /// 加载日期（订单加载到当前系统的时间）
        /// </summary>
        public string DateAssign{get;set;}

        /// <summary>
        /// 已经分配的施工单数
        /// </summary>
        public int AssignedCount{get;set;}

        /// <summary>
        /// 订单的商品总数量
        /// </summary>
        [DisplayName("订单总商品数")]
        public int ProductCount{get;set;}

        /// <summary>
        /// 已分派商品数
        /// </summary>
        public int ProductAssignedCount{get;set;}

        /// <summary>
        /// 已生产商品数量
        /// </summary>
        public int ProductFinishedCount{get;set;}

        /// <summary>
        /// 待分派商品数
        /// </summary>
        [DisplayName("可分派数量")]
        public int ProductFreeCount{get;set;}
    }


    public class OrderDbContext : DbContext
    {
        public OrderDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Orders> Orders { get; set; }
    }

    /// <summary>
    /// 施工单模型
    /// </summary>
    public class Schedules
    {
        /// <summary>
        /// 索引
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [DisplayName("施工单编号")]
        public int ID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("工单状态")]
        public enumStatus Status { get; set; }

        [StringLength(50)]
        [DisplayName("工单编号")]
        public string Number { get; set; }

        [DisplayName("机台")]
        public int MachineId { get; set; }

        [DisplayName("机台")]
        public string MachineName { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [StringLength(50)]
        [DisplayName("订单编号")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("施工单创建时间")]
        public DateTime DateCreate { get; set; }


        [DisplayName("最后修改时间")]
        public DateTime DateLastUpdate { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        [DisplayName("创建者")]
        public int CreatorID { get; set; }

        [StringLength(50)]
        [DisplayName("创建者")]
        public string CreatorName { get; set; }

        [DisplayName("最后修改人")]
        public int LastUpdatePersonID { get; set; }

        [StringLength(50)]
        [DisplayName("最后修改人")]
        public string LastUpdatePersonName { get; set; }

        /// <summary>
        /// 商品总数量
        /// </summary>
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "仅可以输入正整数")]
        [DisplayName("需生产总数")]
        public int ProductCount { get; set; }

        /// <summary>
        /// 已完成数
        /// </summary>
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "仅可以输入正整数")]
        [DisplayName("已完成数")]
        public int FinishCount { get; set; }

        [DisplayName("详细说明")]
        public string DetailInfo { get; set; }

        [DisplayName("注意事项")]
        public string NoticeInfo { get; set; }
    }

    public class ScheduleDbContext : DbContext
    {
        public ScheduleDbContext(): base("DefaultConnection")
        {
        }

        public DbSet<Schedules> Schedules { get; set; }
    }

    public class Machines
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [DisplayName("机台编号")]
        public int ID { get; set; }

        [DisplayName("机台名称")]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("IP地址")]
        public string IpAddress { get; set; }
        
        [DisplayName("Zigbee编号")]
        public string ZigbeeNumber { get; set; }

        [DisplayName("备注信息")]
        public string RemarkInfo { get; set; }

        
        [DisplayName("设备状态")]
        public enumMachineStatus Status { get; set; }

    }

    public class MachineDbContext : DbContext
    {
        public MachineDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Machines> Machines { get; set; }
    }

    public class ScheduleOrder
    {
        public Schedules Schedules { get; set; }
        public Orders Orders { get; set; }
        public List<SelectListItem> MachineList { get; private set; }

        private OrderDbContext dbOrder = new OrderDbContext();

        public ScheduleOrder()
        {
            IEnumerable<Machines> allMachines = from item in new MachineDbContext().Machines
                                                where item.Status == enumMachineStatus.Normal
                                                select item;            
            this.MachineList = new List<SelectListItem>(allMachines.Count<Machines>());
            foreach (Machines item in allMachines)
            {
                this.MachineList.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString () });
            }
        }

        public void SelectMachine(int machineId)
        {
            foreach (SelectListItem item in this.MachineList)
            {
                item.Selected = false;
                if (int.Parse(item.Value) == machineId)
                    item.Selected = true;
            }
        }

        public void GetOrderById(int orderId)
        {
            this.Orders = this.dbOrder.Orders.Find(orderId);
        }
    }
}

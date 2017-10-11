using System;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZdflCount.Models
{
    public enum enumOrderStatus
    {
        //未处理
        Unhandle = 0x00,

        //已分派
        Assigned,

        //停止生产
        Pause,

        //已完成
        Finished
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
        public string OrderNumber { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enumOrderStatus Status { get; set; }

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
        /// 已分派在生产商品数
        /// </summary>
        public int ProductWorkingCount{get;set;}
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
        public enumOrderStatus Status { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [DisplayName("订单编号")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        [DisplayName("创建者")]
        public int CreatorID { get; set; }

        /// <summary>
        /// 商品总数量
        /// </summary>
        [DisplayName("商品总数量")]
        public int ProductCount { get; set; }

        /// <summary>
        /// 已完成数
        /// </summary>
        [DisplayName("已完成数")]
        public int FinishCount { get; set; }
    }

    public class ScheduleDbContext : DbContext
    {
        public ScheduleDbContext(): base("DefaultConnection")
        {
        }

        public DbSet<Schedules> Schedules { get; set; }
    }
}

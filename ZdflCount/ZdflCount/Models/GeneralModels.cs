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
    #region 状态枚举
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
    #endregion

    #region 数据结构类
    #region 订单
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
    #endregion 

    #region 施工单
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
    #endregion

    #region 机器设备
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
    #endregion

    #region 生产记录
    public class ProductInfo
    {
        /// <summary>
        /// 索引
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("记录上传时间")]
        public DateTime DateCreate { get; set; }

        [StringLength(50)]
        [DisplayName("员工工号")]
        public string staffId { get; set; }

        [StringLength(50)]
        [DisplayName("员工姓名")]
        public string StaffName { get; set; }

        [DisplayName("机台")]
        public int MachineId { get; set; }

        [DisplayName("机台")]
        public string MachineName { get; set; }

        [DisplayName("机台网络地址")]
        public string MachineIP { get; set; }

        [DisplayName("通道数")]
        public int ChannelCount { get; set; }

        [DisplayName("通道1计划")]
        public int PlanCount1 { get; set; }
        [DisplayName("通道1已完成")]
        public int Finish1 { get; set; }
        [DisplayName("通道1异常")]
        public int Exception1 { get; set; }

        [DisplayName("通道2计划")]
        public int PlanCount2 { get; set; }
        [DisplayName("通道2已完成")]
        public int Finish2 { get; set; }
        [DisplayName("通道2异常")]
        public int Exception2 { get; set; }

        [DisplayName("通道3计划")]
        public int PlanCount3 { get; set; }
        [DisplayName("通道3已完成")]
        public int Finish3 { get; set; }
        [DisplayName("通道3异常")]
        public int Exception3 { get; set; }

        [DisplayName("通道4计划")]
        public int PlanCount4 { get; set; }
        [DisplayName("通道4已完成")]
        public int Finish4 { get; set; }
        [DisplayName("通道4异常")]
        public int Exception4 { get; set; }
    }
    #endregion

    #region 心跳
    public class HeartBreak
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("记录上传时间")]
        public DateTime DateCreate { get; set; }

        [DisplayName("机台")]
        public int MachineId { get; set; }

        [DisplayName("机台")]
        public string MachineName { get; set; }

        [DisplayName("机台网络地址")]
        public string MachineIP { get; set; }

        public int ChannelInfo { get; set; }

    }
    #endregion

    /// <summary>
    /// 异常日志
    /// </summary>
    public class ErrorInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime HappenTime { get; set; }

        public int userID { get; set; }

        public string Remark { get; set; }

        public string ErrorMsg { get; set; }

        public string ErrorSource { get; set; }

        public string ErrorStack { get; set; }
    }
    #endregion

    #region 前台页面显示模型
    public class ScheduleOrder
    {
        public Schedules Schedules { get; set; }
        public Orders Orders { get; set; }
        public List<SelectListItem> MachineList { get; private set; }

        private DbTableDbContext db = new DbTableDbContext();

        public ScheduleOrder()
        {
            IEnumerable<Machines> allMachines = from item in  db.Machines
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
            this.Orders = this.db.Orders.Find(orderId);
        }
    }
    #endregion

    #region 数据库操作
    public class DbTableDbContext : DbContext
    {
        public DbTableDbContext()
            : base("DefaultConnection")
        {
        }
        /// <summary>
        /// 生产记录
        /// </summary>
        public DbSet<ProductInfo> ProductionInfo { get; set; }
        /// <summary>
        /// 施工单
        /// </summary>
        public DbSet<Schedules> Schedules { get; set; }
        /// <summary>
        /// 心跳
        /// </summary>
        public DbSet<HeartBreak> HeartBreak { get; set; }
        /// <summary>
        /// 订单
        /// </summary>
        public DbSet<Orders> Orders { get; set; }
        /// <summary>
        /// 机器设备
        /// </summary>
        public DbSet<Machines> Machines { get; set; }
        /// <summary>
        /// 异常日志
        /// </summary>
        public DbSet<ErrorInfo> ErrorInfo { get; set; }


        /// <summary>
        /// 根据设备IP获取设备信息
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public Machines GetMachineByIp(string strIP)
        {
            Machines result = null;
            IEnumerable<Machines> allMachines = from item in this.Machines
                                                where item.IpAddress == strIP && item.Status == enumMachineStatus.Normal
                                                select item;

            if (allMachines.Count() > 0)
                result = allMachines.First();

            return result;
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="remark">备注信息（参数值）</param>
        public void RecordErrorInfo(Exception ex, string remark)
        {
            ErrorInfo info = new ErrorInfo()
            {
                HappenTime = DateTime.Now,
                Remark = remark,
                ErrorMsg = ex.Message,
                ErrorSource = ex.Source,
                ErrorStack = ex.StackTrace
            };
            this.ErrorInfo.Add(info);
            this.SaveChanges();
        }

    }
    #endregion
}

using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

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

    public enum enumStaffStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0x00,

        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        Dimission, 

        /// <summary>
        /// 退休
        /// </summary>
        [Description("退休")]
        Retire,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Deleted
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

    #region 员工信息
    public class StaffInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("状态")]
        public enumStaffStatus Status { get; set; }

        [StringLength(20)]
        [DisplayName("姓名")]
        [Required]
        public string Name { get; set; }

        [StringLength(20)]
        [DisplayName("职位")]
        public string Position { get; set; }

        [StringLength(5)]
        [DisplayName("性别")]
        public string Sex { get; set; }

        [StringLength(20)]
        [Required]
        [DisplayName("工号")]
        public string Number { get; set; }

        [StringLength(20)]
        [DisplayName("所在部门")]
        public string DeptName { get; set; }

        [StringLength(20)]
        [Required]
        [DisplayName("手机")]
        [RegularExpression(@"((\+86)|(86))?1[3-9]\d{9}", ErrorMessage = "请输入手机号码")]
        public string Phone { get; set; }

        [StringLength(20)]
        [DisplayName("座机")]
        public string telPhone { get; set; }

        [StringLength(200)]
        [DisplayName("现在住址")]
        public string Address { get; set; }

        [StringLength(200)]
        [DisplayName("家庭地址")]
        public string HomeAddress { get; set; }

        [DisplayName("生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<DateTime> BirthDate { get; set; }

        [DisplayName("入职日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<DateTime> JoinInDate { get; set; }

        [StringLength(20)]
        [DisplayName("紧急联系人")]
        public string EmergencyName { get; set; }

        [StringLength(20)]
        [DisplayName("紧急联系人电话")]
        public string EmergencyPhone { get; set; }

        [DisplayName("备注信息")]
        public string Remarks { get; set; }

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
        [DisplayName("机台ID")]
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("机台编码")]
        public string Number { get; set; }

        [DisplayName("机台名称")]
        [StringLength(50)]
        public string Name { get; set; }

        [DisplayName("所在车间")]
        public int RoomID{get;set;}
        
        [StringLength(50)]
        [DisplayName("所在车间")]
        public string RoomName{get;set;}

        [StringLength(50)]
        [DisplayName("IP地址")]
        public string IpAddress { get; set; }

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
        public string staffNumber { get; set; }

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

        [DisplayName("信息类型")]
        public byte MsgType  { get; set; }

        [DisplayName("通道1已完成")]
        public int ChannelFinish1 { get; set; }

        [DisplayName("通道2已完成")]
        public int ChannelFinish2 { get; set; }

        [DisplayName("通道3已完成")]
        public int ChannelFinish3 { get; set; }

        [DisplayName("通道4已完成")]
        public int ChannelFinish4 { get; set; }

        [DisplayName("通道5已完成")]
        public int ChannelFinish5 { get; set; }

        [DisplayName("通道6已完成")]
        public int ChannelFinish6 { get; set; }

        [DisplayName("异常总数")]
        public int ExceptionCount { get; set; }

        public override string ToString()
        {
            return string.Format("ID:{0};时间:{1};工号:{2};姓名:{3};机台ID:{4};机台:{5};机台网址:{6};通道数:{7};信息类型:{8};通道1:{9};通道2:{10};通道3:{11};通道4:{12};通道5:{13};通道6:{14};异常总数{15}",
                this.ID, this.DateCreate, this.staffNumber, this.StaffName, this.MachineId, this.MachineName, this.MachineIP, this.ChannelCount, this.MsgType, this.ChannelFinish1, this.ChannelFinish2, this.ChannelFinish3,
                this.ChannelFinish4, this.ChannelFinish5, this.ChannelFinish6, this.ExceptionCount);
        }
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
        [StringLength(50)]
        public string MachineName { get; set; }

        public int ChannelInfo { get; set; }

    }
    #endregion

    #region 车间
    public class FactoryRoom
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RoomID { get; set; }

        [DisplayName("车间")]
        [StringLength(50)]
        public string RoomName { get; set; }

        [DisplayName("工厂")]
        [StringLength(50)]
        public string FactoryName { get; set; }

        [DisplayName("设备总数量")]
        public int MachineCount { get; set; }

        [DisplayName("负责人")]
        public string ManagerID { get; set; }

        [DisplayName("负责人")]
        [StringLength(50)]
        public string ManagerName { get; set; }

        [DisplayName("负责人电话")]
        [StringLength(20)]
        public string ManagerPhone { get; set; }

        [DisplayName("车间电话")]
        [StringLength(20)]
        public string Phone { get; set; }

        [DisplayName("车间位置")]
        [StringLength(50)]
        public string Address { get; set; }

        [DisplayName("车间备注信息")]
        public string Remarks { get; set; }
    }
    #endregion

    #region 统计基础数据
    public class StatisticInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("员工姓名")]
        [StringLength(50)]
        public string StaffName { get; set; }

        [DisplayName("员工工号")]
        [Required]
        [StringLength(50)]
        public string StaffNumber { get; set; }

        [DisplayName("设备备注名")]
        [StringLength(50)]
        public string MachineName { get; set; }

        [DisplayName("设备编码")]
        [Required]
        [StringLength(50)]
        public string MachineNumber { get; set; }

        [DisplayName("车间")]
        [Required]
        public int RoomID { get; set; }

        [DisplayName("车间")]
        [StringLength(50)]
        public string RoomName { get; set; }

        [DisplayName("订单编号")]
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        [DisplayName("工厂")]
        [StringLength(50)]
        public string Factory { get; set; }

        [DisplayName("完成总数")]
        [Required]
        public int FinishCount { get; set; }

        [DisplayName("异常总数")]
        [Required]
        public int ExceptionCount { get; set; }

        [DisplayName("日期")]
        [Required]
        public DateTime Date { get; set; }
    }
    #endregion

    #region 最新心跳包记录
    public class LastHeartBreak
    {
        public DateTime DateRefresh { get; set; }

        [Key]
        public int MachineId { get; set; }
        
        [StringLength(50)]
        public string MachineName { get; set; }

        public int ChannelInfo { get; set; }

        public int RoomID {get;set;}
                
        [StringLength(50)]
        public string RoomName { get; set; }

        public int MachineCount { get; set; }
        
        [StringLength(50)]
        public string FactoryName { get; set; }
    }
    #endregion

    #region 异常日志
    public class ErrorInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("异常类型")]
        public App_Start.enumSystemErrorCode ErrorType { get; set; }

        [DisplayName("时间")]
        public DateTime HappenTime { get; set; }

        [DisplayName("帐号")]
        public int userID { get; set; }

        [DisplayName("备注")]
        public string Remark { get; set; }

        public byte[] RemarkBinary { get; set; }

        [DisplayName("错误信息")]
        public string ErrorMsg { get; set; }

        [DisplayName("主体")]
        public string ErrorSource { get; set; }

        public string ErrorStack { get; set; }
    }
    #endregion
    #endregion

    #region 前台页面显示模型
    /// <summary>
    /// 订单、施工单
    /// </summary>
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

    /// <summary>
    /// 设备状态
    /// </summary>
    public class DeviceStatus
    {
        public string FactoryName { get; set; }

        public string RoomName { get; set; }

        public int RoomID { get; set; }

        public int MachineCount { get; set; }

        public Dictionary<string, DateTime> MachineList { get; set; }
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
        public DbSet<ProductInfo> ProductInfo { get; set; }
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
        /// 员工信息
        /// </summary>
        public DbSet<StaffInfo> StaffInfo { get; set; }
        /// <summary>
        /// 工厂车间
        /// </summary>
        public DbSet<FactoryRoom> FactoryRoom { get; set; }
        /// <summary>
        /// 统计资料库
        /// </summary>
        public DbSet<StatisticInfo> Statistics { get; set; }
        /// <summary>
        /// 最新的心跳记录
        /// </summary>
        public DbSet<LastHeartBreak> LastHeartBreak { get; set; }

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
        /// <param name="type">异常类型</param>
        /// <param name="ex">异常</param>
        /// <param name="remark">备注信息（参数值）</param>
        /// <param name="remarkBinary">备注信息（二进制形式）</param>
        /// <param name="userId"></param>
        public void RecordErrorInfo(App_Start.enumSystemErrorCode type, Exception ex, string remark, byte[] remarkBinary,int userId=0)
        {
            ErrorInfo info = new ErrorInfo()
            {
                HappenTime = DateTime.Now,
                ErrorType = type,
                Remark = remark,
                userID = userId,
                ErrorMsg = ex.Message,
                ErrorSource = ex.Source,
                ErrorStack = ex.StackTrace,
                RemarkBinary = remarkBinary
            };
            this.ErrorInfo.Add(info);
            this.SaveChanges();
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="type">异常类型</param>
        /// <param name="remark">备注信息（参数值）</param>
        /// <param name="remarkBinary">备注信息（二进制形式）</param>
        public void RecordErrorInfo(App_Start.enumSystemErrorCode type, string remark, byte[] remarkBinary, int userId = 0)
        {
            ErrorInfo info = new ErrorInfo()
            {
                HappenTime = DateTime.Now,
                ErrorType = type,
                userID = userId,
                Remark = remark,
                RemarkBinary = remarkBinary
            };
            this.ErrorInfo.Add(info);
            this.SaveChanges();
        }

    }
    #endregion
}

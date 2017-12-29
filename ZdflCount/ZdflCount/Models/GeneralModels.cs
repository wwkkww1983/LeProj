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

    public enum enumDeviceWarnningStatus
    {
        /// <summary>
        /// 设备端新发上来
        /// </summary>
        [Description("未处理")]
        Unhandler = 0x00,

        /// <summary>
        /// 管理平台已经响应
        /// </summary>
        [Description("已经知道")]
        GetInfo
    }

    public enum enumDeviceWorkStatus
    {
        [Description("启动")]
        Start = 0x01,

        [Description("停止")]
        End
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
        [DisplayName("订单状态")]
        public enumStatus Status { get; set; }

        public int RoomId { get; set; }

        [DisplayName("车间编码")]
        [StringLength(50)]
        public string RoomNumber { get; set; }

        [DisplayName("车间")]
        [StringLength(50)]
        public string RoomName { get; set; }

        /// <summary>
        /// 订单交期（订单自身属性）
        /// </summary>
        [DisplayName("订单交期")]
        public DateTime DateNeedFinish { get; set; }

        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "仅可以输入正整数")]
        [DisplayName("上跳持续数量")]
        public int UpContinueCount { get; set; }

        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "仅可以输入正整数")]
        [DisplayName("落下持续数量")]
        public int DownContinueCount { get; set; }

        [StringLength(50)]
        [DisplayName("物料信息")]
        public string MaterialInfo { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>
        [DisplayName("产品代码")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品单位
        /// </summary>
        [DisplayName("产品单位")]
        public string ProductUnit { get; set; }
        /// <summary>
        /// 产品详情
        /// </summary>
        [DisplayName("产品详情")]
        public string ProductInfo { get; set; }

        /// <summary>
        /// 加载日期（订单加载到当前系统的时间）
        /// </summary>
        [DisplayName("订单加载时间")]
        public DateTime DateAssign { get; set; }

        /// <summary>
        /// 已经分配的施工单数
        /// </summary>
        [DisplayName("施工单数")]
        public int AssignedCount { get; set; }

        /// <summary>
        /// 订单的商品总数量
        /// </summary>
        [DisplayName("订单总商品数")]
        public int ProductCount { get; set; }

        /// <summary>
        /// 已分派商品数
        /// </summary>
        [DisplayName("已分派商品数")]
        public int ProductAssignedCount { get; set; }

        /// <summary>
        /// 已生产商品数量
        /// </summary>
        [DisplayName("已生产商品数")]
        public int ProductFinishedCount { get; set; }

        /// <summary>
        /// 待分派商品数
        /// </summary>
        [DisplayName("可分派数量")]
        public int ProductFreeCount { get; set; }

        [DisplayName("详细说明")]
        public string DetailInfo { get; set; }

        [DisplayName("注意事项")]
        public string NoticeInfo { get; set; }
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

        [StringLength(50)]
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("登录密码")]
        public string Password { get; set; }

        [StringLength(20)]
        [DisplayName("所在部门")]
        public string DeptName { get; set; }

        [DisplayName("部门编号")]
        public string DeptNumber { get; set; }

        public int DeptId { get; set; }

        [StringLength(20)]
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
        /// 设备所在车间
        /// </summary>
        public int RoomId { get; set; }

        [DisplayName("车间")]
        [StringLength(30)]
        public string RoomNumber { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>
        [DisplayName("产品代码")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品单位
        /// </summary>
        [DisplayName("产品单位")]
        public string ProductUnit { get; set; }
        /// <summary>
        /// 产品详情
        /// </summary>
        [DisplayName("产品详情")]
        public string ProductInfo { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [StringLength(50)]
        [DisplayName("订单编号")]
        public string OrderNumber { get; set; }

        [StringLength(50)]
        [DisplayName("物料信息")]
        public string MaterialInfo { get; set; }

        [DisplayName("物料信息")]
        public string MaterialDetail { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("工单创建时间")]
        public DateTime DateCreate { get; set; }

        [DisplayName("最后修改时间")]
        public DateTime DateLastUpdate { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        [DisplayName("创建者")]
        public int CreatorID { get; set; }

        [StringLength(50)]
        [DisplayName("施工单创建人")]
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

        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "仅可以输入正整数")]
        [DisplayName("上跳持续数量")]
        public int UpContinueCount { get; set; }

        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "仅可以输入正整数")]
        [DisplayName("落下持续数量")]
        public int DownContinueCount { get; set; }

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

    #region 生产物料信息
    public class Materials
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 原料单位
        /// </summary>
        [StringLength(20)]
        public string Unit { get; set; }

        /// <summary>
        /// 物料描述
        /// </summary>
        [StringLength(500)]
        public string DetailInfo { get; set; }

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
        public int RoomID { get; set; }

        [StringLength(50)]
        [DisplayName("车间编码")]
        public string RoomNumber { get; set; }

        [StringLength(50)]
        [DisplayName("车间")]
        public string RoomName { get; set; }

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

        [DisplayName("记录时间")]
        public DateTime DateCreate { get; set; }

        [StringLength(50)]
        [DisplayName("员工工号")]
        public string staffNumber { get; set; }

        [StringLength(50)]
        [DisplayName("员工姓名")]
        public string StaffName { get; set; }

        [StringLength(50)]
        [DisplayName("工单号")]
        public string ScheduleNumber { get; set; }

        [DisplayName("机台")]
        public int MachineId { get; set; }

        [DisplayName("机台")]
        public string MachineName { get; set; }

        [DisplayName("通道数")]
        public int ChannelCount { get; set; }

        [DisplayName("信息类型")]
        public byte MsgType { get; set; }

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
            return string.Format("ID:{0};时间:{1};工号:{2};姓名:{3};机台ID:{4};机台:{5};工单号:{6};通道数:{7};信息类型:{8};通道1:{9};通道2:{10};通道3:{11};通道4:{12};通道5:{13};通道6:{14};异常总数{15}",
                this.ID, this.DateCreate, this.staffNumber, this.StaffName, this.MachineId, this.MachineName, this.ScheduleNumber, this.ChannelCount, this.MsgType, this.ChannelFinish1, this.ChannelFinish2, this.ChannelFinish3,
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

    #region 设备报修记录
    public class MachineReport
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("报修时间")]
        public DateTime DateCreate { get; set; }

        [DisplayName("响应时间")]
        public Nullable<DateTime> DateGetInfo { get; set; }

        public int MachineId { get; set; }

        [DisplayName("机台编号")]
        [StringLength(50)]
        public string MachineNumber { get; set; }

        [DisplayName("机台")]
        [StringLength(50)]
        public string MachineName { get; set; }

        public int RoomId { get; set; }

        [DisplayName("车间编码")]
        [StringLength(50)]
        public string RoomNumber { get; set; }

        [DisplayName("车间")]
        [StringLength(50)]
        public string RoomName { get; set; }

        [DisplayName("状态")]
        public enumDeviceWarnningStatus Status { get; set; }

    }
    #endregion

    #region 设备叫料记录
    public class MachineCallMaterial
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("叫料时间")]
        public DateTime DateCreate { get; set; }

        [DisplayName("响应时间")]
        public Nullable<DateTime> DateGetInfo { get; set; }

        public int MachineId { get; set; }

        [DisplayName("机台编号")]
        [StringLength(50)]
        public string MachineNumber { get; set; }

        [DisplayName("机台")]
        [StringLength(50)]
        public string MachineName { get; set; }

        public int RoomId { get; set; }

        [DisplayName("车间编码")]
        [StringLength(50)]
        public string RoomNumber { get; set; }

        [StringLength(50)]
        [DisplayName("车间")]
        public string RoomName { get; set; }

        public int OrderId { get; set; }

        [DisplayName("订单编码")]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        public int ScheduleId { get; set; }

        [DisplayName("施工单编码")]
        [StringLength(50)]
        public string SchueduleNumber { get; set; }

        [DisplayName("物料信息")]
        public string MaterialInfo { get; set; }

        [DisplayName("状态")]
        public enumDeviceWarnningStatus Status { get; set; }

    }
    #endregion

    #region 设备启动停止记录
    public class MachineStartEnd
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("启动时间")]
        public Nullable<DateTime> DateStart { get; set; }

        [DisplayName("停止时间")]
        public Nullable<DateTime> DateEnd { get; set; }

        public int MachineId { get; set; }

        [DisplayName("机台编号")]
        [StringLength(50)]
        public string MachineNumber { get; set; }

        [DisplayName("机台")]
        [StringLength(50)]
        public string MachineName { get; set; }

        public int RoomId { get; set; }

        [DisplayName("车间编码")]
        [StringLength(50)]
        public string RoomNumber { get; set; }

        public int OrderId { get; set; }

        [DisplayName("订单编码")]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        public int ScheduleId { get; set; }

        [DisplayName("施工单编码")]
        [StringLength(50)]
        public string SchueduleNumber { get; set; }

        public int UserId { get; set; }

        [DisplayName("员工编码")]
        public string UserNumber { get; set; }

        [DisplayName("状态")]
        public enumDeviceWorkStatus Status { get; set; }

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

        [DisplayName("车间编码")]
        public string RoomNumber { get; set; }

        [DisplayName("工厂")]
        [StringLength(50)]
        public string FactoryName { get; set; }

        [DisplayName("报修账号")]
        [StringLength(50)]
        public string RepairNumber { get; set; }

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

    #region 车间权限管理
    public class UsersInRooms
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }
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

        public int MachineId { get; set; }

        [DisplayName("设备编码")]
        [Required]
        [StringLength(50)]
        public string MachineNumber { get; set; }

        [Required]
        public int RoomID { get; set; }

        [DisplayName("车间")]
        [StringLength(50)]
        public string RoomName { get; set; }

        [Required]
        public int ScheduleID { get; set; }

        [DisplayName("施工单编号")]
        [Required]
        [StringLength(50)]
        public string ScheduleNumber { get; set; }

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

        [DisplayName("登录时间")]
        public Nullable<DateTime> DateStart { get; set; }

        [DisplayName("退出时间")]
        public Nullable<DateTime> DateOut { get; set; }
        /// <summary>
        /// 登录流水ID
        /// </summary>
        public int ProductIdStart { get; set; }
        /// <summary>
        /// 退出流水ID
        /// </summary>
        public int ProductIdOut { get; set; }
    }
    #endregion

    #region 最新心跳包记录
    public class LastHeartBreak
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime DateRefresh { get; set; }

        //[Key]
        public int MachineId { get; set; }

        [StringLength(50)]
        public string MachineName { get; set; }

        public int RoomID { get; set; }

        [StringLength(50)]
        public string RoomName { get; set; }

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

    #region 统计视图
    #region 员工产量表

    public class VS_StaffProduction
    {
        [Key]
        public Guid ID { get; set; }

        [DisplayName("车间")]
        [StringLength(50)]
        public string RoomName { get; set; }

        [DisplayName("员工姓名")]
        [StringLength(50)]
        public string StaffName { get; set; }

        [DisplayName("订单编号")]
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        [DisplayName("完成总数")]
        [Required]
        public int Finish { get; set; }

        [DisplayName("异常总数")]
        [Required]
        public int Exception { get; set; }
    }
    #endregion

    #region 机台产量表

    public class VS_MachineProduction
    {
        [Key]
        public Guid ID { get; set; }

        [DisplayName("车间")]
        [StringLength(50)]
        public string RoomName { get; set; }

        [DisplayName("设备编码")]
        [Required]
        [StringLength(50)]
        public string MachineNumber { get; set; }

        [DisplayName("订单编号")]
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        [DisplayName("完成总数")]
        [Required]
        public int Finish { get; set; }

        [DisplayName("异常总数")]
        [Required]
        public int Exception { get; set; }
    }
    #endregion

    #region 开机时间表

    public class VS_MachineWorking
    {
        [Key]
        public Guid ID { get; set; }

        [DisplayName("车间")]
        [StringLength(50)]
        public string RoomName { get; set; }

        [DisplayName("设备编码")]
        [Required]
        [StringLength(50)]
        public string MachineNumber { get; set; }

        [DisplayName("工作总时间")]
        [Required]
        public int WorkingMinute { get; set; }
    }
    #endregion
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
        public Dictionary<int, string> MaterialList { get; private set; }
        public List<SelectListItem> MachineList { get; private set; }

        private DbTableDbContext db = new DbTableDbContext();

        public ScheduleOrder()
        {
        }

        private int[] GetRoomsForUser(int userId)
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

        public void GetMachineByUser(int userId)
        {
            int[] rooms = this.GetRoomsForUser(userId);
            IEnumerable<Machines> allMachines = from item in db.Machines
                                                where item.Status == enumMachineStatus.Normal && rooms.Contains(item.RoomID)
                                                select item;
            this.MachineList = new List<SelectListItem>(allMachines.Count<Machines>());
            foreach (Machines item in allMachines)
            {
                this.MachineList.Add(new SelectListItem { Text = item.RoomName + " - " + item.Number, Value = item.ID.ToString() });
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
            this.GetOrderMaterial(this.Orders.MaterialInfo);
        }

        public void GetOrderMaterial(string strMaterials)
        {
            string[] strMatArray = strMaterials.Split(';');
            int[] intMatArray = new int[strMatArray.Length];
            for (int i = 0; i < strMatArray.Length; i++)
            {
                intMatArray[i] = int.Parse(strMatArray[i]);
            }
            IEnumerable<Materials> tempMaterialList = from item in this.db.Materials
                                                      where intMatArray.Contains(item.ID)
                                                      select item;
            this.MaterialList = new Dictionary<int, string>();
            foreach (Materials item in tempMaterialList)
            {
                this.MaterialList.Add(item.ID, string.Format(App_Start.Constants.MACHINE_MATERIAL_STRUCTURE, item.Code, item.Unit, item.DetailInfo));
            }
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

        public string[] OfflineMachines { get; set; }
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
        /// 设备报修记录
        /// </summary>
        public DbSet<MachineReport> MachineReport { get; set; }
        /// <summary>
        /// 设备叫料记录
        /// </summary>
        public DbSet<MachineCallMaterial> MachineCallMaterial { get; set; }
        /// <summary>
        /// 设备物料信息
        /// </summary>
        public DbSet<Materials> Materials { get; set; }
        /// <summary>
        /// 用户车间管理权限
        /// </summary>
        public DbSet<UsersInRooms> UsersInRooms { get; set; }
        /// <summary>
        /// 设备启动停止记录
        /// </summary>
        public DbSet<MachineStartEnd> MachineStartEnd { get; set; }


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
        public void RecordErrorInfo(App_Start.enumSystemErrorCode type, Exception ex, string remark, byte[] remarkBinary, int userId = 0)
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
            try
            {
                this.ErrorInfo.Add(info);
                this.SaveChanges();
            }
            catch
            {

            }
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
            }; try
            {
                this.ErrorInfo.Add(info);
                this.SaveChanges();
            }
            catch
            {

            }
        }

    }

    public class DbViewDbContext : DbContext
    {
        public DbViewDbContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// 员工产量表
        /// </summary>
        public DbSet<VS_StaffProduction> StaffProduction { get; set; }

        /// <summary>
        /// 设备产量表
        /// </summary>
        public DbSet<VS_MachineProduction> MachineProduction { get; set; }

        /// <summary>
        /// 设备开机时间
        /// </summary>
        public DbSet<VS_MachineWorking> MachineWorking { get; set; }        
    }
    #endregion
}

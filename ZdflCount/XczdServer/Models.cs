using System;

namespace XczdServer
{
    #region 数据表类型枚举
    /// <summary>
    /// 数据表的主键类型
    /// </summary>
    public enum Generator : byte
    {
        /// <summary>
        /// GUID
        /// </summary>
        Guid,
        /// <summary>
        /// 默认-数据库自带方式
        /// </summary>
        Native,
        /// <summary>
        /// 序列-程序编写全局的序列使用
        /// </summary>
        Sequence,
        /// <summary>
        /// 程序赋值
        /// </summary>
        Assigned
    }

    /// <summary>
    /// 数据表的索引类型
    /// </summary>
    public enum IndexType : byte
    {
        /// <summary>
        /// 普通索引
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 唯一索引
        /// </summary>
        Unique = 2,

        /// <summary>
        /// 全文索引
        /// </summary>
        FullText = 4,

        /// <summary>
        /// 空间索引
        /// </summary>
        Spatial = 8,

        /// <summary>
        /// 多列索引
        /// </summary>
        MultiColumn = 16,

        /// <summary>
        /// 聚集索引
        /// </summary>
        Clustered = 32
    }
    #endregion 

    #region 状态枚举


    public enum enumStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        Unhandle = 0x00,

        /// <summary>
        /// 已分派
        /// </summary>
        Assigned,

        /// <summary>
        /// 工作中
        /// </summary>
        Working,

        /// <summary>
        /// 已完成
        /// </summary>
        Finished,

        /// <summary>
        /// 临时保存
        /// </summary>
        Temporary,

        /// <summary>
        /// 已关闭
        /// </summary>
        Closed,

        /// <summary>
        /// 已报废
        /// </summary>
        Discard
    }

    public enum enumMachineStatus
    {
        /// <summary>
        /// 设备正常
        /// </summary>
        Normal = 0x00,

        /// <summary>
        /// 设备故障
        /// </summary>
        Invalid,

        /// <summary>
        /// 维修中
        /// </summary>
        Repair,

        /// <summary>
        /// 工作中
        /// </summary>
        Working,

        /// <summary>
        /// 设备报废
        /// </summary>
        Discard
    }

    public enum enumDeviceWarnningStatus
    {
        /// <summary>
        /// 设备端新发上来
        /// </summary>
        Unhandler = 0x00,

        /// <summary>
        /// 管理平台已经响应
        /// </summary>
        GetInfo
    }

    public enum enumDeviceWorkStatus
    {
        Start = 0x01,

        End
    }
    #endregion

    #region 系统数据类型枚举

    /// <summary>
    /// 命令
    /// </summary>
    public enum enumCommandType
    {
        /// <summary>
        /// 下派施工单
        /// </summary>
        DOWN_SHEDULE_SEND = 601,
        DOWN_SHEDULE_RESP = 602,

        /// <summary>
        /// 关闭施工单
        /// </summary>
        DOWN_SHEDULE_CLOSE_SEND = 611,
        DOWN_SHEDULE_CLOSE_RESP = 612,

        /// <summary>
        /// 报废施工单
        /// </summary>
        DOWN_SHEDULE_DISCARD_SEND = 621,
        DOWN_SHEDULE_DISCARD_RESP = 622,

        /// <summary>
        /// 心跳
        /// </summary>
        UP_HEART_SEND = 101,
        UP_HEART_RESP = 102,

        /// <summary>
        /// 生产情况
        /// </summary>
        UP_PRODUCT_SEND = 201,
        UP_PRODUCT_RESP = 202,

        /// <summary>
        /// 设备报修
        /// </summary>
        UP_DEVICE_REPORT_SEND = 211,
        UP_DEVICE_REPORT_RESP = 212,

        /// <summary>
        /// 设备叫料
        /// </summary>
        UP_DEVICE_CALL_MATERIAL_SEND = 221,
        UP_DEVICE_CALL_MATERIAL_RESP = 222,

        /// <summary>
        /// 设备叫料
        /// </summary>
        UP_DEVICE_STARTEND_SEND = 231,
        UP_DEVICE_STARTEND_RESP = 232,

        /// <summary>
        /// 设备信息设置
        /// </summary>
        UP_DEVICE_SETTING_SEND = 301,
        UP_DEVICE_SETTING_RESP = 302
    }

    /// <summary>
    /// 返回给客户端的错误码
    /// </summary>
    public enum enumErrorCode
    {
        NONE = 0x00,

        /// <summary>
        /// 操作成功
        /// </summary>
        HandlerSuccess,

        /// <summary>
        /// 仅可以提交Excel文件
        /// </summary>
        FileOnlyExcel,

        /// <summary>
        /// 文件格式错误
        /// </summary>
        FileFormatError,

        /// <summary>
        /// Excel表头校验失败
        /// </summary>
        ExcelHeadError,

        /// <summary>
        /// Excel部分内容校验失败
        /// </summary>
        ExcelContentError,

        /// <summary>
        /// 设备没在工作中，不能下发信息
        /// </summary>
        DeviceNotWork,

        /// <summary>
        /// 设备处理失败，请重新下派
        /// </summary>
        DeviceRespFailInfo,

        /// <summary>
        /// 设备接受超时
        /// </summary>
        DeviceReciveTimeOut,

        /// <summary>
        /// 设备已有的施工单达到最大数量
        /// </summary>
        DeviceScheduleFull,

        /// <summary>
        /// 设备正在为该施工单生产，无法进行操作
        /// </summary>
        DeviceScheduleWorking,

        /// <summary>
        /// 设备通信失败
        /// </summary>
        DeviceCommunicateError
    }


    /// <summary>
    /// 系统自身错误码
    /// </summary>
    public enum enumSystemErrorCode
    {
        NONE = 0x00,

        /// <summary>
        /// 监听线程异常
        /// </summary>
        TcpListenerException,

        /// <summary>
        /// TCP监听线程模块
        /// </summary>
        TcpRecieveErr,

        /// <summary>
        /// TCP接受后，处理程序
        /// </summary>
        TcpHandlerException,

        /// <summary>
        /// 设备连接处理异常
        /// </summary>
        TcpMachineStreamException,

        /// <summary>
        /// TCP接受编码找不到，使用了默认处理程序处理
        /// </summary>
        TcpDefaultHandlerErr,

        /// <summary>
        /// 给客户端发送模块
        /// </summary>
        TcpSenderException,

        /// <summary>
        /// 生产信息退出登录员工不同
        /// </summary>
        ProductOutInDiff,
        ProductOutWithoutIn,
        /// <summary>
        /// 设备报修外部接口调用失败
        /// </summary>
        DeviceReportOutInterface
    }

    /// <summary>
    /// 生产数据类型
    /// </summary>
    public enum enumProductType
    {
        /// <summary>
        /// 员工在设备端登录（开始记录状态）
        /// </summary>
        LoginIn = 0x00,
        /// <summary>
        /// 退出（统计信息）
        /// </summary>
        LoginOut
    }
    #endregion

    #region 数据结构类

    #region 机器设备
    public class Machines
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomName { get; set; }
        public string IpAddress { get; set; }
        public string RemarkInfo { get; set; }
        public enumMachineStatus Status { get; set; }
    }
    #endregion

    #region 生产记录
    public class ProductInfo
    {
        public int ID { get; set; }
        public DateTime DateCreate { get; set; }
        public string staffNumber { get; set; }
        public string StaffName { get; set; }
        public string ScheduleNumber { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int ChannelCount { get; set; }
        public byte MsgType  { get; set; }
        public int ChannelFinish1 { get; set; }
        public int ChannelFinish2 { get; set; }
        public int ChannelFinish3 { get; set; }
        public int ChannelFinish4 { get; set; }
        public int ChannelFinish5 { get; set; }
        public int ChannelFinish6 { get; set; }
        public int ExceptionCount { get; set; }

        public override string ToString()
        {
            return string.Format("ID:{0};时间:{1};工号:{2};姓名:{3};机台ID:{4};机台:{5};工单号:{6};通道数:{7};信息类型:{8};通道1:{9};通道2:{10};通道3:{11};通道4:{12};通道5:{13};通道6:{14};异常总数{15}",
                this.ID, this.DateCreate, this.staffNumber, this.StaffName, this.MachineId, this.MachineName, this.ScheduleNumber, this.ChannelCount, this.MsgType, this.ChannelFinish1, this.ChannelFinish2, this.ChannelFinish3,
                this.ChannelFinish4, this.ChannelFinish5, this.ChannelFinish6, this.ExceptionCount);
        }
    }
    #endregion

    #region 车间
    public class FactoryRoom
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomNumber { get; set; }
        public string FactoryName { get; set; }
        public int MachineCount { get; set; }
        public string ManagerID { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPhone { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
    }
    #endregion

    #region 登陆账户
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
    #endregion

    #region 心跳
    public class HeartBreak
    {
        public int ID { get; set; }
        public DateTime DateCreate { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int ChannelInfo { get; set; }

    }
    #endregion

    #region 设备报修记录
    public class MachineReport
    {
        public int ID { get; set; }
        public DateTime DateCreate { get; set; }
        public Nullable<DateTime> DateGetInfo { get; set; }
        public int MachineId { get; set; }
        public string MachineNumber { get; set; }
        public string MachineName { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomName { get; set; }
        public enumDeviceWarnningStatus Status { get; set; }

    }
    #endregion

    #region 设备叫料记录
    public class MachineCallMaterial
    {
        public int ID { get; set; }
        public DateTime DateCreate { get; set; }
        public Nullable<DateTime> DateGetInfo { get; set; }
        public int MachineId { get; set; }
        public string MachineNumber { get; set; }
        public string MachineName { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomName { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int ScheduleId { get; set; }
        public string SchueduleNumber { get; set; }
        public string MaterialInfo { get; set; }
        public enumDeviceWarnningStatus Status { get; set; }

    }
    #endregion

    #region 设备启动停止记录
    public class MachineStartEnd
    {
        public int ID { get; set; }
        public Nullable<DateTime> DateStart { get; set; }
        public Nullable<DateTime> DateEnd { get; set; }
        public int MachineId { get; set; }
        public string MachineNumber { get; set; }
        public string MachineName { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int ScheduleId { get; set; }
        public string SchueduleNumber { get; set; }
        public int UserId { get; set; }
        public string UserNumber { get; set; }
        public enumDeviceWorkStatus Status { get; set; }

    }
    #endregion

    #region 最新心跳包记录
    public class LastHeartBreak
    {
        public int ID { get; set; }
        public DateTime DateRefresh { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int RoomID {get;set;}
        public string RoomName { get; set; }
        public string FactoryName { get; set; }
    }
    #endregion

    #region 统计基础数据
    public class StatisticInfo
    {
        public int ID { get; set; }
        public string StaffName { get; set; }
        public string StaffNumber { get; set; }
        public int MachineId { get; set; }
        public string MachineNumber { get; set; }
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int ScheduleID { get; set; }
        public string ScheduleNumber { get; set; }
        public string OrderNumber { get; set; }
        public string Factory { get; set; }
        public int FinishCount { get; set; }
        public int ExceptionCount { get; set; }
        public Nullable<DateTime> DateStart { get; set; }
        public Nullable<DateTime> DateOut { get; set; }
        public int ProductIdStart { get; set; }
        public int ProductIdOut { get; set; }
    }
    #endregion

    #region 施工单
    public class Schedules
    {
        public int ID { get; set; }
        public enumStatus Status { get; set; }
        public string Number { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int OrderId { get; set; }
        public string ProductCode { get; set; }
        public string ProductUnit { get; set; }
        public string ProductInfo { get; set; }
        public string OrderNumber { get; set; }
        public string MaterialInfo { get; set; }
        public string MaterialDetail { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateLastUpdate { get; set; }
        public int CreatorID { get; set; }
        public string CreatorName { get; set; }
        public int LastUpdatePersonID { get; set; }
        public string LastUpdatePersonName { get; set; }
        public int ProductCount { get; set; }
        public int UpContinueCount { get; set; }
        public int DownContinueCount { get; set; }
        public int FinishCount { get; set; }
        public string DetailInfo { get; set; }
        public string NoticeInfo { get; set; }
    }
    #endregion

    #region 异常日志
    public class ErrorInfo
    {
        public int ID { get; set; }
        public enumSystemErrorCode ErrorType { get; set; }
        public DateTime HappenTime { get; set; }
        public int userID { get; set; }
        public string Remark { get; set; }
        public byte[] RemarkBinary { get; set; }
        public string ErrorMsg { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorStack { get; set; }
    }
    #endregion

    #endregion

    #region 接收信息包结构体
    public class NetStructure
    {    /// <summary>
        /// 数据格式框架
        /// </summary>
        public struct NormalDataStruct
        {
            public int FactoryNumber;
            public enumCommandType Code;
            public int contentLen;
            public int FillField;
            public byte[] Content;
            public string IpAddress;
            public System.Net.Sockets.NetworkStream stream;
        }

        /// <summary>
        /// 生产情况
        /// </summary>
        public struct ProductInfo
        {
            public int MachineId;
            public string ScheduleNumber;
            public int ChannelCount;
            public string StaffNumber;
            public string StaffName;
            public enumProductType MsgStatus;
            public int ChannelFinish1;
            public int ChannelFinish2;
            public int ChannelFinish3;
            public int ChannelFinish4;
            public int ChannelFinish5;
            public int ChannelFinish6;
            public int UnusualCount;
        }

        /// <summary>
        /// 心跳
        /// </summary>
        public struct HeartBreak
        {
            public int MachineId;
            public int ChannelInfo;
        }

        /// <summary>
        /// 下派信息返回包
        /// </summary>
        public struct ClientResp
        {
            public int MachineId;
            public enumErrorCode RespResult;
        }

        /// <summary>
        /// 设备设置
        /// </summary>
        public struct DeviceSetting
        {
            public int DeviceId;
            public int OperateType;
            public string RoomNumber;
            public string DeviceNumber;
            public string DeviceName;
            public string IPAddress;
        }

        /// <summary>
        /// 设备叫料
        /// </summary>
        public struct DeviceMaterial
        {
            public int MachineId;
            public string ScheduleNumber;
        }

        public struct DeviceStartEnd
        {
            public int MachineId;
            public string UserNumber;
            public string ScheduleNumber;
            public enumDeviceWorkStatus Status;
        }
    }
    #endregion
}

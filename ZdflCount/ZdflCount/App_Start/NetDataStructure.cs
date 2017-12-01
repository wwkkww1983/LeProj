using System;

namespace ZdflCount.App_Start
{
    /// <summary>
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
        public Models.enumDeviceWorkStatus Status;
    }
}

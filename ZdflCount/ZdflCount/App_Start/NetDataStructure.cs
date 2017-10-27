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
    }
    /// <summary>
    /// 各通道生产情况
    /// </summary>
    public struct ChannelInfo
    {
        public int PlanCount;
        public int Finish;
        public int Exception;
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
        public ChannelInfo Channel1;
        public ChannelInfo Channel2;
        public ChannelInfo Channel3;
        public ChannelInfo Channel4;
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
    /// 设备设置
    /// </summary>
    public struct DeviceSetting
    {
        public int OperateType;
        public string DeviceNumber;
        public string DeviceName;
        public string IPAddress;
    }
}

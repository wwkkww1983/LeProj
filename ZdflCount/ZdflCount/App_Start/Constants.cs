using System;

namespace ZdflCount.App_Start
{
    public class Constants
    {
    }

    /// <summary>
    /// 命令
    /// </summary>
    public enum enumCommandType
    {
        /// <summary>
        /// 施工单
        /// </summary>
        DOWN_SHEDULE_SEND = 601,
        DOWN_SHEDULE_RESP = 699,

        /// <summary>
        /// 心跳
        /// </summary>
        UP_HEART_SEND = 101,
        UP_HEART_RESP = 201,

        /// <summary>
        /// 生产情况
        /// </summary>
        UP_PRODUCT_SEND = 110,
        UP_PRODUCT_RESP = 210
    }

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
        public int ChannelCount;
        public string StaffNumber;
        public string StaffName;
        public ChannelInfo Channel1;
        public ChannelInfo Channel2;
        public ChannelInfo Channel3;
        public ChannelInfo Channel4;
    }
}

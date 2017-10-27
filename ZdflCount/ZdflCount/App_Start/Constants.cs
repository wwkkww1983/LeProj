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
        DOWN_SHEDULE_RESP = 602,

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
        /// 设备信息设置
        /// </summary>
        UP_DEVICE_SETTING_SEND = 301,
        UP_DEVICE_SETTING_RESP = 302
    }
}

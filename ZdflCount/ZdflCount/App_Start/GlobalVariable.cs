using System;
using System.Collections.Generic;
using ZdflCount.Models;

namespace ZdflCount.App_Start
{
    public class GlobalVariable 
    {
        /// <summary>
        /// 当前所有设备的工作状态（心跳包更新）
        /// </summary>
        public static List<DeviceStatus> deviceStatusList = new List<DeviceStatus>();

        /// <summary>
        /// 当前下派施工单的信息返回状态
        /// </summary>
        public static Dictionary<int, bool> DownScheduleWaitStatus = new Dictionary<int, bool>();

        /// <summary>
        /// 当前下派施工单的信息返回结果
        /// </summary>
        public static Dictionary<int, enumErrorCode> DownScheduleRespResult = new Dictionary<int, enumErrorCode>();

        /// <summary>
        /// 当前关闭施工单的信息返回状态
        /// </summary>
        public static Dictionary<int, bool> DownScheCloseWaitStatus = new Dictionary<int, bool>();

        /// <summary>
        /// 当前关闭施工单的信息返回结果
        /// </summary>
        public static Dictionary<int, enumErrorCode> DownScheCloseRespResult = new Dictionary<int, enumErrorCode>();

        /// <summary>
        /// 当前报废施工单的信息返回状态
        /// </summary>
        public static Dictionary<int, bool> DownScheDiscardWaitStatus = new Dictionary<int, bool>();

        /// <summary>
        /// 当前报废施工单的信息返回结果
        /// </summary>
        public static Dictionary<int, enumErrorCode> DownScheDiscardRespResult = new Dictionary<int, enumErrorCode>();
    }
}

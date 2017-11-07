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
        public static Dictionary<int, int> DownScheduleRespResult = new Dictionary<int, int>();
    }
}

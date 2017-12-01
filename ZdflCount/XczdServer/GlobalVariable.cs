using System;
using System.Collections.Generic;
using ServiceStack.Redis;

namespace XczdServer
{
    public class GlobalVariable 
    {
        /// <summary>
        /// 当前所有设备的工作状态（心跳包更新）
        /// </summary>
        //public static List<DeviceStatus> deviceStatusList = new List<DeviceStatus>();

        public const string PAGE_SEND_CONTENT = "PAGESENDCONTENT", PRE_DOWN_INFO_MACHINE = "PREDOWNMACIHNE", PRE_DOWN_INFO = "PREDOWNINFO",
                            PRE_RESP_DOWN_INFO = "PRERESPDOWNINFO",
                            PRE_INFO_TYPE_CREATE = "INFOTYPECREATE", PRE_INFO_TYPE_CLOSE = "INFOTYPECLOSE", PRE_INFO_TYPE_DISCARD = "INFOTYPEDISCARD";
        private static readonly string strIp = Ini.GetItemValue("redis", "server");
        private static readonly string strPort = Ini.GetItemValue("redis", "port");
        private static readonly string strIPPort = string.Format("{0}:{1}", strIp, strPort);
        private static readonly PooledRedisClientManager prcm = CreateManager(new string[] { strIPPort }, new string[] { strIPPort });
        public static IRedisClient RedisClient
        {
            get;
            private set;
        } 

        static GlobalVariable()
        {
            RedisClient = prcm.GetClient();
        }

        private static PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            // 支持读写分离，均衡负载 
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = 5, // “写”链接池链接数 
                MaxReadPoolSize = 5, // “读”链接池链接数 
                AutoStart = true,
            });
        }

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

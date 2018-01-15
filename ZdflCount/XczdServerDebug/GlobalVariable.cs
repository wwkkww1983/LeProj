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
        private static PooledRedisClientManager prcm = null;
        private readonly static object _lockObjectRedis = new object();
        //private static RedisClient client = null;
        public static IRedisClient RedisClient
        {
            get
            {
                if (prcm == null)
                {
                    lock (_lockObjectRedis)
                    {
                        if (prcm == null)
                        {
                            string strIPPort = string.Format("{0}:{1}", strIp, strPort);
                            // 支持读写分离，均衡负载 
                            prcm = new PooledRedisClientManager(new string[] { strIPPort }, new string[] { strIPPort }, new RedisClientManagerConfig
                            {
                                MaxWritePoolSize = 200, // “写”链接池链接数 
                                MaxReadPoolSize = 200, // “读”链接池链接数 
                                AutoStart = true,
                            });
                        }
                    }
                }
                return prcm.GetClient();
                //if (client == null)
                //{
                //    lock (_lockObjectRedis)
                //    {
                //        if (client == null)
                //        {
                //            client = RedisClientFactory.Instance.CreateRedisClient(strIp, int.Parse(strPort));
                //        }
                //    }
                //}
                //return client;
            }
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

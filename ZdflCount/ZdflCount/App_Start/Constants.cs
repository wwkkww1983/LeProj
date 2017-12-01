using System;
using System.ComponentModel;
using System.Collections.Generic;
using ServiceStack.Redis;

namespace ZdflCount.App_Start
{
    public class Constants
    {
        public const string MACHINE_MATERIAL_STRUCTURE = "物料：{0}（{1}）； 详细信息：{2}";
        private static string strRedisPort = System.Configuration.ConfigurationManager.AppSettings["InnerRedisServerPort"];
        private static Dictionary<enumErrorCode, string> errorKeyValue = new Dictionary<enumErrorCode, string>();

        public static RedisClient RedisClient
        {
            get
            {
                return new RedisClient("127.0.0.1", int.Parse(strRedisPort));
            }
        }
        static Constants()
        {

            errorKeyValue.Add(enumErrorCode.NONE, string.Empty);
            errorKeyValue.Add(enumErrorCode.HandlerSuccess, "操作成功");
            errorKeyValue.Add(enumErrorCode.FileOnlyExcel, "仅允许提交Excel文件");
            errorKeyValue.Add(enumErrorCode.FileFormatError, "文件格式错误");
            errorKeyValue.Add(enumErrorCode.ExcelHeadError, "Excel表头校验失败，请不要调整模版顺序");
            errorKeyValue.Add(enumErrorCode.ExcelContentError, "Excel部分内容校验失败，请检测数据格式");
            errorKeyValue.Add(enumErrorCode.DeviceNotWork, "设备没在工作中，不能下发信息");
            errorKeyValue.Add(enumErrorCode.DeviceRespFailInfo, "设备处理失败，请重新下派");
            errorKeyValue.Add(enumErrorCode.DeviceReciveTimeOut, "设备接受超时，请重新下派");
            errorKeyValue.Add(enumErrorCode.DeviceScheduleFull, "设备已有的施工单达到最大数量（10个）");
            errorKeyValue.Add(enumErrorCode.DeviceScheduleWorking, "设备正在为该施工单生产，无法进行操作");
            errorKeyValue.Add(enumErrorCode.DeviceCommunicateError, "设备通信失败，请联系信息部沟通");        
        }

        public static string GetErrorString(enumErrorCode code)
        {
            return errorKeyValue[code];
        }
    }

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
        [Description("操作成功")]
        NONE  = 0x00,

        /// <summary>
        /// 监听线程异常
        /// </summary>
        [Description("服务器监听程序启动失败")]
        TcpListenerException,

        /// <summary>
        /// TCP监听线程模块
        /// </summary>
        [Description("监听线程错误")]
        TcpRecieveErr,

        /// <summary>
        /// TCP接受后，处理程序
        /// </summary>
        [Description("监听处理异常")]
        TcpHandlerException,

        /// <summary>
        /// 设备连接处理异常
        /// </summary>
        [Description("设备连接处理异常")]
        TcpMachineStreamException,

        /// <summary>
        /// TCP接受编码找不到，使用了默认处理程序处理
        /// </summary>
        [Description("命令码错误")]
        TcpDefaultHandlerErr,

        /// <summary>
        /// 给客户端发送模块
        /// </summary>
        [Description("设备通信异常")]
        TcpSenderException,

        /// <summary>
        /// 生产信息退出登录员工不同
        /// </summary>
        [Description("生产设备退出和登录员工不同")]
        ProductOutInDiff,
        [Description("生产设备退出没有对应的登录信息")]
        ProductOutWithoutIn
    }

    /// <summary>
    /// 员工角色
    /// </summary>
    public enum enumUserRole
    {
        超级管理员 = 0x01,

        员工信息管理员,

        施工单管理员,

        员工权限管理员,

        订单加载,

        施工单查看,

        施工单创建,

        施工单修改,

        施工单下派,

        施工单关闭,

        施工单报废,

        批量导入员工,

        单个新增员工,

        修改员工信息,

        删除员工信息,

        生产统计数据查看,
        
        系统管理员,

        报料管理员,

        报修管理员,

        启停管理员
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
}

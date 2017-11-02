using System;
using System.Collections.Generic;

namespace ZdflCount.App_Start
{
    public class Constants
    {
        private static Dictionary<enumErrorCode, string> errorKeyValue = new Dictionary<enumErrorCode, string>();
        static Constants()
        {
            errorKeyValue.Add(enumErrorCode.NONE, string.Empty);
            errorKeyValue.Add(enumErrorCode.FileOnlyExcel, "仅允许提交Excel文件");
            errorKeyValue.Add(enumErrorCode.FileFormatError, "文件格式错误");
            errorKeyValue.Add(enumErrorCode.ExcelHeadError, "Excel表头校验失败");
            errorKeyValue.Add(enumErrorCode.ExcelContentError, "Excel部分内容校验失败");         
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

    /// <summary>
    /// 返回给客户端的错误码
    /// </summary>
    public enum enumErrorCode
    {
        NONE = 0x00,

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
        ExcelContentError

    }


    /// <summary>
    /// 系统自身错误码
    /// </summary>
    public enum enumSystemErrorCode
    {
        /// <summary>
        /// 监听线程异常
        /// </summary>
        TcpListenerException = 0x00,

        /// <summary>
        /// TCP监听处理模块
        /// </summary>
        TcpRecieveErr,

        /// <summary>
        /// TCP接受后，处理程序
        /// </summary>
        TcpHandlerException,

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
        ProductOutInDiff
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
        
        系统管理员
    }

    /// <summary>
    /// 生产数据类型
    /// </summary>
    public enum enumProductType
    {
        LoginIn = 0x00,

        LoginOut
    }
}

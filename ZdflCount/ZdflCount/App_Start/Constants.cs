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
    /// 错误码
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
        ExcelHeadError

    }


}

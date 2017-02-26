using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamInter
{
    /// <summary>
    /// 中间环类型
    /// </summary>
    public enum EnumType
    {
        /// <summary>
        /// 镜头
        /// </summary>
        CamLens = 0x00,

        /// <summary>
        /// 调焦环
        /// </summary>
        Focus ,

        /// <summary>
        /// 转接环
        /// </summary>
        Adapter,

        /// <summary>
        /// 延长环
        /// </summary>
        Extend

    }

    /// <summary>
    /// 常量
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// 镜头
        /// </summary>
        public const string CAM_LENS = "镜头";

        /// <summary>
        /// 调焦环
        /// </summary>
        public const string FOCUS = "调焦环";

        /// <summary>
        /// 转接环
        /// </summary>
        public const string ADAPTER = "转接环";

        /// <summary>
        /// 延长环
        /// </summary>
        public const string EXTEND = "延长环";

    }
}

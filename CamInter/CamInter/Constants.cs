using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamInter
{
    /// <summary>
    /// 产品类型
    /// </summary>
    public enum enumProductType
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
        Extend,

        /// <summary>
        /// 接口
        /// </summary>
        Interface
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

        /// <summary>
        /// 延长环
        /// </summary>
        public const string INTERFACE = "接口";

    }

    /// <summary>
    /// 中间环计算结果
    /// </summary>
    public struct RingResult
    {
        /// <summary>
        /// 方案序号
        /// </summary>
        string Idx;

        /// <summary>
        /// 调焦环（最多一个）
        /// </summary>
        RingMedium Focus;

        /// <summary>
        /// 转接环数量
        /// </summary>
        int AdapterCount;

        /// <summary>
        /// 转接环列表
        /// </summary>
        List<RingMedium> AdapterList;

        /// <summary>
        /// 延长环数量
        /// </summary>
        int ExtendCount;

        /// <summary>
        /// 延长环列表
        /// </summary>
        List<RingMedium> ExtendList;
    }
}

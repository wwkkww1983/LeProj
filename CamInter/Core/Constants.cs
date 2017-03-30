using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
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
        Interface,

        /// <summary>
        /// 相机
        /// </summary>
        Camera
    }

    /// <summary>
    /// 常量
    /// </summary>
    public class Constants
    {
        public static string GetNameByType(enumProductType type)
        {
            string strTypeName = string.Empty;
            switch (type)
            {
                case enumProductType.Focus: strTypeName = FOCUS; break;
                case enumProductType.Adapter: strTypeName = ADAPTER; break;
                case enumProductType.Extend: strTypeName = EXTEND; break;
                case enumProductType.Interface: strTypeName = INTERFACE; break;
                case enumProductType.CamLens: strTypeName = CAM_LENS; break;
                default: break;
            }
            return strTypeName;
        }

        /// <summary>
        /// 镜头
        /// </summary>
        private const string CAM_LENS = "镜头";

        /// <summary>
        /// 调焦环
        /// </summary>
        private const string FOCUS = "调焦环";

        /// <summary>
        /// 转接环
        /// </summary>
        private const string ADAPTER = "转接环";

        /// <summary>
        /// 延长环
        /// </summary>
        private const string EXTEND = "延长环";

        /// <summary>
        /// 延长环
        /// </summary>
        private const string INTERFACE = "接口";

    }

    /// <summary>
    /// 中间环计算结果
    /// </summary>
    public struct RingResult
    {
        /// <summary>
        /// 方案序号
        /// </summary>
        public int Idx;

        /// <summary>
        /// 镜头（有且仅有一个）
        /// </summary>
        public CameraLens Lens;

        /// <summary>
        /// 调焦环（最多一个）
        /// </summary>
        public RingMedium Focus;

        /// <summary>
        /// 转接环列表
        /// </summary>
        public List<RingMedium> AdapterList;

        /// <summary>
        /// 延长环
        /// </summary>
        public List<RingMedium> ExtendList;

        /// <summary>
        /// 放大倍率
        /// </summary>
        public float Ratio;

        /// <summary>
        /// 工作距离
        /// </summary>
        public float WorkDistance;

        /// <summary>
        /// 视野尺寸
        /// </summary>
        public float FovLength;
        
        /// <summary>
        /// 视野尺寸
        /// </summary>
        public float FovWidth;

    }


    /// <summary>
    /// 中间环计算结果
    /// </summary>
    public struct RingResults
    {
        /// <summary>
        /// 方案序号
        /// </summary>
        public int Idx;

        /// <summary>
        /// 镜头（有且仅有一个）
        /// </summary>
        public CameraLens Lens;

        /// <summary>
        /// 中间环列表
        /// </summary>
        public List<RingMedium> ringList;

        /// <summary>
        /// 放大倍率
        /// </summary>
        public float Ratio;

        /// <summary>
        /// 工作距离
        /// </summary>
        public float WorkDistance;

        /// <summary>
        /// 视野尺寸
        /// </summary>
        public float FovLength;

        /// <summary>
        /// 视野尺寸
        /// </summary>
        public float FovWidth;

    }
}

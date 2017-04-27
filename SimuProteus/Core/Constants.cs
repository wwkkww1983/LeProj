using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuProteus
{
    public struct Constants
    {
        static string m_directory = System.Environment.CurrentDirectory;

        /// <summary>
        /// 当前程序目录
        /// </summary>
        public static string CurrentDirectory
        {
            get
            {
                return m_directory;
            }
        }
        /// <summary>
        /// 引脚尺寸
        /// </summary>
        public static int FOOT_SIZE_PIXEL
        {
            get
            {
                return int.Parse(Ini.GetItemValue("sizeInfo", "pixelFootSize"));
            }
        }
        /// <summary>
        /// 线宽
        /// </summary>
        public static int LINE_LINK_WIDTH
        {
            get
            {
                return int.Parse(Ini.GetItemValue("sizeInfo", "pixelLineWidth"));
            }
        }

        /// <summary>
        /// 线条选中标志的方块尺寸
        /// </summary>
        public const int SELECT_LINE_BULK_SIZE = 3;

        /// <summary>
        /// 元器件选中标志的方块尺寸
        /// </summary>
        public const int SELECT_ELEMENT_BULK_SIZE = 10;

        /// <summary>
        /// 元器件选中标志的方块颜色
        /// </summary>
        public static System.Drawing.Color SELECT_BULK_COLOR = System.Drawing.Color.Blue;

        /// <summary>
        /// 串口状态
        /// </summary>
        public static bool SeiralPortStatusIsOpen = false;

        /// <summary>
        /// 元器件持续时间
        /// </summary>
        public static int ElementStaySeconds = 5;
    }

    public enum enumComponent
    {
        /// <summary>
        /// 箭头
        /// </summary>
        NONE = 0x00,

        /// <summary>
        /// 电容
        /// </summary>
        Capacitor,

        /// <summary>
        /// 电阻
        /// </summary>
        Resistance,

        /// <summary>
        /// 接地
        /// </summary>
        Land,

        /// <summary>
        /// 二极管
        /// </summary>
        Diode,

        /// <summary>
        /// 三极管
        /// </summary>
        Triode,

        /// <summary>
        /// 运算放大器
        /// </summary>
        Amplifier
    }

    /// <summary>
    /// 组件类型
    /// </summary>
    public enum enumComponentType
    {
        /// <summary>
        /// 芯片
        /// </summary>
        Chips = 0x01,
        /// <summary>
        /// 一般元器件
        /// </summary>
        NormalComponent
    }

    /// <summary>
    /// 引脚类型
    /// </summary>
    public enum enumPinsType
    {
        Input = 0x00,
        Output,
        Power,
        OD,
        OC
    }

    /// <summary>
    /// 节点类型
    /// </summary>
    public enum enumNetPointType
    {
        VCC = 0x01,
        GND,
        NONE,
        Element,
        DeleteLine
    }
}

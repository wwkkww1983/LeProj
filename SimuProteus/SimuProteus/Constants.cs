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
        public const int FOOT_SIZE_PIXEL = 6;
        /// <summary>
        /// 线宽
        /// </summary>
        public const int LINE_LINK_WIDTH = 5;
    }

    /// <summary>
    /// 组件类型
    /// </summary>
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
        Land
    }

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
}

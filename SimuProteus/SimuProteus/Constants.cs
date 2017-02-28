﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuProteus
{
    public struct Constants
    {
        static string m_directory = System.Environment.CurrentDirectory;
        static int m_footSize = int.Parse(Ini.GetItemValue("sizeInfo", "pixelFootSize"));
        static int m_lineWidth = int.Parse(Ini.GetItemValue("sizeInfo", "pixelLineWidth"));

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
                return m_footSize;
            }
        }
        /// <summary>
        /// 线宽
        /// </summary>
        public static int LINE_LINK_WIDTH
        {
            get
            {
                return m_lineWidth;
            }
        }

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
        Land
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
        Input,
        Output,
        Power,
        OD,
        OC
    }
}

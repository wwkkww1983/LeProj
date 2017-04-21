using System;
using System.Drawing;
using System.Collections.Generic;

namespace SimuProteus
{
    /// <summary>
    /// 元器件属性信息
    /// </summary>
    public class ElementInfo:ICloneable
    {
        public ElementInfo()
        {
            this.BackColor = Color.Gray;
            //默认图片
            this.BackImage = System.Environment.CurrentDirectory + "\\circle.png";
        }

        /// <summary>
        /// 参数默认值
        /// </summary>
        public ElementInfo(Size size, Point location)
        {
            this.Size = size;
            this.Location = location;
            this.BackColor = Color.Gray;
            this.BackImage = string.Empty;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// 元器件类型
        /// </summary>
        public enumComponentType FootType
        {
            get;
            set;
        }

        /// <summary>
        /// 组件编号
        /// </summary>
        public int Component
        {
            get;
            set;
        }

        /// <summary>
        /// 项目内索引序号
        /// </summary>
        public int InnerIdx
        {
            get;
            set;
        }

        /// <summary>
        /// 索引序号
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// ID号码
        /// </summary>
        public string Number
        {
            get;
            set;
        }

        /// <summary>
        /// 元器件名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 元器件显示尺寸
        /// </summary>
        public Size Size
        {
            get;
            set;
        }

        /// <summary>
        /// 位置
        /// </summary>
        public Point Location
        {
            get;
            set;
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor
        {
            get;
            set;
        }

        /// <summary>
        /// 元器件图片路径
        /// </summary>
        public string BackImage
        {
            get;
            set;
        }

        /// <summary>
        /// 元器件管脚
        /// </summary>
        public List<LineFoot> LineFoots
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 管脚
    /// </summary>
    public struct LineFoot
    {
        public int Idx;
        public int InnerIdx;
        public enumPinsType PinsType;
        public int LocX;
        public int LocY;
        public Color Color;
        public string Name;
        public int NameLocX;
        public int NameLocY;
    }

    /// <summary>
    /// 管脚属性
    /// </summary>
    public struct LineFootView
    {
        public int Idx;
        public int InnerID;
        public int Component;
        public int Element;
        public int InnerIdx;
        public int Foot;
        public enumPinsType PinsType;
        public string PinsName;
        public int PointX;
        public int PointY;
    }

    /// <summary>
    /// 元器件之间的连线
    /// </summary>
    public struct ElementLine
    {
        public int Idx;
        public int InnerIdx;
        public int LineIdx;
        public int oneElement;
        public int otherElement;
        public int oneFoot;
        public int otherFoot;
        public int LocX;
        public int LocY;
        public int LocOtherX;
        public int LocOtherY;
        public enumNetPointType PointType;
        public Color Color;
        public string Name;
    }

    /// <summary>
    /// 项目基本信息
    /// </summary>
    public struct ProjectInfo
    {
        public int Idx;
        public string Name;
        public int OriginX;
        public int OriginY;
        public DateTime CreateTime;
        public DateTime UpdateTime;
        public int Chips;
        public string Description;
    }

    /// <summary>
    /// 项目详情
    /// </summary>
    public class ProjectDetails
    {
        /// <summary>
        /// 项目基本信息
        /// </summary>
        public ProjectInfo Project;
        /// <summary>
        /// 连线信息
        /// </summary>
        public List<ElementLine> linesList;
        /// <summary>
        /// 元器件信息
        /// </summary>
        public List<ElementInfo> elementList;
        /// <summary>
        /// 引脚信息（修改后的参数）
        /// </summary>
        public List<LineFootView> footsList;
        /// <summary>
        /// 节点信息（VCC/GND）
        /// </summary>
        public List<NetPoint> pointsList;
    }

    /// <summary>
    /// 串口信息
    /// </summary>
    public struct SerialInfo
    {
        public string PortName;
        public int BaudRate;
        public int Parity;
        public int DataBits;
        public int StopBits;
        public int TimeOut;
    }

    /// <summary>
    /// 网络节点信息
    /// </summary>
    public struct NetPoint
    {
        public int Idx;
        public int X;
        public int Y;
        public enumNetPointType Type;
    }

    /// <summary>
    /// 跟下位机通信结构
    /// </summary>
    public struct CommunicateInfo
    {
        public int ID;
        public string Number;
        public byte X;
        public byte Y;
        public DateTime Time;
        public bool DirectUp;
        public string Content;
    }
}

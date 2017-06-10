using System.Drawing;

namespace Core
{
    public class Constants
    {
        /// <summary>
        /// 视频文件格式
        /// </summary>
        public const string Second_NAME_FORMAT = "yyyyMMddHHmmss";

        /// <summary>
        /// 图片文件格式
        /// </summary>
        public const string MiloSecond_NAME_FORMAT = "yyyyMMddHHmmssfff";

        /// <summary>
        /// 视频文件夹目录
        /// </summary>
        public const string VIDEO_FOLDER = "video/";

        /// <summary>
        /// 图片文件夹目录
        /// </summary>
        public const string IMAGE_FOLDER = "image/";

        /// <summary>
        /// 视频文件格式
        /// </summary>
        public const string VIDEO_FORMAT = ".avi";

        /// <summary>
        /// 图片文件格式
        /// </summary>
        public const string IMAGE_FORMAT = ".jpg";

        /// <summary>
        /// 图片宽度
        /// </summary>
        public const int IMAGE_WIDTH = 640;

        /// <summary>
        /// 图片高度
        /// </summary>
        public const int IMAGE_HEIGHT = 480;
    }

    public class LineInfo
    {
        public int Idx
        {
            get;
            set;
        }

        public Point One
        {
            get;
            set;
        }

        public Point Other
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }
    }

    public enum EnumLineType
    {
        None = 0x00,

        UserLink,

        HVLine
    }
}

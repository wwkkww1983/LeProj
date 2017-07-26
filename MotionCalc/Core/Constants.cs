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
        public static int IMAGE_WIDTH = 640;

        /// <summary>
        /// 图片高度
        /// </summary>
        public static int IMAGE_HEIGHT = 480;

        /// <summary>
        /// 是否显示网格线
        /// </summary>
        public static bool ShowNetFlag = false;

        /// <summary>
        /// 是否启动自动识别颜色
        /// </summary>
        public static bool AutoRecogColorFlag = false;

        /// <summary>
        /// 网格线宽
        /// </summary>
        public static int NetLineWidth = 2;

        /// <summary>
        /// 网格线间距
        /// </summary>
        public static int LineSeperationWidth = 50;

        /// <summary>
        /// 网格线间距
        /// </summary>
        public static int LineSeperationHeight = 50;

        /// <summary>
        /// 被识别点半径
        /// </summary>
        public static int RecogCircleRadiusBK = 8;

        /// <summary>
        /// 被识别点内圆半径
        /// </summary>
        public static int RecogCircleRadiusInner = 4;

        /// <summary>
        /// 被识别点内圆线宽
        /// </summary>
        public static int RecogCircleWidthInner = 2;

        /// <summary>
        /// 被选中的线宽
        /// </summary>
        public static int LineWidthSelected = 2;

        /// <summary>
        /// 标签成像最小面积
        /// </summary>
        public static int MinRecogRectArea = 300;

        /// <summary>
        /// 标签成像最大面积
        /// </summary>
        public static int MaxRecogRectArea = 1500;

        /// <summary>
        /// 最小标签成像宽高比
        /// </summary>
        public static float MinRecogRectWHRatio = -1;

        /// <summary>
        /// 最大标签成像宽高比
        /// </summary>
        public static float MaxRecogRectWHRatio = -1;

        /// <summary>
        /// 标签颜色
        /// </summary>
        public static int LabelColor = -256;

        /// <summary>
        /// 网格线条颜色
        /// </summary>
        public static Color ColorNetLine = Color.Gray;

        /// <summary>
        /// 被识别点的内圆颜色
        /// </summary>
        public static Color RecogCircleColorInner = Color.Black;

        /// <summary>
        /// 被识别点的整体背景色
        /// </summary>
        public static Color RecogCircleColorBK = Color.Yellow;

        /// <summary>
        /// 选中线条标记线
        /// </summary>
        public static Color LineColorSelected = Color.Black;

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

        /// <summary>
        /// 是否显示（被用户隐藏后为false）
        /// </summary>
        public bool ShowFlag
        {
            get;
            set;
        }
    }
}

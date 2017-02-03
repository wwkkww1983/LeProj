using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web2Excel
{
    /// <summary>
    /// 数据内容条目
    /// </summary>
    public struct Content
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName;

        /// <summary>
        /// 地址
        /// </summary>
        public string Address;

        /// <summary>
        /// 撞号（楼号）
        /// </summary>
        public string BlockNumber;

        /// <summary>
        /// 层高
        /// </summary>
        public string LayerCount;

        /// <summary>
        /// 所在层
        /// </summary>
        public string LayerNumber;

        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNumber;

        /// <summary>
        /// 户型
        /// </summary>
        public string RoomType;

        /// <summary>
        /// 用途
        /// </summary>
        public string FunctionName;

        /// <summary>
        /// 建筑面积
        /// </summary>
        public string AreaBuilding;

        /// <summary>
        /// 套内面积
        /// </summary>
        public string AreaRoom;

        /// <summary>
        /// 分摊面积
        /// </summary>
        public string AreaPublic;

        /// <summary>
        /// 房屋状态
        /// </summary>
        public string HouseStatus;

        /// <summary>
        /// 预售/现售
        /// </summary>
        public string IsPrepare;

        /// <summary>
        /// 成交日期
        /// </summary>
        public string DealDate;
    }

    /// <summary>
    /// 异常
    /// </summary>
    public enum ErrorInfo
    {
        OK,

        YesterFileNoExists,

        TodayFileNoExists,

        ERROR
    }

    /// <summary>
    /// 选择的户型
    /// </summary>
    public enum HouseType
    {
        NONE,

        ALL,

        CANSELL,

        COMPARE
    }
}

using System;
using System.Data;

namespace CamInter
{
    /// <summary>
    /// 中间环属性
    /// </summary>
    class tbRingMedium : tbGeneral
    {
        public ValueType DecodeOneItemByDb(DataRow dr)
        {
            RingMedium info = new RingMedium();

            info.Idx = Convert.ToInt32(dr["lineIdx"]);
            info.RingType = (enumProductType)Convert.ToInt32(dr["ringType"]);
            info.InterUp = Convert.ToInt32(dr["interUp"]);
            info.InterDown = Convert.ToInt32(dr["interDown"]);
            info.InnerDiameter = Convert.ToInt32(dr["innerDiameter"]);
            info.Weight = Convert.ToInt32(dr["weight"]);
            info.LengthMin = Convert.ToInt32(dr["lengthMin"]);
            info.LengthMax = Convert.ToInt32(dr["lengthMax"]);

            return info;
        }

        public string CreateTableStructure()
        {
            return @"create table ringMedium (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ringType int,
                        interUp int,
                        interDown int,
                        innerDiameter int,
                        weight int,
                        lengthMin int,
                        lengthMax int);";
        }
    }


    /// <summary>
    /// 中间环信息
    /// </summary>
    public struct RingMedium
    {
        public int Idx;
        /// <summary>
        /// 中间环类型
        /// </summary>
        public enumProductType RingType;
        /// <summary>
        /// 上端接口（A - 镜头）
        /// </summary>
        public int InterUp;
        /// <summary>
        /// 下端接口（B - 相机）
        /// </summary>
        public int InterDown;
        /// <summary>
        /// 内径
        /// </summary>
        public int InnerDiameter;
        /// <summary>
        /// 重量
        /// </summary>
        public int Weight;
        /// <summary>
        /// 延长范围（最小值）
        /// </summary>
        public int LengthMin;
        /// <summary>
        /// 延长范围（最大值）
        /// </summary>
        public int LengthMax;
    }
}

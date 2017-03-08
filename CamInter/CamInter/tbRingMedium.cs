using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SQLite;

namespace CamInter
{
    /// <summary>
    /// 中间环属性
    /// </summary>
    class tbRingMedium : itable
    {
        private string STR_CONNECTION;
        public tbRingMedium(string strConn)
        {
            this.STR_CONNECTION = strConn;
        }

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

        public bool InsertOneItem(ValueType item)
        {
            RingMedium info = (RingMedium)item;

            string strSql = "insert into camLens (ringType,interUp,interDown,innerDiameter,weight,lengthMin,lengthMax) values (@ringType,@interUp,@interDown,@innerDiameter,@weight,@lengthMin,@lengthMax)";
            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            paraList.Add(SQLiteHelper.CreateParameter("@ringType", DbType.Int32, (int)info.RingType));
            paraList.Add(SQLiteHelper.CreateParameter("@interUp", DbType.Int32, info.InterUp));
            paraList.Add(SQLiteHelper.CreateParameter("@interDown", DbType.Int32, info.InterDown));
            paraList.Add(SQLiteHelper.CreateParameter("@innerDiameter", DbType.Int32, info.InnerDiameter));
            paraList.Add(SQLiteHelper.CreateParameter("@weight", DbType.Int32, info.Weight));
            paraList.Add(SQLiteHelper.CreateParameter("@lengthMin", DbType.Int32, info.LengthMin));
            paraList.Add(SQLiteHelper.CreateParameter("@lengthMax", DbType.Int32, info.LengthMax));

            SQLiteCommand command = SQLiteHelper.CreateCommand(this.STR_CONNECTION, strSql, paraList.ToArray());
            return SQLiteHelper.ExecuteNonQuery(command) > 0;
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

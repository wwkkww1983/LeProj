using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Core
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

            info.Idx = Convert.ToInt32(dr["id"]);
            info.Name = dr["name"].ToString();
            info.ImgName = dr["imgName"].ToString();
            info.Number = dr["number"].ToString();
            info.RingType = (enumProductType)Convert.ToInt32(dr["ringType"]);
            info.InterUp = Convert.ToInt32(dr["interUp"]);
            info.InterDown = Convert.ToInt32(dr["interDown"]);
            info.InnerDiameter = Convert.ToInt32(dr["innerDiameter"]);
            info.Weight = Convert.ToInt32(dr["weight"]);
            info.Length = Convert.ToSingle(dr["length"]);
            info.LengthMin = Convert.ToSingle(dr["lengthMin"]);
            info.LengthMax = Convert.ToSingle(dr["lengthMax"]);

            return info;
        }

        public string CreateTableStructure()
        {
            return @"create table ringMedium (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name nvarchar(100),
                        imgName nvarchar(100),
                        number nvarchar(100),
                        ringType int,
                        interUp int,
                        interDown int,
                        innerDiameter int,
                        weight int,
                        length float,
                        lengthMin float,
                        lengthMax float);";
        }

        public bool InsertOneItem(ValueType item)
        {
            RingMedium info = (RingMedium)item;

            string strSql = "insert into ringMedium (name,imgName,number,ringType,interUp,interDown,innerDiameter,weight,length,lengthMin,lengthMax) values (@name,@imgName,@number,@ringType,@interUp,@interDown,@innerDiameter,@weight,@length,@lengthMin,@lengthMax)";
            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            paraList.Add(SQLiteHelper.CreateParameter("@name", DbType.String, info.Name));
            paraList.Add(SQLiteHelper.CreateParameter("@imgName", DbType.String, info.ImgName));
            paraList.Add(SQLiteHelper.CreateParameter("@number", DbType.String, info.Number));
            paraList.Add(SQLiteHelper.CreateParameter("@ringType", DbType.Int32, (int)info.RingType));
            paraList.Add(SQLiteHelper.CreateParameter("@interUp", DbType.Int32, info.InterUp));
            paraList.Add(SQLiteHelper.CreateParameter("@interDown", DbType.Int32, info.InterDown));
            paraList.Add(SQLiteHelper.CreateParameter("@innerDiameter", DbType.Int32, info.InnerDiameter));
            paraList.Add(SQLiteHelper.CreateParameter("@weight", DbType.Int32, info.Weight));
            paraList.Add(SQLiteHelper.CreateParameter("@length", DbType.Decimal, info.Length));
            paraList.Add(SQLiteHelper.CreateParameter("@lengthMin", DbType.Decimal, info.LengthMin));
            paraList.Add(SQLiteHelper.CreateParameter("@lengthMax", DbType.Decimal, info.LengthMax));

            SQLiteCommand command = SQLiteHelper.CreateCommand(this.STR_CONNECTION, strSql, paraList.ToArray());
            return SQLiteHelper.ExecuteNonQuery(command) > 0;
        }

        public List<ValueType> GetAllData()
        {
            string strSql = "select * from ringMedium";
            DataSet dsConnector = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            return new Coder().DecodeListByDb(dsConnector.Tables[0], this);
        }
    }


    /// <summary>
    /// 中间环信息
    /// </summary>
    public struct RingMedium
    {
        public int Idx;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 图片名
        /// </summary>
        public string ImgName;
        /// <summary>
        /// 货号
        /// </summary>
        public string Number;
        /// <summary>
        /// 中间环类型
        /// </summary>
        public enumProductType RingType;
        /// <summary>
        /// 上端接口（B - 相机）
        /// </summary>
        public int InterUp;
        /// <summary>
        /// 下端接口（A - 镜头）
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
        /// 长度（中间值）mm
        /// </summary>
        public float Length;
        /// <summary>
        /// 延长范围（最小值）
        /// </summary>
        public float LengthMin;
        /// <summary>
        /// 延长范围（最大值）
        /// </summary>
        public float LengthMax;
    }
}

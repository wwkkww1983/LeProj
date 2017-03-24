using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SQLite;

namespace CamInter
{
    
    /// <summary>
    /// 镜头属性
    /// </summary>
    class tbCamLens : itable
    {
        private string STR_CONNECTION;
        public tbCamLens(string strConn)
        {
            this.STR_CONNECTION = strConn;
        }

        public ValueType DecodeOneItemByDb(DataRow dr)
        {
            CameraLens info = new CameraLens();

            info.Idx = Convert.ToInt32(dr["id"]);
            info.Length = Convert.ToInt32(dr["length"]);
            info.FocusLength = Convert.ToInt32(dr["focusLength"]);
            info.FocusLengthBak = Convert.ToInt32(dr["focusLengthBak"]);
            info.Connector = Convert.ToInt32(dr["connector"]);
            info.Weight = Convert.ToInt32(dr["weight"]);
            info.Contrast = Convert.ToInt32(dr["contrast"]);
            info.TargetSurface = Convert.ToInt32(dr["targetSurface"]);
            info.Distort = Convert.ToInt32(dr["distort"]);

            return info;
        }

        public string CreateTableStructure()
        {
            return @"create table camLens(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        length int,
                        focusLength int,
                        focusLengthBak int,
                        connector int,
                        weight int,
                        contrast int,
                        targetSurface int,
                        distort int);";
        }

        public bool InsertOneItem(ValueType item)
        {
            CameraLens info = (CameraLens)item;

            string strSql = "insert into camLens (length,focusLength,focusLengthBak,connector,weight,contrast,targetSurface,distort) values (@length,@focusLength,@focusLengthBak,@connector,@weight,@contrast,@targetSurface,@distort)";
            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            paraList.Add(SQLiteHelper.CreateParameter("@length", DbType.Int32, info.Length));
            paraList.Add(SQLiteHelper.CreateParameter("@focusLength", DbType.Int32, info.FocusLength));
            paraList.Add(SQLiteHelper.CreateParameter("@focusLengthBak", DbType.Int32, info.FocusLengthBak));
            paraList.Add(SQLiteHelper.CreateParameter("@connector", DbType.Int32, info.Connector));
            paraList.Add(SQLiteHelper.CreateParameter("@weight", DbType.Int32, info.Weight));
            paraList.Add(SQLiteHelper.CreateParameter("@contrast", DbType.Int32, info.Contrast));
            paraList.Add(SQLiteHelper.CreateParameter("@targetSurface", DbType.Int32, info.TargetSurface));
            paraList.Add(SQLiteHelper.CreateParameter("@distort", DbType.Int32, info.Distort));

            SQLiteCommand command = SQLiteHelper.CreateCommand(this.STR_CONNECTION, strSql, paraList.ToArray());
            return SQLiteHelper.ExecuteNonQuery(command) > 0;
        }
        public List<ValueType> GetAllData()
        {
            string strSql = "select * from camLens";
            DataSet dsConnector = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            return new Coder().DecodeListByDb(dsConnector.Tables[0], this);
        }
    }


    /// <summary>
    /// 相机镜头
    /// </summary>
    public struct CameraLens
    {
        public int Idx;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 货号
        /// </summary>
        public string Number;
        /// <summary>
        /// 相机Sensor
        /// </summary>
        public int Sensor;
        /// <summary>
        /// 视野
        /// </summary>
        public int Fov;
        /// <summary>
        /// 接口
        /// </summary>
        public int Connector;
        /// <summary>
        /// 焦距
        /// </summary>
        public float Focus;
        /// <summary>
        /// 相机法兰距（后焦距）
        /// </summary>
        public int Flange;
        /// <summary>
        /// 最小放大倍率
        /// </summary>
        public float RatioMin;
        /// <summary>
        /// 最大放大倍率
        /// </summary>
        public float RatioMax;
        /// <summary>
        /// 最大靶面
        /// </summary>
        public int Target;
        /// <summary>
        /// 重量
        /// </summary>
        public int Weight;
        /// <summary>
        /// 空间频率
        /// </summary>
        public int Contrast;
        /// <summary>
        /// 畸变量
        /// </summary>
        public int Distort;
        /// <summary>
        /// 放大倍率
        /// </summary>
        public float Ratio;
    }
}

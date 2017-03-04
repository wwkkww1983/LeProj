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

            string strSql = "insert into camLens (length,targetSurface) values (@length,@targetSurface)";
            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            paraList.Add(SQLiteHelper.CreateParameter("@length", DbType.Int32, info.Length));
            paraList.Add(SQLiteHelper.CreateParameter("@targetSurface", DbType.Int16, info.TargetSurface));

            SQLiteCommand command = SQLiteHelper.CreateCommand(this.STR_CONNECTION, strSql, paraList.ToArray());
            return SQLiteHelper.ExecuteNonQuery(command) > 0;
        }
    }


    /// <summary>
    /// 相机镜头
    /// </summary>
    public struct CameraLens
    {
        public int Idx;
        /// <summary>
        /// 长度
        /// </summary>
        public int Length;
        /// <summary>
        /// 焦距
        /// </summary>
        public int FocusLength;
        /// <summary>
        /// 后焦距
        /// </summary>
        public int FocusLengthBak;
        /// <summary>
        /// 接口
        /// </summary>
        public int Connector;
        /// <summary>
        /// 重量
        /// </summary>
        public int Weight;
        /// <summary>
        /// 空间频率
        /// </summary>
        public int Contrast;
        /// <summary>
        /// 靶面
        /// </summary>
        public int TargetSurface;
        /// <summary>
        /// 畸变量
        /// </summary>
        public int Distort;
    }
}

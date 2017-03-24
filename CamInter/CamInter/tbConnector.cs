using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SQLite;

namespace CamInter
{
    /// <summary>
    /// 接口
    /// </summary>
    class tbConnector : itable
    {
        private string STR_CONNECTION;
        public tbConnector(string strConn)
        {
            this.STR_CONNECTION = strConn;
        }

        public ValueType DecodeOneItemByDb(DataRow dr)
        {
            Connectors info = new Connectors();

            info.Idx = Convert.ToInt32(dr["itemId"]);
            info.Name = dr["name"].ToString();
            info.Description = dr["desc"].ToString();

            return info;
        }

        public string CreateTableStructure()
        {
            return @"create table connectors (
                        itemId INTEGER PRIMARY KEY AUTOINCREMENT,
                        name nvarchar(20),
                        desc varchar(100));";
        }

        public List<ValueType> GetAllData()
        {
            string strSql = "select * from connectors";
            DataSet dsConnector = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            return new Coder().DecodeListByDb(dsConnector.Tables[0], this);
        }

        public bool InsertOneItem(ValueType item)
        {
            Connectors info = (Connectors)item;
            string strSql = "insert into connectors (name,desc) values (@name,@desc)";
            List<SQLiteParameter> paraList = new List<SQLiteParameter> ();
            paraList.Add(SQLiteHelper.CreateParameter("@name", DbType.String, info.Name));
            paraList.Add(SQLiteHelper.CreateParameter("@desc", DbType.String, info.Description));

            SQLiteCommand command = SQLiteHelper.CreateCommand(this.STR_CONNECTION, strSql, paraList.ToArray());
            return SQLiteHelper.ExecuteNonQuery(command) > 0;
        }
    }

    /// <summary>
    /// 接口
    /// </summary>
    public struct Connectors
    {
        public int Idx;
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 详细备注
        /// </summary>
        public string Description;
    }
}

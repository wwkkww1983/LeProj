using System;
using System.Data;
using System.Drawing;
using System.Data.SQLite;
using System.Collections.Generic;


namespace CameraAPPServer
{
    public class DBUtility
    {
        private string strPath;
        private static string STR_CONNECTION ;

        public DBUtility()
        {
        }

        public DBUtility(string strPath, bool dbEncrypt)
        {
            this.strPath = strPath;
            STR_CONNECTION = string.Format("Data Source={0}vision.s;Version=3;", this.strPath);

            SQLiteHelper.SetPassWordFlag = dbEncrypt;
            if(dbEncrypt)
                SQLiteHelper.SetSignature = "hudongDOTnet";
        }

        /// <summary>
        /// 新建表结构
        /// </summary>
        /// <returns></returns>
        public void InitialTable()
        {
            List<string> tableList = this.CreateTableStruct();
            string strSql = string.Empty;
            foreach (string strItem in tableList)
            {
                strSql += strItem;
            }

            SQLiteHelper.CreateDatabase(this.strPath + "vision.s");
            SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql);
        }

        private List<string> CreateTableStruct()
        {
            List<string> tableList = new List<string>();
            //新建的项目
            tableList.Add(@"create table users(
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               name nvarchar(50),
                               pwd nvarchar(50),
                               createtime datetime,
                               desc nvarchar(100));");

            //网格节点
            tableList.Add(@"create table loginRecord(
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               userId INTEGER,
                               logintime datetime);");
            return tableList;
        }

        public bool Login(string name ,string pwd)
        {
            string strSql = "select id from users where name=@name and pwd=@pwd";
            string[] strArray = new string[] {name,pwd };

            object objCount = SQLiteHelper.ExecuteScalar(STR_CONNECTION, strSql,strArray);
            if (objCount == null) return false;

            int count = Convert.ToInt32(objCount);
            strSql = string.Format("insert into loginRecord (userId,logintime) values ({0},'{1}')", count.ToString(),DateTime.Now.ToString());
            SQLiteHelper.ExecuteNonQuery(STR_CONNECTION,strSql);
            return true;
        }

        public bool Register(string name, string pwd)
        {
            string strSql = "select id from users where name=@name";
            string[] strArray = { name};
            object objCount = SQLiteHelper.ExecuteScalar(STR_CONNECTION, strSql, strArray);
            if (objCount != null) return false;


            strSql = "insert into users (name,pwd,createtime,desc) values (@name,@pwd,@createtime,@desc)";
            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            paraList.Add(SQLiteHelper.CreateParameter("@name", DbType.String, name));
            paraList.Add(SQLiteHelper.CreateParameter("@pwd", DbType.String, pwd));
            paraList.Add(SQLiteHelper.CreateParameter("@createtime", DbType.DateTime, DateTime.Now));
            paraList.Add(SQLiteHelper.CreateParameter("@desc", DbType.String, "无"));


            SQLiteCommand command = SQLiteHelper.CreateCommand(STR_CONNECTION,strSql,paraList.ToArray());
            return SQLiteHelper.ExecuteNonQuery(command) > 0;
        }
    }
}
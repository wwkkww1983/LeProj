using System;
using System.Data;
using System.Drawing;
using System.Data.SQLite;
using System.Collections.Generic;

namespace CamInter
{
    class DBUtility
    {
        private const string STR_CONNECTION = "Data Source=cam.s;Version=3;";
        private Coder code = new Coder();
        private tbCamLens dbCamlens = new tbCamLens(STR_CONNECTION);
        private tbConnector dbConnector = new tbConnector(STR_CONNECTION);
        private tbRingMedium dbRingMedium = new tbRingMedium(STR_CONNECTION);

        public DBUtility()
        {

        }

        public DBUtility(bool dbEncrypt)
        {
            SQLiteHelper.SetPassWordFlag = dbEncrypt;
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

            SQLiteHelper.CreateDatabase("cam.s");
            SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql);
            this.InitialTableData();
        }

        private List<string> CreateTableStruct()
        {
            List<string> tableList = new List<string>();
            //接口
            tableList.Add(dbCamlens.CreateTableStructure());
            //镜头属性
            tableList.Add(dbConnector.CreateTableStructure());
            //中间环属性
            tableList.Add(dbRingMedium.CreateTableStructure());
            return tableList;
        }

        private void InitialTableData()
        {
            this.InsertItem(new Connectors() { Name = "M42" });
            this.InsertItem(new Connectors() { Name = "M58" });
            this.InsertItem(new Connectors() { Name = "M72" });
            this.InsertItem(new Connectors() { Name = "M90" });
            this.InsertItem(new Connectors() { Name = "V38" });
            this.InsertItem(new Connectors() { Name = "V70" });
        }

        /// <summary>
        /// 获取当前所有接口
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllConnector()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Idx");
            dt.Columns.Add(dc);
            dc = new DataColumn("Name");
            dt.Columns.Add(dc);
            itable handler = this.GetTableHandlerByType(enumProductType.Interface);
            List<ValueType> list = handler.GetAllData();
            foreach (ValueType item in list)
            {                
                DataRow dr = dt.NewRow();
                dr["Idx"] = ((Connectors)item).Idx;
                dr["Name"] = ((Connectors)item).Name;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private List<ValueType> GetDataList(string strSql, itable table)
        {
            DataSet dsConnector = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);

            return code.DecodeListByDb(dsConnector.Tables[0], table);
        }

        public bool InsertItem(ValueType info)
        {
            itable handler = this.GetTableHandlerByType(info.GetType());
            return handler.InsertOneItem(info);
        }

        private itable GetTableHandlerByType(Type itemType)
        {
            itable result = null;
            if (itemType == typeof(CameraLens))
                result = dbCamlens;
            else if (itemType == typeof(Connectors))
                result = dbConnector;
            else if (itemType == typeof(RingMedium))
                result = dbRingMedium;

            return result;
        }

        private itable GetTableHandlerByType(enumProductType itemType)
        {
            itable result = null;
            switch (itemType)
            {
                case enumProductType.Adapter: 
                case enumProductType.Extend:
                case enumProductType.Focus: result = dbRingMedium; break;
                case enumProductType.Interface: result = dbConnector; break;
                case enumProductType.CamLens: result = dbCamlens; break;
                default: break;
            }

            return result;
        }

//        public List<ProjectInfo> GetAllProjects()
//        {
//            string strSql = "select * from projects";
//            DataSet ds = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
//            return code.DecodeProjectsByDb(ds);
//        }

//        public List<LineFoot> GetChipFoots(int chipIdx)
//        {
//            string strSql = string.Format("select * from lineFoot where footType={0} and component={1}", (int)enumComponentType.Chips, chipIdx);
//            DataSet dsFoots = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);

//            return code.DecodeCompFootsByDb(dsFoots.Tables[0], chipIdx);
//        }

//        public bool CheckProjectNameExists(string projName)
//        {
//            string strSql = string.Format("select id from projects where name='{0}'", projName) ;
//            object objId = SQLiteHelper.ExecuteScalar(STR_CONNECTION, strSql);
//            return Convert.ToInt32(objId) > 0;
//        }

//        /// <summary>
//        /// 新增元器件
//        /// </summary>
//        /// <param name="info"></param>
//        /// <returns></returns>
//        private bool AddNewBaseComponent(ElementInfo info)
//        {
//            string strSql = string.Format(@"insert into components (name,width,height,backColor,backImage) 
//                                                values ('{0}',{1},{2},{3},'{4}');select last_insert_rowid();",
//                                info.Name, info.Size.Width, info.Size.Height, info.BackColor.ToArgb(), info.BackImage);
//            object objIdx = SQLiteHelper.ExecuteScalar(STR_CONNECTION, strSql);

//            return this.AddComponentFoots(Convert.ToInt32(objIdx), info.FootType, info.LineFoots);
//        }

    }
}
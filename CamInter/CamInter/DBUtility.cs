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

            SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql);
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


        /// <summary>
        /// 获取当前所有接口
        /// </summary>
        /// <returns></returns>
        public List<ValueType> GetAllConnector()
        {
            string strSql = "select * from connectors";
            DataSet dsConnector = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);

            return code.DecodeListByDb(dsConnector.Tables[0], dbConnector);
        }

        public void InsertItem(ValueType info)
        {
            itable handler = GetTableHandlerByType(info.GetType());
            handler.InsertOneItem(info);
        }

        public itable GetTableHandlerByType(Type itemType)
        {
            itable result = null;
            if(itemType == typeof(CameraLens))            
                result = dbCamlens;
            else if (itemType == typeof(Connectors))
                result = dbConnector;

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
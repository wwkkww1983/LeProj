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
            #region 接口
            this.InsertItem(new Connectors() { Name = "M42", Idx=1 });
            this.InsertItem(new Connectors() { Name = "M58", Idx = 2 });
            this.InsertItem(new Connectors() { Name = "M72", Idx = 3 });
            this.InsertItem(new Connectors() { Name = "M90", Idx = 4});
            this.InsertItem(new Connectors() { Name = "V38", Idx = 5 });
            this.InsertItem(new Connectors() { Name = "V70", Idx = 6 });
            this.InsertItem(new Connectors() { Name = "C", Idx = 7 });
            this.InsertItem(new Connectors() { Name = "F", Idx = 8 });
            this.InsertItem(new Connectors() { Name = "Basler", Idx = 9 });
            this.InsertItem(new Connectors() { Name = "M95", Idx = 10 });
            #endregion

            #region 调焦环
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus 5",
                Number = "902002A",
                Length = 18.3f,
                LengthMin = 15.8f,
                LengthMax = 20.8f,
                InterUp = 5,
                InterDown = 9
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus 7(M72)",
                Number = "902004A",
                Length = 23.5f,
                LengthMin = 20f,
                LengthMax = 27f,
                InterUp = 5,
                InterDown = 3
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus 7(M58)",
                Number = "1054532A",
                Length = 23.5f,
                LengthMin = 20f,
                LengthMax = 27f,
                InterUp = 5,
                InterDown = 2
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus 7(M42)",
                Number = "902003A",
                Length = 18.85f,
                LengthMin = 15.7f,
                LengthMax = 22f,
                InterUp = 5,
                InterDown = 1
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus 7",
                Number = "1001041A",
                Length = 23.5f,
                LengthMin = 20f,
                LengthMax = 27f,
                InterUp = 5,
                InterDown = 5
            });
            this.InsertItem(new RingMedium() {
                 RingType= enumProductType.Focus,
                Name = "Smart Focus 23",
                Number = "902001A",
                Length = 32.1f,
                LengthMin = 20.6f,
                LengthMax = 43.6f,
                 InterUp = 1,
                 InterDown = 2
            }); 
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus V/C",
                Number = "1011634A",
                Length = 21.7f,
                LengthMin = 19.2f,
                LengthMax = 24.2f,
                InterUp = 5,
                InterDown = 7
            });
            #endregion

            #region 转接环
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "V70 to M72",
                Number = "1072419A",
                Length = 10f,
                InterUp = 6,
                InterDown = 3
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M90 to M95",
                Number = "903001A",
                Length = 4f,
                InterUp = 4,
                InterDown = 10
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M72 to M58",
                Number = "1075556A",
                Length = 6f,
                InterUp = 3,
                InterDown = 2
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M72 to M90",
                Number = "1084879A",
                Length = 4f,
                InterUp = 3,
                InterDown = 4
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M72 to M90",
                Number = "1084879A",
                Length = 4f,
                InterUp = 3,
                InterDown = 4
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M58  to Nikon F-Mount",
                Number = "903002A",
                Length = 9f,
                InterUp = 2,
                InterDown = 8
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M58 to M72",
                Number = "13052A",
                Length = 2f,
                InterUp = 2,
                InterDown = 3
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "V38 to Nikon F-Mount",
                Number = "21610A",
                Length = 9.3f,
                InterUp = 5,
                InterDown = 8
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "V38 to M58",
                Number = "1018385A",
                Length = 10f,
                InterUp = 5,
                InterDown = 2
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "V38 to M42",
                Number = "20059A",
                Length = 6.5f,
                InterUp = 5,
                InterDown = 1
            });
            #endregion

            #region 延长环
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube V38 / V38",
                Number = "20176A",
                Length = 6f,
                InterUp = 5,
                InterDown = 5
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube V38 / V38",
                Number = "20178A",
                Length = 10f,
                InterUp = 5,
                InterDown = 5
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube V38 / V38",
                Number = "20179A",
                Length = 25f,
                InterUp = 5,
                InterDown = 5
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M42 x 1.0",
                Number = "904001A",
                Length = 25f,
                InterUp = 1,
                InterDown = 1
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M58 x 0.75",
                Number = "13051A",
                Length = 10f,
                InterUp = 2,
                InterDown = 2
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M58 x 0.75",
                Number = "13050A",
                Length = 25f,
                InterUp = 2,
                InterDown = 2
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M72 x 0,75",
                Number = "1072421A",
                Length = 10f,
                InterUp = 3,
                InterDown = 3
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M72 x 0,75",
                Number = "26406A",
                Length = 25f,
                InterUp = 3,
                InterDown = 3
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M72 x 0,75",
                Number = "1054733A",
                Length = 50f,
                InterUp = 3,
                InterDown = 3
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M90 x 1.0",
                Number = "1084875A",
                Length = 10f,
                InterUp = 4,
                InterDown = 4
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M90 x 1.0",
                Number = "1084876A",
                Length = 25f,
                InterUp = 4,
                InterDown = 4
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M90 x 1.0",
                Number = "1084877A",
                Length = 50f,
                InterUp = 4,
                InterDown = 4
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M90 x 1.0",
                Number = "1084878A",
                Length = 100f,
                InterUp = 4,
                InterDown = 4
            });
            #endregion

            #region 镜头
            this.InsertItem(new CameraLens
            {
                Name = "Mk-CPN 2.8/28",
                Number = "14794",
                 Focus = 29.29f,
                  Connector = 5,
                   Target = 30f,
                    Flange=25.13f,
            });
            #endregion
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
            List<ValueType> list = this.GetAllDevices(enumProductType.Interface);
            foreach (ValueType item in list)
            {                
                DataRow dr = dt.NewRow();
                dr["Idx"] = ((Connectors)item).Idx;
                dr["Name"] = ((Connectors)item).Name;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public List<ValueType> GetAllDevices(enumProductType type)
        {
            itable handler = this.GetTableHandlerByType(type);
            return handler.GetAllData();
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
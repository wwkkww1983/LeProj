using System;
using System.Data;
using System.Drawing;
using System.Data.SQLite;
using System.Collections.Generic;

namespace CamInter
{
    class DBUtility
    {
        private const string STR_CONNECTION = "Data Source=cam.sqlite;Version=3;";
        private Coder code = new Coder();

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

            SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql, null);
            this.CreateModelData();
        }

        private List<string> CreateTableStruct()
        {
            List<string> tableList = new List<string>();
            //接口
            tableList.Add(new tbConnector().CreateTableStructure());
            //镜头属性
            tableList.Add(new tbCamLens().CreateTableStructure());
            //中间环属性
            tableList.Add(new tbRingMedium().CreateTableStructure());
            return tableList;
        }

        private void CreateModelData()
        {
            //ElementInfo info = new ElementInfo();
            ////箭头
            //info.Name = enumComponent.NONE.ToString ();
            //info.FootType = enumComponentType.NormalComponent;
            //info.Size = new Size(0, 0);
            //info.BackColor = Color.Gray;
            //info.LineFoots = new List<LineFoot>();
            //info.BackImage = "\\img\\arrow.png";
            //AddNewBaseComponent(info);
            ////接地
            //info.Name = enumComponent.Land.ToString ();
            //info.FootType = enumComponentType.NormalComponent;
            //info.Size = new Size(50, 50);
            //info.BackColor = Color.Gray;
            //info.LineFoots = new List<LineFoot>(1) { 
            //    new LineFoot() { Color = Color.Blue, Name = "接地线", LocX=25, LocY=0} };
            //info.BackImage = "\\img\\land.png";
            //AddNewBaseComponent(info);
            ////电阻
            //info.Name = enumComponent.Resistance.ToString ();
            //info.FootType = enumComponentType.NormalComponent;
            //info.Size = new Size(50,10);
            //info.BackColor = Color.Gray;
            //info.LineFoots = new List<LineFoot>(2){ 
            //    new LineFoot() { Color = Color.Blue, Name = "左", LocX=0, LocY=5 },
            //    new LineFoot() { Color = Color.Blue, Name = "右", LocX=50, LocY=5}};
            //info.BackImage = "\\img\\resistance.png";
            //AddNewBaseComponent(info);
            ////电容
            //info.Name = enumComponent.Capacitor.ToString ();
            //info.FootType = enumComponentType.NormalComponent;
            //info.Size = new Size(21, 50);
            //info.BackColor = Color.Gray;
            //info.LineFoots = new List<LineFoot>(2){ 
            //    new LineFoot() { Color = Color.Blue, Name = "左", LocX=5, LocY=50 },
            //    new LineFoot() { Color = Color.Blue, Name = "右", LocX=15, LocY=50}};
            //info.BackImage = "\\img\\capacitor.png";
            //AddNewBaseComponent(info);
            ////74HC244
            //AddComponentFoots(1,enumComponentType.Chips, new List<LineFoot>(){
            //    new LineFoot(){LocX=20,LocY=1},
            //    new LineFoot(){LocX=38,LocY=1},
            //    new LineFoot(){LocX=57,LocY=1},
            //    new LineFoot(){LocX=75,LocY=1},
            //    new LineFoot(){LocX=94,LocY=1},
            //    new LineFoot(){LocX=112,LocY=1},
            //    new LineFoot(){LocX=130,LocY=1},
            //    new LineFoot(){LocX=149,LocY=1},
            //    new LineFoot(){LocX=167,LocY=1},
            //    new LineFoot(){LocX=185,LocY=1},

            //    new LineFoot(){LocX=18,LocY=150},
            //    new LineFoot(){LocX=36,LocY=150},
            //    new LineFoot(){LocX=56,LocY=150},
            //    new LineFoot(){LocX=75,LocY=150},
            //    new LineFoot(){LocX=94,LocY=150},
            //    new LineFoot(){LocX=112,LocY=150},
            //    new LineFoot(){LocX=130,LocY=150},
            //    new LineFoot(){LocX=149,LocY=150},
            //    new LineFoot(){LocX=167,LocY=150},
            //    new LineFoot(){LocX=185,LocY=150}
            //});
            ////HD74LS221P
            //AddComponentFoots(2, enumComponentType.Chips,new List<LineFoot>(){
            //    new LineFoot(){LocX=18,LocY=2},
            //    new LineFoot(){LocX=39,LocY=1},
            //    new LineFoot(){LocX=62,LocY=1},
            //    new LineFoot(){LocX=85,LocY=1},
            //    new LineFoot(){LocX=105,LocY=1},
            //    new LineFoot(){LocX=126,LocY=1},
            //    new LineFoot(){LocX=147,LocY=1},
            //    new LineFoot(){LocX=163,LocY=2},

            //    new LineFoot(){LocX=16,LocY=95},
            //    new LineFoot(){LocX=40,LocY=94},
            //    new LineFoot(){LocX=61,LocY=92},
            //    new LineFoot(){LocX=83,LocY=90},
            //    new LineFoot(){LocX=103,LocY=90},
            //    new LineFoot(){LocX=122,LocY=88},
            //    new LineFoot(){LocX=143,LocY=86},
            //    new LineFoot(){LocX=162,LocY=84}
            //});
        }

        /// <summary>
        /// 获取当前所有接口
        /// </summary>
        /// <returns></returns>
        public List<ValueType> GetAllConnector()
        {
            string strSql = "select * from connectors";
            DataSet dsConnector = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);

            return code.DecodeListByDb(dsConnector.Tables[0], new tbConnector());
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
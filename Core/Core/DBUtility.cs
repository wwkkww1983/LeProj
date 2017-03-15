using System;
using System.Data;
using System.Drawing;
using System.Data.SQLite;
using System.Collections.Generic;


namespace SimuProteus
{
    public class DBUtility
    {
        private const string STR_CONNECTION = "Data Source=prot.s;Version=3;";
        private Coder code = new Coder();

        public DBUtility()
        {
        }

        public DBUtility(bool dbEncrypt)
        {
            SQLiteHelper.SetPassWordFlag = dbEncrypt;
            SQLiteHelper.SetSignature = Ini.GetItemValue("sizeInfo", "appSignature");
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

            SQLiteHelper.CreateDatabase("prot.s");
            SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql, null);
            this.CreateModelData();
        }

        private List<string> CreateTableStruct()
        {
            List<string> tableList = new List<string>();
            //串口信息
            tableList.Add(@"create table serialInfo(
                               portName varchar(10),
                               baudRate int,
                               parity int,
                               databits int,
                               stopbits int,
                               timeOut int);");
            //组件的基础信息，新拖动到面板时展示的样式
            tableList.Add(@"create table components (
                               itemId INTEGER PRIMARY KEY AUTOINCREMENT,
                               type int,
                               name nvarchar(20),
                               width int,
                               height int,
                               backColor int,
                               backImage varchar(100));");
            //新建的项目
            tableList.Add(@"create table projects(
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               name nvarchar(20),
                               x0 int,
                               y0 int,
                               createtime datetime,
                               updatetime datetime,
                               chips nvarchar(100),
                               desc nvarchar(100));");

            //网格节点
            tableList.Add(@"create table netPoints(
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               projIdx int,
                               x int,
                               y int,
                               status int);");

            //当前面板上面的所有元器件
            tableList.Add(@"create table componentView (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               projIdx int,
                               component int,
                               innerIdx int,
                               name nvarchar(20),
                               locX int,
                               locY int,
                               width int,
                               height int,
                               backColor int,
                               backImage varchar(100));");

            //元器件上的引脚
            tableList.Add(@"create table lineFoot (
                               lineIdx INTEGER PRIMARY KEY AUTOINCREMENT,
                               footType int,
                               pinsType int,
                               component int,
                               name nvarchar(20),
                               locX int,
                               locY int,
                               color int);");

            //当前面板上引脚属性
            tableList.Add(@"create table lineFootView (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               projIdx int,
                               component int,
                               innerIdx int,
                               footIdx int,
                               name nvarchar(20),
                               pinsType int);");

            //元器件之间的连线
            tableList.Add(@"create table lineLink (
                               lineIdx INTEGER PRIMARY KEY AUTOINCREMENT,
                               project int,
                               name nvarchar(20),
                               oneElement int,
                               otherElement int,
                               oneFoot int,
                               otherFoot int,
                               locX int,
                               locY int,
                               locOtherX int,
                               locOtherY int,
                               color int);");
            return tableList;
        }

        private void CreateModelData()
        {
            this.InsertSerialInfo(new SerialInfo()
            {
                PortName = "COM1",
                BaudRate = 9600,
                DataBits = 8,
                Parity = 1,
                StopBits = 1,
                TimeOut = 200
            });
            Color colorFoot = Color.FromArgb(Convert.ToInt32(Ini.GetItemValue("colorInfo", "colorFoot")));
            ElementInfo info = new ElementInfo();
            //箭头
            info.Name = enumComponent.NONE.ToString ();
            info.FootType = enumComponentType.NormalComponent;
            info.Size = new Size(0, 0);
            info.BackColor = Color.Gray;
            info.LineFoots = new List<LineFoot>();
            info.BackImage = "img\\arrow.png";
            AddNewBaseComponent(info);
            //运放
            info.Name = enumComponent.Amplifier.ToString ();
            info.FootType = enumComponentType.NormalComponent;
            info.Size = new Size(50, 57);
            info.BackColor = Color.Gray;
            info.LineFoots = new List<LineFoot>(8) { 
                new LineFoot() { Color =colorFoot, Name = "1", LocX=6, LocY=2},
                new LineFoot() { Color =colorFoot, Name = "2", LocX=19, LocY=2},
                new LineFoot() { Color =colorFoot, Name = "3", LocX=32, LocY=2},
                new LineFoot() { Color =colorFoot, Name = "4", LocX=45, LocY=2},
                new LineFoot() { Color =colorFoot, Name = "5", LocX=6, LocY=54},
                new LineFoot() { Color =colorFoot, Name = "6", LocX=19, LocY=54},
                new LineFoot() { Color =colorFoot, Name = "7", LocX=32, LocY=54},
                new LineFoot() { Color =colorFoot, Name = "8", LocX=45, LocY=54}
            };
            info.BackImage = "img\\amplifier.png";
            AddNewBaseComponent(info);
            //电阻
            info.Name = enumComponent.Resistance.ToString ();
            info.FootType = enumComponentType.NormalComponent;
            info.Size = new Size(50,10);
            info.BackColor = Color.Gray;
            info.LineFoots = new List<LineFoot>(2){ 
                new LineFoot() { Color = colorFoot, Name = "左", LocX=0, LocY=6 },
                new LineFoot() { Color = colorFoot, Name = "右", LocX=50, LocY=6}};
            info.BackImage = "img\\resistance.png";
            AddNewBaseComponent(info);
            //二极管
            info.Name = enumComponent.Diode.ToString();
            info.FootType = enumComponentType.NormalComponent;
            info.Size = new Size(50, 10);
            info.BackColor = Color.Gray;
            info.LineFoots = new List<LineFoot>(2){ 
                new LineFoot() { Color = colorFoot, Name = "左", LocX=0, LocY=6 },
                new LineFoot() { Color = colorFoot, Name = "右", LocX=50, LocY=6}};
            info.BackImage = "img\\diode.png";
            AddNewBaseComponent(info);
            //三极管
            info.Name = enumComponent.Triode.ToString();
            info.FootType = enumComponentType.NormalComponent;
            info.Size = new Size(50, 100);
            info.BackColor = Color.Gray;
            info.LineFoots = new List<LineFoot>(2){ 
                new LineFoot() { Color = colorFoot, Name = "左", LocX=0, LocY=50 },
                new LineFoot() { Color = colorFoot, Name = "右", LocX=50, LocY=50},
                new LineFoot() { Color = colorFoot, Name = "下", LocX=29, LocY=100}};
            info.BackImage = "img\\triode.png";
            AddNewBaseComponent(info);
            //电容
            info.Name = enumComponent.Capacitor.ToString ();
            info.FootType = enumComponentType.NormalComponent;
            info.Size = new Size(50, 93);
            info.BackColor = Color.Gray;
            info.LineFoots = new List<LineFoot>(2){ 
                new LineFoot() { Color = colorFoot, Name = "左", LocX=11, LocY=90 },
                new LineFoot() { Color = colorFoot, Name = "右", LocX=40, LocY=90}};
            info.BackImage = "img\\capacitor.png";
            AddNewBaseComponent(info);
            //74HC244
            info.Name = "74HC244";
            info.FootType = enumComponentType.Chips;
            info.Size = new Size(200,150);
            info.BackColor = Color.Gray;
            info.LineFoots = new List<LineFoot>(){
                new LineFoot(){LocX=20,LocY=1},
                new LineFoot(){LocX=38,LocY=1},
                new LineFoot(){LocX=57,LocY=1},
                new LineFoot(){LocX=75,LocY=1},
                new LineFoot(){LocX=94,LocY=1},
                new LineFoot(){LocX=112,LocY=1},
                new LineFoot(){LocX=130,LocY=1},
                new LineFoot(){LocX=149,LocY=1},
                new LineFoot(){LocX=167,LocY=1},
                new LineFoot(){LocX=185,LocY=1},

                new LineFoot(){LocX=18,LocY=150},
                new LineFoot(){LocX=36,LocY=150},
                new LineFoot(){LocX=56,LocY=150},
                new LineFoot(){LocX=75,LocY=150},
                new LineFoot(){LocX=94,LocY=150},
                new LineFoot(){LocX=112,LocY=150},
                new LineFoot(){LocX=130,LocY=150},
                new LineFoot(){LocX=149,LocY=150},
                new LineFoot(){LocX=167,LocY=150},
                new LineFoot(){LocX=185,LocY=150}
            };
            info.BackImage = "img\\74HC244.jpg";
            AddNewBaseComponent(info);
            //74Serial
            info.Name = "74Serial";
            info.FootType = enumComponentType.Chips;
            info.Size = new Size(200, 150);
            info.BackColor = Color.Gray;
            info.LineFoots = new List<LineFoot>(){
                new LineFoot(){ Name="1", LocX=17,LocY=149},
                new LineFoot(){ Name="2",LocX=45,LocY=149},
                new LineFoot(){ Name="3",LocX=73,LocY=149},
                new LineFoot(){ Name="4",LocX=102,LocY=149},
                new LineFoot(){ Name="5",LocX=130,LocY=149},
                new LineFoot(){ Name="6",LocX=158,LocY=149},
                new LineFoot(){ Name="7",LocX=185,LocY=149},

                new LineFoot(){ Name="9",LocX=16,LocY=5},
                new LineFoot(){ Name="10",LocX=44,LocY=5},
                new LineFoot(){ Name="11",LocX=72,LocY=5},
                new LineFoot(){ Name="12",LocX=101,LocY=5},
                new LineFoot(){ Name="13",LocX=128,LocY=5},
                new LineFoot(){ Name="14",LocX=157,LocY=5},
                new LineFoot(){ Name="15",LocX=184,LocY=5}
            };
            info.BackImage = "img\\74Serial.png";
            AddNewBaseComponent(info);
        }

        public void InsertSerialInfo(SerialInfo info)
        {
            string strSql = string.Format("insert into serialInfo (portName,baudRate,parity,databits,stopbits,timeOut) values ('{0}',{1},{2},{3},{4},{5})",
                info.PortName, info.BaudRate, info.Parity, info.DataBits, info.StopBits, info.TimeOut);
            SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql);
        }

        /// <summary>
        /// 获取串口信息
        /// </summary>
        /// <returns></returns>
        public SerialInfo GetSerialInfo()
        {
            string strSql = "select * from serialInfo";
            DataSet dsInfo = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            return code.DecodeSerialInfo(dsInfo);
        }

        public bool UpdateSerialInfo(SerialInfo info)
        {
            string strSql = string.Format("update serialInfo set portName='{0}',baudRate={1},parity={2},databits={3},stopbits={4},timeOut={5}",
                info.PortName, info.BaudRate, info.Parity, info.DataBits, info.StopBits, info.TimeOut);
            return SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql) > 0;
        }

        /// <summary>
        /// 获取当前所有元器件
        /// </summary>
        /// <returns></returns>
        public List<ElementInfo> GetBaseComponents()
        {
            string strSql = "select * from components";
            DataSet dsComps = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            strSql = "select * from lineFoot";
            DataSet dsFoots = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            
            return code.DecodeElementsByDb(dsComps, dsFoots,true);
        }

        public List<ProjectInfo> GetAllProjects()
        {
            string strSql = "select * from projects";
            DataSet ds = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            return code.DecodeProjectsByDb(ds);
        }

        public ProjectDetails getProjectDetail(int projIdx)
        {
            ProjectDetails project = new ProjectDetails();

            string strSql = string.Format( "select * from projects where id={0}",projIdx);
            DataSet ds = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql,null);
            project.Project = code.DecodeOneProjectByDb(ds.Tables[0].Rows[0]);

            strSql = string.Format("select * from componentView where projIdx={0}", projIdx);
            DataSet dscomps = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            strSql = string.Format("select * from lineFoot where footType={0}", (int)enumComponentType.NormalComponent);
            DataSet dsFoots = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql,null);
            project.elementList = code.DecodeElementsByDb(dscomps,dsFoots,false);

            strSql = string.Format("select * from lineLink where project={0}", projIdx);
            DataSet dsLines = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            project.linesList = code.DecodeElementLineByDb(dsLines);

            strSql = string.Format("select * from netPoints where projIdx={0}", projIdx);
            DataSet dsPoints = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            project.pointsList = code.DecodeNetPointsByDb(dsPoints);

            project.footsList = this.GetPinsInfo(projIdx, -1);

            return project;
            
        }

        public List<LineFoot> GetChipFoots(int chipIdx)
        {
            string strSql = string.Format("select * from lineFoot where footType={0} and component={1}", (int)enumComponentType.Chips, chipIdx);
            DataSet dsFoots = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);

            return code.DecodeCompFootsByDb(dsFoots.Tables[0], chipIdx);
        }

        public bool RemoveOneProject(int projIdx)
        {
            string strSql = string.Format(@"delete from projects where id={0};
                               delete from componentView where projIdx={0};
                               delete from lineLink where project={0};
                               delete from lineFootView where projIdx={0};
                               delete from netPoints where projIdx={0};", projIdx);
            return SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql) > 0;
        }

        public bool CheckProjectNameExists(string projName)
        {
            string strSql = string.Format("select id from projects where name='{0}'", projName) ;
            object objId = SQLiteHelper.ExecuteScalar(STR_CONNECTION, strSql);
            return Convert.ToInt32(objId) > 0;
        }

        public List<LineFootView> GetPinsInfo(int projIdx, int elementIdx)
        {
            string strSql = string.Format("select * from lineFootView where projIdx={0}",projIdx);
            if (elementIdx > 0)
            {
                strSql = string.Format("{0} and innerIdx={1}", strSql, elementIdx);
            }
            DataSet ds = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            return code.DecodeElementLineFootsByDb(ds);
        }

        public bool UpdateElementName(int projIdx, int elementIdx, string name)
        {
            string strSql = string.Format("update componentView set name='{0}' where projIdx={1} and innerIdx={2}",name,projIdx,elementIdx);
            return SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql) == 1;
        }

        /// <summary>
        /// 新增元器件
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddNewBaseComponent(ElementInfo info)
        {
            string strSql = string.Format(@"insert into components (name,width,height,backColor,backImage,type) 
                                                values ('{0}',{1},{2},{3},'{4}',{5});select last_insert_rowid();",
                                info.Name, info.Size.Width, info.Size.Height, info.BackColor.ToArgb(), info.BackImage,(int)info.FootType);
            object objIdx = SQLiteHelper.ExecuteScalar(STR_CONNECTION, strSql);

            return this.AddComponentFoots(Convert.ToInt32(objIdx), info.FootType, info.LineFoots);
        }
        /// <summary>
        /// 新增芯片引脚
        /// </summary>
        /// <param name="compIdx"></param>
        /// <param name="footList"></param>
        /// <returns></returns>
        private bool AddComponentFoots(int compIdx, enumComponentType compType, List<LineFoot> footList)
        {
            string strSql = string.Empty;
            foreach (LineFoot item in footList)
            {
                strSql += string.Format("insert into lineFoot (component,footType,pinsType,name,locX,locY,color) values ({0},{1},{2},'{3}',{4},{5},{6});",
                    compIdx, (int)compType,(int)item.PinsType, item.Name == null ? string.Empty : item.Name, item.LocX, item.LocY, item.Color == null ? 0 : item.Color.ToArgb());
            }
            return footList.Count == SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql);
        }


        /// <summary>
        /// 新建项目
        /// </summary>
        /// <param name="projName"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public bool CreateNewProject(string projName,string desc)
        {
            string strSql = @"insert into projects 
                                (name,time,desc) 
                                  values 
                                (@projName,@time,@desc);";
            object[] param = new object[] { projName, DateTime.Now, desc};
            return SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql, param) > 0;
        }

        /// <summary>
        /// 移动元器件（最常用，所以独立出来）
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool MoveComponentShow(ElementInfo info)
        {
            string strSql = string.Format("update componentView set locX={0},locY={1} where id={2}",info.Location.X,info.Location.Y,info.ID);
            return SQLiteHelper.ExecuteNonQuery(STR_CONNECTION,strSql,null) > 0;
        }

        public int InsertNewProject(ProjectDetails project)
        {
            ProjectInfo baseInfo = project.Project;
            string strSql = string.Format(@"insert into projects (name,createtime,updatetime,chips,desc,x0,y0) 
                                                values ('{0}','{1}','{2}','{3}','{4}',{5},{6});select last_insert_rowid();",
                                baseInfo.Name, baseInfo.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), baseInfo.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), baseInfo.Chips, baseInfo.Description,baseInfo.OriginX,baseInfo.OriginY);
            object objIdx = SQLiteHelper.ExecuteScalar(STR_CONNECTION, strSql);
            int projIdx = Convert.ToInt32(objIdx);
            if (projIdx <= 0) return projIdx;

            strSql = string.Empty;
            if (project.elementList.Count == 0) return projIdx;
            foreach (ElementInfo item in project.elementList)
            {
                strSql += string.Format(@"insert into componentView (projIdx,component,name,locX,locY,width,height,backColor,backImage,innerIdx) 
                                        values ({0},{1},'{2}',{3},{4},{5},{6},{7},'{8}',{9});",
                             projIdx, item.Component, item.Name, item.Location.X, item.Location.Y, item.Size.Width, item.Size.Height, item.BackColor.ToArgb(), item.BackImage,item.InnerIdx);
            } 
            objIdx = SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql);
            if(Convert.ToInt32(objIdx) != project.elementList.Count) 
                return -1;

            strSql = string.Empty;
            if (project.linesList.Count == 0) return projIdx;
            foreach (ElementLine item in project.linesList)
            {
                strSql += string.Format(@"insert into lineLink (project,name,oneFoot,otherFoot,locX,locY,locOtherX,locOtherY,color,oneElement,otherElement) 
                                        values ({0},'{1}',{2},{3},{4},{5},{6},{7},'{8}',{9},{10});",
                             projIdx, item.Name, item.oneFoot, item.otherFoot, item.LocX, item.LocY, item.LocOtherX, item.LocOtherY, item.Color.ToArgb (),item.oneElement,item.otherElement);
            }
            objIdx = SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql);
            if (Convert.ToInt32(objIdx) != project.linesList.Count)
                return -1;

            strSql = string.Empty;
            if (project.footsList.Count == 0) return projIdx;
            foreach (LineFootView lineItem in project.footsList)
            {
                strSql += string.Format("insert into lineFootView (projIdx,component,innerIdx,name,pinsType,footIdx) values ({0},{1},{2},'{3}',{4},{5});",
                    projIdx, lineItem.Component, lineItem.Element, lineItem.PinsName, (int)lineItem.PinsType, lineItem.Foot);
            }
            objIdx = SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql);
            if (Convert.ToInt32(objIdx) != project.footsList.Count)
                return -1;

            strSql = string.Empty;
            if (project.pointsList.Count == 0) return projIdx;
            foreach (NetPoint pointItem in project.pointsList)
            {
                strSql += string.Format("insert into netPoints (projIdx,x,y,status) values ({0},{1},{2},{3});",
                    projIdx, pointItem.X, pointItem.Y, (int)pointItem.Type);
            }
            objIdx = SQLiteHelper.ExecuteNonQuery(STR_CONNECTION, strSql);
            if (Convert.ToInt32(objIdx) != project.pointsList.Count)
                return -1;

            return projIdx;
        }

        /// <summary>
        /// 修改部分属性
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateComponent(ElementInfo info)
        {
            string strSql = "update componentView set locX=@locX,locY=@locY where id=@id";
            List<SQLiteParameter> paraList = new List<SQLiteParameter> ();
            paraList.Add(SQLiteHelper.CreateParameter("@locX",DbType.Int32,info.Location.X));
            paraList.Add(SQLiteHelper.CreateParameter("@locY",DbType.Int32,info.Location.Y));
            paraList.Add(SQLiteHelper.CreateParameter("@id",DbType.Int32,info.ID));

            SQLiteCommand command = SQLiteHelper.CreateCommand(STR_CONNECTION,strSql,paraList.ToArray());
            return SQLiteHelper.ExecuteNonQuery(command) > 0;
        }
    }
}
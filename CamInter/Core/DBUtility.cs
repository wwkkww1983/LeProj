using System;
using System.Data;
using System.Drawing;
using System.Data.SQLite;
using System.Collections.Generic;

namespace Core
{
    public class DBUtility
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
            this.InsertItem(new Connectors() { Name = "M42", Idx = 1, Length = 42,IsShowInList = true });
            this.InsertItem(new Connectors() { Name = "M58", Idx = 2, Length = 58, IsShowInList = true });
            this.InsertItem(new Connectors() { Name = "M72", Idx = 3, Length = 72, IsShowInList = true });
            this.InsertItem(new Connectors() { Name = "M90", Idx = 4, Length = 90, IsShowInList = true });
            this.InsertItem(new Connectors() { Name = "V38", Idx = 5, Length = 38, IsShowInList = false });
            this.InsertItem(new Connectors() { Name = "V70", Idx = 6, Length = 70, IsShowInList = false });
            this.InsertItem(new Connectors() { Name = "C", Idx = 7, Length = 25, IsShowInList = true });
            this.InsertItem(new Connectors() { Name = "F", Idx = 8, Length = 41, IsShowInList = true });
            this.InsertItem(new Connectors() { Name = "Basler beat/racer", Idx = 9, Length = 33.8f, IsShowInList = true });
            this.InsertItem(new Connectors() { Name = "M95", Idx = 10, Length = 95, IsShowInList = true });
            this.InsertItem(new Connectors() { Name = "V90", Idx = 11, Length = 70, IsShowInList = false });
            this.InsertItem(new Connectors() { Name = "V48", Idx = 12, Length = 90, IsShowInList = false });
            this.InsertItem(new Connectors() { Name = "CS", Idx = 13, Length = 12, IsShowInList = true });
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
                InterUp = 9,
                InterDown = 5,
                ImgName = "902002A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus 7(M72)",
                Number = "902004A",
                Length = 23.5f,
                LengthMin = 20f,
                LengthMax = 27f,
                InterUp = 3,
                InterDown = 5,
                ImgName = "902004A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus 7(M58)",
                Number = "1054532A",
                Length = 23.5f,
                LengthMin = 20f,
                LengthMax = 27f,
                InterUp = 2,
                InterDown = 5,
                ImgName = "1054532A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus 7(M42)",
                Number = "902003A",
                Length = 18.85f,
                LengthMin = 15.7f,
                LengthMax = 22f,
                InterUp = 1,
                InterDown = 5,
                ImgName = "902003A.jpg"
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
                InterDown = 5,
                ImgName = "1001041A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus 23",
                Number = "902001A",
                Length = 32.1f,
                LengthMin = 20.6f,
                LengthMax = 43.6f,
                InterUp = 2,
                InterDown = 1,
                ImgName = "902001A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "Smart Focus V/C",
                Number = "1011634A",
                Length = 21.7f,
                LengthMin = 19.2f,
                LengthMax = 24.2f,
                InterUp = 7,
                InterDown = 5,
                ImgName = "1011634A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Focus,
                Name = "V48 to V70",
                Number = "1075304A",
                Length = 0f,
                LengthMin = 0f,
                LengthMax = 0f,
                InterUp = 6,
                InterDown = 12,
                ImgName = "1075304A.jpg"
            });
            #endregion

            #region 转接环
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "V70 to M72",
                Number = "1072419A",
                Length = 10f,
                LengthMin = 10f,
                LengthMax = 10f,
                InterUp = 3,
                InterDown = 6,
                ImgName = "1072419A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M90 to M95",
                Number = "903001A",
                Length = 4f,
                LengthMin = 4f,
                LengthMax = 4f,
                InterUp = 10,
                InterDown = 4,
                ImgName = "903001A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M72 to M58",
                Number = "1075556A",
                Length = 6f,
                LengthMin = 6f,
                LengthMax = 6f,
                InterUp = 2,
                InterDown = 3,
                ImgName = "1075556A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M72 to M90",
                Number = "1084879A",
                Length = 4f,
                LengthMin = 4f,
                LengthMax = 4f,
                InterUp = 4,
                InterDown = 3,
                ImgName = "1084879A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M58  to Nikon F-Mount",
                Number = "903002A",
                Length = 9f,
                LengthMin = 9f,
                LengthMax = 9f,
                InterUp = 8,
                InterDown = 2,
                ImgName = "903002A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "M58 to M72",
                Number = "13052A",
                Length = 2f,
                LengthMin = 2f,
                LengthMax = 2f,
                InterUp = 3,
                InterDown = 2,
                ImgName = "13052A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "V38 to Nikon F-Mount",
                Number = "21610A",
                Length = 9.3f,
                LengthMin = 9.3f,
                LengthMax = 9.3f,
                InterUp = 8,
                InterDown = 5,
                ImgName = "21610A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "V38 to M58",
                Number = "1018385A",
                Length = 10f,
                LengthMin = 10f,
                LengthMax = 10f,
                InterUp = 2,
                InterDown = 5,
                ImgName = "1018385A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Adapter,
                Name = "V38 to M42",
                Number = "20059A",
                Length = 6.5f,
                LengthMin = 6.5f,
                LengthMax = 6.5f,
                InterUp = 1,
                InterDown = 5,
                ImgName = "20059A.jpg"
            });
            #endregion

            #region 延长环
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube V38 / V38",
                Number = "20176A",
                Length = 6f,
                LengthMin = 6f,
                LengthMax = 6f,
                InterUp = 5,
                InterDown = 5,
                ImgName = "20176A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube V38 / V38",
                Number = "20178A",
                Length = 10f,
                LengthMin = 10f,
                LengthMax = 10f,
                InterUp = 5,
                InterDown = 5,
                ImgName = "20178A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube V38 / V38",
                Number = "20179A",
                Length = 25f,
                LengthMin = 25f,
                LengthMax = 25f,
                InterUp = 5,
                InterDown = 5,
                ImgName = "20179A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M42 x 1.0",
                Number = "904001A",
                Length = 25f,
                LengthMin = 25f,
                LengthMax = 25f,
                InterUp = 1,
                InterDown = 1,
                ImgName = "904001A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M58 x 0.75",
                Number = "13051A",
                Length = 10f,
                LengthMin = 10f,
                LengthMax = 10f,
                InterUp = 2,
                InterDown = 2,
                ImgName = "13051A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M58 x 0.75",
                Number = "13050A",
                Length = 25f,
                LengthMin = 25f,
                LengthMax = 25f,
                InterUp = 2,
                InterDown = 2,
                ImgName = "13050A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M72 x 0,75",
                Number = "1072421A",
                Length = 10f,
                LengthMin = 10f,
                LengthMax = 10f,
                InterUp = 3,
                InterDown = 3,
                ImgName = "1072421A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M72 x 0,75",
                Number = "26406A",
                Length = 25f,
                LengthMin = 25f,
                LengthMax = 25f,
                InterUp = 3,
                InterDown = 3,
                ImgName = "26406A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M72 x 0,75",
                Number = "1054733A",
                Length = 50f,
                LengthMin = 50f,
                LengthMax = 50f,
                InterUp = 3,
                InterDown = 3,
                ImgName = "1054733A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M90 x 1.0",
                Number = "1084875A",
                Length = 10f,
                LengthMin = 10f,
                LengthMax = 10f,
                InterUp = 4,
                InterDown = 4,
                ImgName = "1084875A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M90 x 1.0",
                Number = "1084876A",
                Length = 25f,
                LengthMin = 25f,
                LengthMax = 25f,
                InterUp = 4,
                InterDown = 4,
                ImgName = "1084876A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M90 x 1.0",
                Number = "1084877A",
                Length = 50f,
                LengthMin = 50f,
                LengthMax = 50f,
                InterUp = 4,
                InterDown = 4,
                ImgName = "1084877A.jpg"
            });
            this.InsertItem(new RingMedium()
            {
                RingType = enumProductType.Extend,
                Name = "Ext.Tube M90 x 1.0",
                Number = "1084878A",
                Length = 100f,
                LengthMin = 100f,
                LengthMax = 100f,
                InterUp = 4,
                InterDown = 4,
                ImgName = "1084878A.jpg"
            });
            #endregion

            #region 镜头
            this.InsertItem(new CameraLens
            {
                Name = "Mk-CPN-S 5.6/100",
                Number = "35142",
                Focus = 102.34f,
                Target = 90f,
                Flange = 95.87f,
                Connector = 5,
                HH = -2.38f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName = "35142.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Mk-APO-CPN 4.5/90",
                Number = "1070213",
                Focus = 91.2f,
                Target = 90f,
                Flange = 86.5f,
                Connector = 5,
                HH = -3.6f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName = "1070213.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Makro MSR 5.6/80**",
                Number = "1070160",
                Focus = 82.4f,
                Target = 80f,
                Flange = 77.6f,
                Connector = 5,
                HH = -1.31f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName = "1070160.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Mk-CPN-S 4.0/80",
                Number = "14780",
                Focus = 80.34f,
                Target = 80f,
                Flange = 77.5f,
                Connector = 5,
                HH = -1.81f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName = "14780.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Mk-APO-CPN 4.0/60",
                Number = "14802",
                Focus = 59.94f,
                Target = 70f,
                Flange = 53.29f,
                Connector = 5,
                HH = -1.85f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName="14802.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Mk-CPN-S 2.8/50",
                Number = "14796",
                Focus = 50.17f,
                Target = 43.2f,
                Flange = 42f,
                Connector = 5,
                HH = -3.14f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName = "14796.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Mk-APO-CPN 4.0/45",
                Number = "14783",
                Focus = 46.53f,
                Target = 43.2f,
                Flange = 42.35f,
                Connector = 5,
                HH = -1.8f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName = "14783.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Mk-APO-CPN 2.8/40",
                Number = "14798",
                Focus = 41.52f,
                Target = 43.2f,
                Flange = 38.11f,
                Connector = 5,
                HH = -2.19f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName = "14798.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Mk-CPN 2.8/35",
                Number = "14792",
                Focus = 34.98f,
                Target = 32.5f,
                Flange = 30.75f,
                Connector = 5,
                HH = -3.54f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName = "14792.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Mk-CPN 2.8/28",
                Number = "14794",
                Focus = 29.29f,
                Connector = 5,
                Target = 30f,
                Flange = 25.13f,
                HH = -2.94f,
                Length = 28.6f,
                RatioMin = 0,
                RatioMax = 1,
                ImgName = "14794.jpg"
            });


            this.InsertItem(new CameraLens
            {
                Name = "Makro-Symmar 5.6/120-0058",
                Number = "1002647",
                Focus = 120.7f,
                Connector = 5,
                Target = 82f,
                Flange = 115.3f,
                HH = -1.8f,
                Length = 28.6f,
                RatioMin = 0.78f,
                RatioMax = 1.23f,
                ImgName = "1002647.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Makro-Symmar 5.6/120-0059",
                Number = "1002648",
                Focus = 120.2f,
                Connector = 5,
                Target = 82f,
                Flange = 114.9f,
                HH = -1.2f,
                Length = 28.6f,
                RatioMin = 0.53f,
                RatioMax = 0.98f,
                ImgName = "1002648.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Makro-Symmar 5.6/120-0060",
                Number = "1002650",
                Focus = 119.8f,
                Connector = 5,
                Target = 82f,
                Flange = 114.1f,
                HH = -0.5f,
                Length = 28.6f,
                RatioMin = 0.28f,
                RatioMax = 0.73f,
                ImgName = "1002650.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Makro-Symmar 5.6/120-0061",
                Number = "1004611",
                Focus = 118.9f,
                Connector = 5,
                Target = 82f,
                Flange = 113.8f,
                HH = 0.6f,
                Length = 28.6f,
                RatioMin = 0.16f,
                RatioMax = 0.48f,
                ImgName = "1004611.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Makro-APO-CPN 4.5/90-0018",
                Number = "1004531",
                Focus = 90.8f,
                Connector = 5,
                Target = 82f,
                Flange = 86.55f,
                HH = -3.56f,
                Length = 28.6f,
                RatioMin = 0.1f,
                RatioMax = 0.5f,
                ImgName = "1004531.jpg"
            });



//==============================================
            this.InsertItem(new CameraLens
            {
                Name = "Micro-Symmar 2.8/50mm",
                Number = "1012492",
                Focus = 50.811f,
                Connector = 5,
                Target = 58f,
                Flange =6.78f,
                HH = -32.518f,
                Length = 28.6f,
                RatioMin = 3.4f,
                RatioMax = 3.6f,
                ImgName = "1012492.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "MacroVaron 4.5/85",
                Number = "1072517",
                Focus = 85.1f,
                Connector = 5,
                Target = 63f,
                Flange = 64.05f,
                HH = -5.1f,
                Length = 28.6f,
                RatioMin = 0.4f,
                RatioMax = 3.6f,
                ImgName = "1072517.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "MacroVaron 4.5/85 with BS",
                Number = "1069837",
                Focus = 85.2f,
                Connector = 5,
                Target = 63f,
                Flange = 60.9f,
                HH = 2.9f,
                Length = 28.6f,
                RatioMin = 3.4f,
                RatioMax = 3.6f,
                ImgName = "1069837.jpg"
            });


            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 4.5/95mm",
                Number = "1068013",
                Focus = 95f,
                Connector = 6,
                Target = 62.5f,
                Flange =60.87f,
                HH = -2.22f,
                Length = 28.6f,
                RatioMin = 0.02f,
                RatioMax = 0.18f,
                ImgName = "1068013.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 3.9/95mm",
                Number = "1071819",
                Focus = 95.1f,
                Connector = 6,
                Target = 62.5f,
                Flange = 62.33f,
                HH = -8.8f,
                Length = 28.6f,
                RatioMin = 0.19f,
                RatioMax = 0.29f,
                ImgName = "1071819.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 3.7/96mm",
                Number = "1071818",
                Focus = 95.5f,
                Connector = 6,
                Target = 62.5f,
                Flange = 60.85f,
                HH = -9.28f,
                Length = 28.6f,
                RatioMin = 0.245f,
                RatioMax = 0.34f,
                ImgName = "1071818.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 3.5/96mm",
                Number = "1068012",
                Focus = 95.5f,
                Connector = 6,
                Target = 62.5f,
                Flange = 60.15f,
                HH = -9.1f,
                Length = 28.6f,
                RatioMin = 0.29f,
                RatioMax = 0.40f,
                ImgName = "1068012.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 3.2/96mm",
                Number = "1071189",
                Focus = 96.2f,
                Connector = 6,
                Target = 62.5f,
                Flange =60.82f,
                HH = -9.79f,
                Length = 28.6f,
                RatioMin = 0.44f,
                RatioMax = 0.56f,
                ImgName = "1071189.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 3.2/97mm",
                Number = "1071190",
                Focus = 97f,
                Connector = 6,
                Target = 62.5f,
                Flange = 60.24f,
                HH = -12.3f,
                Length = 28.6f,
                RatioMin = 0.64f,
                RatioMax = 0.77f,
                ImgName = "1071190.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 3.2/88mm",
                Number = "1068014",
                Focus = 87.5f,
                Connector = 6,
                Target = 62.5f,
                Flange = 59.8f,
                HH = -7.2f,
                Length = 28.6f,
                RatioMin = 1.64f,
                RatioMax = 1.86f,
                ImgName = "1068014.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 3.2/97mm",
                Number = "1076096",
                Focus = 97f,
                Connector = 6,
                Target = 82f,
                Flange =10.8f,
                HH = -12.3f,
                Length = 28.6f,
                RatioMin = 1.34f,
                RatioMax = 1.55f,
                ImgName = "1076096.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 3.2/88mm with BS",
                Number = "1073347",
                Focus = 88.2f,
                Connector = 6,
                Target = 62.5f,
                Flange = 60.8f,
                HH = -0.9f,
                Length = 28.6f,
                RatioMin = 1.64f,
                RatioMax = 1.86f,
                ImgName = "1073347.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 2.8/98mm",
                Number = "1076452",
                Focus = 97.5f,
                Connector = 6,
                Target = 62.5f,
                Flange = 61.46f,
                HH = -12.89f,
                Length = 28.6f,
                RatioMin = 0.82f,
                RatioMax = 0.93f,
                ImgName = "1076452.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 2.8/98mm",
                Number = "1076453",
                Focus = 97.5f,
                Connector = 6,
                Target = 82f,
                Flange = 9.7f,
                HH = -12.9f,
                Length = 28.6f,
                RatioMin = 1.07f,
                RatioMax = 1.21f,
                ImgName = "1076453.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Sapphire 3.2/96mm",
                Number = "1076351",
                Focus = 96.21f,
                Connector = 6,
                Target = 82f,
                Flange = 17.28f,
                HH = -9.62f,
                Length = 28.6f,
                RatioMin = 1.84f,
                RatioMax = 2.16f,
                ImgName = "1076351.jpg"
            });


            this.InsertItem(new CameraLens
            {
                Name = "Xenon-Diamond 2.7/111mm",
                Number = "1078039",
                Focus = 111.2f,
                Connector = 6,
                Target = 82f,
                Flange = 66.17f,
                HH = -6.8f,
                Length = 28.6f,
                RatioMin = 2.44f,
                RatioMax = 2.76f,
                ImgName = "1078039.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon-Diamond 2.9/106mm with BS",
                Number = "1076949",
                Focus = 105.3f,
                Connector = 6,
                Target = 82f,
                Flange = 68.44f,
                HH = 13.1f,
                Length = 28.6f,
                RatioMin = 2.44f,
                RatioMax = 2.76f,
                ImgName = "1076949.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon-Diamond 2.2/117mm",
                Number = "1076963",
                Focus = 116.9f,
                Connector = 11,
                Target = 82f,
                Flange = 36.9f,
                HH = -8.3f,
                Length = 28.6f,
                RatioMin = 3.34f,
                RatioMax = 3.66f,
                ImgName = "1076963.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon-Diamond 2.3/116mm with BS",
                Number = "1079718",
                Focus = 116f,
                Connector = 11,
                Target = 82f,
                Flange = 32.9f,
                HH = 0.83f,
                Length = 28.6f,
                RatioMin = 3.34f,
                RatioMax = 3.66f,
                ImgName = "1079718.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon-Diamond 1.5/82mm",
                Number = "1079320",
                Focus = 82.4f,
                Connector = 6,
                Target = 82f,
                Flange = 0.59f,
                HH = -27.3f,
                Length = 28.6f,
                RatioMin = 5.04f,
                RatioMax = 5.31f,
                ImgName = "1079320.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon-Diamond 1.6/80mm with BS",
                Number = "1081873",
                Focus = 80f,
                Connector = 6,
                Target = 82f,
                Flange = 3.1f,
                HH = -14.65f,
                Length = 28.6f,
                RatioMin = 5.04f,
                RatioMax = 5.31f,
                ImgName = "1081873.jpg"
            });


            this.InsertItem(new CameraLens
            {
                Name = "Xenon Zirconia 3.3/92mm",
                Number = "1073622",
                Focus = 91.9f,
                Connector = 12,
                Target = 82f,
                Flange = 52.32f,
                HH = -17.3f,
                Length = 28.6f,
                RatioMin = 0.15f,
                RatioMax = 0.25f,
                ImgName = "1073622.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Zirconia XL 4.0/104mm",
                Number = "1079651",
                Focus = 103.8f,
                Connector = 12,
                Target = 82f,
                Flange = 58.72f,
                HH = -18.6f,
                Length = 28.6f,
                RatioMin = 0.27f,
                RatioMax = 0.39f,
                ImgName = "1079651.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Zirconia 3.2/91mm",
                Number = "1076966",
                Focus = 91.5f,
                Connector = 12,
                Target = 82f,
                Flange = 48.07f,
                HH = -18f,
                Length = 28.6f,
                RatioMin = 0.42f,
                RatioMax = 0.58f,
                ImgName = "1076966.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Zirconia 3.1/91mm",
                Number = "1078947",
                Focus = 91.2f,
                Connector = 12,
                Target = 82f,
                Flange = 43.8f,
                HH = -21.2f,
                Length = 28.6f,
                RatioMin = 0.62f,
                RatioMax = 0.78f,
                ImgName = "1078947.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Zirconia 2.8/89mm",
                Number = "1078948",
                Focus = 88.8f,
                Connector = 12,
                Target = 82f,
                Flange = 41.28f,
                HH = -20.1f,
                Length = 28.6f,
                RatioMin = 0.89f,
                RatioMax = 1.11f,
                ImgName = "1078948.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Zirconia 3.2/92mm",
                Number = "1078872",
                Focus = 91.6f,
                Connector = 12,
                Target = 82f,
                Flange = 41f,
                HH = -18.4f,
                Length = 28.6f,
                RatioMin = 1.79f,
                RatioMax = 2.21f,
                ImgName = "1078872.jpg"
            });
            this.InsertItem(new CameraLens
            {
                Name = "Xenon Zirconia 3.2/92mm with BS",
                Number = "1078988",
                Focus = 93.3f,
                Connector = 12,
                Target = 82f,
                Flange = 45.1f,
                HH = -11.9f,
                Length = 28.6f,
                RatioMin = 1.79f,
                RatioMax = 2.21f,
                ImgName = "1078988.jpg"
            });
            #endregion
        }

        /// <summary>
        /// 获取当前所有接口
        /// </summary>
        /// <returns></returns>
        public DataTable GetDropDownListInfo(enumProductType type)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Idx");
            dt.Columns.Add(dc);
            dc = new DataColumn("Name");
            dt.Columns.Add(dc);
            List<ValueType> list = this.GetAllDevices(type);
            foreach (ValueType item in list)
            {
                iGetIDName tmpIDName = (iGetIDName)item;
                if (!tmpIDName.IsShowInList) continue;

                DataRow dr = dt.NewRow();
                dr["Idx"] = tmpIDName.Idx;
                dr["Name"] = tmpIDName.Name;
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
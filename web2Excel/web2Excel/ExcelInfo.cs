using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace web2Excel
{
    class ExcelInfo
    {
        private static Excel.Application app;
        private static Excel.Workbook workBook;
        public static UpdateBoardValue UpdateProgess;
        public static UpdateProjCount updateItemCount;
        public static UpdateProgressInfo updateProgressInfo;

        /// <summary>
        /// 在制定的文件夹创建Excel文件
        /// </summary>
        /// <param name="foldName">文件夹路径</param>
        /// <param name="fileName">文件名</param>
        public static void CreateExcelFile(string foldName, string fileName)
        {
            object Nothing = System.Reflection.Missing.Value;
            app = new Excel.Application();
            app.Visible = false;
            workBook = app.Workbooks.Add(Nothing);

            Excel.Worksheet worksheet = (Excel.Worksheet)workBook.Sheets[1];
            worksheet.Name = "原始数据";
            AddExcelHead(worksheet, false);

            worksheet = (Excel.Worksheet)workBook.Sheets[2];
            worksheet.Name = "成交数据表";
            AddExcelHead(worksheet, true);

            worksheet.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// Excel表头添加
        /// </summary>
        /// <param name="worksheet">Excel表</param>
        /// <param name="hasDeal">是否显示成交日期</param>
        private static void AddExcelHead(Excel.Worksheet worksheet, bool hasDeal)
        {
            string[] headItems = { "项目名称", "地址", "幢号（楼号）", "层高", "所在层", "房间号", "户型", "用途", "建筑面积", "套内面积", "分摊面积", "房屋状态", "预售/现售", "成交日期" };
            int idx = 1;
            foreach (string strItem in headItems)
            {
                worksheet.Cells[1, idx++] = strItem;
            }
            if (!hasDeal)
            {

                worksheet.Cells[1, idx - 1] = string.Empty;
            }
        }

        /// <summary>
        /// 加入一条数据
        /// </summary>
        /// <param name="excelIdx">表单序号</param>
        /// <param name="itemIdx">第几条数据</param>
        /// <param name="itemInfo">数据内容</param>
        public static void InsertItemInfo(int excelIdx, int itemIdx, Content itemInfo)
        {
            int idx = 1;

            Excel.Worksheet worksheet = (Excel.Worksheet)workBook.Sheets[excelIdx];

            worksheet.Cells[itemIdx, idx++] = itemInfo.ProjectName;
            worksheet.Cells[itemIdx, idx++] = itemInfo.Address;
            worksheet.Cells[itemIdx, idx++] = itemInfo.BlockNumber;
            worksheet.Cells[itemIdx, idx++] = itemInfo.LayerCount;
            worksheet.Cells[itemIdx, idx++] = itemInfo.LayerNumber;
            worksheet.Cells[itemIdx, idx++] = itemInfo.RoomNumber;
            worksheet.Cells[itemIdx, idx++] = itemInfo.RoomType;
            worksheet.Cells[itemIdx, idx++] = itemInfo.FunctionName;
            worksheet.Cells[itemIdx, idx++] = itemInfo.AreaBuilding;
            worksheet.Cells[itemIdx, idx++] = itemInfo.AreaRoom;
            worksheet.Cells[itemIdx, idx++] = itemInfo.AreaPublic;
            worksheet.Cells[itemIdx, idx++] = itemInfo.HouseStatus;
            worksheet.Cells[itemIdx, idx++] = itemInfo.IsPrepare;
            worksheet.Cells[itemIdx, idx++] = itemInfo.DealDate;
        }

        /// <summary>
        /// 更新户型数据
        /// </summary>
        /// <param name="excelIdx">表单序号</param>
        /// <param name="houseType">数据内容</param>
        public static void updateTypeInfo(int excelIdx, IDictionary<int, string> houseType)
        {
            Excel.Worksheet worksheet = (Excel.Worksheet)workBook.Sheets[excelIdx];
            foreach (KeyValuePair<int, string> item in houseType)
            {
                worksheet.Cells[item.Key, 7] = item.Value;
            }
        }

        /// <summary>
        /// 返回数据表数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dataList"></param>
        public static void GetNormalList(string fileName, out string[,] dataList)
        {
            app = new Excel.Application();
            workBook = app.Workbooks.Open(fileName);
            Excel.Worksheet worksheet = (Excel.Worksheet)workBook.Sheets[2];
            GetExcelData(false, worksheet, out dataList);
        }

        /// <summary>
        /// 解析Excel表格数据
        /// </summary>
        /// <param name="upFlag"></param>
        /// <param name="worksheet"></param>
        /// <param name="dataList"></param>
        private static void GetExcelData(bool upFlag, Excel.Worksheet worksheet, out string[,] dataList)
        {
            int row = worksheet.UsedRange.Rows.Count;
            int column = worksheet.UsedRange.Columns.Count;
            dataList = new string[row - 1, column];
            for (int i = 2; i <= row; i++)
            {
                if (upFlag) UpdateProgess(false, i);
                for (int j = 1; j <= column; j++)
                {
                    dataList[i - 2, j - 1] = (worksheet.Cells[i, j] as Excel.Range).Text.ToString();
                }
            }
        }


        /// <summary>
        /// 保存当前信息
        /// </summary>
        public static void SaveInfo()
        {
            workBook.Save();
        }

        /// <summary>
        /// 关闭并退出
        /// </summary>
        public static void FinishExcel()
        {
            SaveInfo();
            workBook.Close(true);
            app.Quit();
        }

        /// <summary>
        /// 对比今天和昨天的数据
        /// </summary>
        /// <param name="todayFileName">今天的Excel文件</param>
        /// <param name="yestdayFileName">昨天</param>
        /// <param name="dealDate">成交日期</param>
        /// <param name="projComparedList">比较结果中的项目</param>
        public static ErrorInfo CompareData(string todayFileName, string yestdayFileName, string dealDate, List<string> projComparedList)
        {
            int changedIdx = 2;

            if (!File.Exists(todayFileName)) return ErrorInfo.TodayFileNoExists;
            if (!File.Exists(yestdayFileName)) return ErrorInfo.YesterFileNoExists;

            Excel.Application appExcel = new Excel.Application();
            Excel.Workbook todayExcel = appExcel.Workbooks.Open(todayFileName);
            Excel.Workbook yestdayExcel = appExcel.Workbooks.Open(yestdayFileName);

            Excel.Worksheet todaySheet = todayExcel.Sheets[1] as Excel.Worksheet;
            Excel.Worksheet yestdaySheet = yestdayExcel.Sheets[1] as Excel.Worksheet;
            Excel.Worksheet todaySheedChanged = todayExcel.Sheets[2] as Excel.Worksheet;


            int todayRow = todaySheet.UsedRange.Rows.Count;
            int yestdayRow = yestdaySheet.UsedRange.Rows.Count;
            string strHouseStatus = string.Empty;

            string[,] todayExcelData, yestdayExcelData;
            updateProgressInfo("读取今天数据。。。");
            updateItemCount(todayRow);
            UpdateProgess(false, 0);
            GetExcelData(true, todaySheet, out todayExcelData);
            updateProgressInfo("读昨天记录。。。");
            updateItemCount(yestdayRow);
            UpdateProgess(false, 0);
            GetExcelData(true, yestdaySheet, out yestdayExcelData);

            updateProgressInfo("数据对比中。。。");
            updateItemCount(todayRow);
            for (int i = 0; i < todayRow - 1; i++)//减去第一行标题
            {
                int j = 0, jFlag = 0; ;
                try
                {
                    UpdateProgess(false, i + 1);
                    if (todayExcelData[i, 0] == "奥园商业广场" && todayExcelData[i, 5] == "1231")
                    {
                        int ddtt = 0;
                    }
                    strHouseStatus = todayExcelData[i, 11];
                    if (strHouseStatus != "签订中" && strHouseStatus != "已备案" && strHouseStatus != "已预告" || yestdayExcelData[i, 11] != "可售")
                        continue;//还是可售或不可售（状态没变）
                    int findFlag = 0;//0:未找到，1：已找到，2已找过
                    //for (int j = Math.Max(2, i - 20); j <= yestdayRow; j++)
                    for (j = Math.Min(i, yestdayRow - 2); j > 0; j--)
                    {//先往前找
                        if (yestdayExcelData[j, 0] != todayExcelData[i, 0])
                        {
                            if (findFlag >= 1) { findFlag++; break; }//数据已经遍历完
                            else continue;//还没找到数据
                        }
                        else
                        {
                            findFlag = 1;
                        }

                        if (yestdayExcelData[j, 0] == todayExcelData[i, 0]
                            && yestdayExcelData[j, 1] == todayExcelData[i, 1]
                            && yestdayExcelData[j, 2] == todayExcelData[i, 2]
                            && yestdayExcelData[j, 4] == todayExcelData[i, 4]
                            && yestdayExcelData[j, 5] == todayExcelData[i, 5])
                        {
                            if (yestdayExcelData[j, 11] != todayExcelData[i, 11])
                            {
                                if (!projComparedList.Contains(todayExcelData[i, 0]))
                                    projComparedList.Add(todayExcelData[i, 0]);
                                int k = 1;
                                for (; k <= 13; k++)
                                {
                                    todaySheedChanged.Cells[changedIdx, k] = todayExcelData[i, k - 1];
                                }
                                todaySheedChanged.Cells[changedIdx, k] = dealDate;
                                changedIdx++;
                                todayExcel.Save();
                            }
                            break;
                        }
                    }
                    jFlag = 1;
                    if (findFlag >= 1) continue;//找到过，则不再向后找
                    jFlag = 2;
                    for (j = Math.Min(i + 1, yestdayRow - 2); j < yestdayRow - 1; j++)
                    {//再往后找
                        if (yestdayExcelData[j, 0] != todayExcelData[i, 0])
                        {
                            if (findFlag >= 1) { findFlag++; break; }//数据已经遍历完
                            else continue;//还没找到数据
                        }
                        else
                        {
                            findFlag = 1;
                        }

                        if (yestdayExcelData[j, 0] == todayExcelData[i, 0]
                            && yestdayExcelData[j, 1] == todayExcelData[i, 1]
                            && yestdayExcelData[j, 2] == todayExcelData[i, 2]
                            && yestdayExcelData[j, 4] == todayExcelData[i, 4]
                            && yestdayExcelData[j, 5] == todayExcelData[i, 5])
                        {
                            if (yestdayExcelData[j, 11] != todayExcelData[i, 11])
                            {
                                if (!projComparedList.Contains(todayExcelData[i, 0]))
                                    projComparedList.Add(todayExcelData[i, 0]);
                                int k = 1;
                                for (; k <= 13; k++)
                                {
                                    todaySheedChanged.Cells[changedIdx, k] = todayExcelData[i, k-1];
                                }
                                todaySheedChanged.Cells[changedIdx, k] = dealDate;
                                changedIdx++;
                                todayExcel.Save();
                            }
                            break;
                        }
                    }
                    jFlag = 3;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("i={0},j={1},jFlag={2},cIdx={3},tr={4},yr={5},error={6},code={7}", i, j, jFlag, changedIdx, todayRow, yestdayRow, ex.Message, ex.Source));
                }
            }
            todayExcel.Save();
            todayExcel.Close();
            yestdayExcel.Close();
            appExcel.Quit();

            return ErrorInfo.OK;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using ZdflCount.App_Start;
using Microsoft.Office.Interop.Excel;

namespace ZdflCount
{
    public class Excel
    {
        private static Application excel = new Application();
        public static enumErrorCode CheckAndSaveStaffInfo(string filePath)
        {
            enumErrorCode result = enumErrorCode.NONE;

            excel.Visible = false;
            excel.UserControl = false;

            object missing = System.Reflection.Missing.Value;
            Workbook wb = excel.Application.Workbooks.Open(filePath, missing, true);
            Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);
            int rowCount = ws.UsedRange.Cells.Rows.Count;
            int columnCount = ws.UsedRange.Cells.Columns.Count;
            if (!(ws.Cells[1, 1].Text.ToString() == "姓名" && 
                ws.Cells[1, 1].Text.ToString() == "工号" && 
                ws.Cells[1, 6].Text.ToString() == "手机"))
            {
                result = enumErrorCode.ExcelHeadError;
                return result;
            }
            return enumErrorCode.NONE;
        }
    }
}

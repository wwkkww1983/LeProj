using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace CamInter
{
    /// <summary>
    /// 显示信息的编码解码
    /// </summary>
    class Coder
    {
        public List<ValueType> DecodeListByDb(DataTable dt, itable tableClass)
        {
            List<ValueType> infoList = new List<ValueType>();
            foreach (DataRow dr in dt.Rows)
            {
                infoList.Add(tableClass.DecodeOneItemByDb(dr));
            }
            return infoList;
        }
    }
}

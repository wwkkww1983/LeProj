using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZdflCount.App_Start
{
    public class Common
    {
        public static string getStartDate(string strStartDate)
        {//默认起始时间为月初
            if (strStartDate == null || strStartDate == string.Empty)
            {
                strStartDate = DateTime.Now.AddDays(-1 * (DateTime.Now.Day - 1)).ToString(App_Start.Constants.DATE_FORMAT);
            }

            return strStartDate;
        }

        public static string getEndDate(string strEndDate)
        {//默认终点时间为今天
            if (strEndDate == null || strEndDate == string.Empty)
            {
                strEndDate = DateTime.Now.ToString(App_Start.Constants.DATE_FORMAT);
            }

            return strEndDate;
        }
    }
}
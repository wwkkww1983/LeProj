using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;  
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CameraAPPServer
{
    public partial class Default : System.Web.UI.Page
    {
        private static string strPath = System.Web.HttpContext.Current.Server.MapPath("/");
        private static DBUtility db = new DBUtility(strPath,true);

        protected void Page_Load(object sender, EventArgs e)
        {
            //db.InitialTable();  
            string Re = "1";  
            if (Request.RequestType.ToUpper() == "POST")  
            {
                SortedList table = sParam();  
                if (table != null)  
                {
                    int flag = Convert.ToInt32(table["flag"]);
                    string strName = table["user"].ToString();
                    string strPwd = table["pwd"].ToString();

                    if (flag == 0)
                    {
                        Re = db.Login(strName, strPwd) ? "0" : "1";
                    }
                    else
                    {
                        Re = db.Register(strName, strPwd) ? "0" : "1";
                    }
                }
            }  
            else  
            {
                NameValueCollection nvc = GETInput();  
                if (nvc.Count != 0)  
                {  
                    for (int i = 0; i < nvc.Count; i++)  
                    {  
                        Re += "参数名：" + nvc.GetKey(i) + " 值：" + nvc.GetValues(i)[0] + "<br/>";  
                    }  
                }  
                else  
                { Re = "你没有传递任何参数过来！"; }  
            }  
            Response.Write(Re);  
        }

        //获取GET返回来的数据  
        private NameValueCollection GETInput()
        {
            return Request.QueryString;
        }

        // 获取POST返回来的数据  
        private string PostInput()
        {
            try
            {
                System.IO.Stream s = Request.InputStream;
                int count = 0;
                byte[] buffer = new byte[1024];
                StringBuilder builder = new StringBuilder();
                while ((count = s.Read(buffer, 0, 1024)) > 0)
                {
                    builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                s.Flush();
                s.Close();
                s.Dispose();
                return builder.ToString();
            }
            catch (Exception ex)
            { throw ex; }
        }
        private SortedList sParam()
        {
            string POSTStr = PostInput();
            SortedList SortList = new SortedList();
            int index = POSTStr.IndexOf("&");
            string[] Arr = { };
            if (index != -1) //参数传递不只一项  
            {
                Arr = POSTStr.Split('&');
                for (int i = 0; i < Arr.Length; i++)
                {
                    int equalindex = Arr[i].IndexOf('=');
                    string paramN = Arr[i].Substring(0, equalindex);
                    string paramV = Arr[i].Substring(equalindex + 1);
                    if (!SortList.ContainsKey(paramN)) //避免用户传递相同参数  
                    { SortList.Add(paramN, paramV); }
                    else //如果有相同的，一直删除取最后一个值为准  
                    { SortList.Remove(paramN); SortList.Add(paramN, paramV); }
                }
            }
            else //参数少于或等于1项  
            {
                int equalindex = POSTStr.IndexOf('=');
                if (equalindex != -1)
                { //参数是1项  
                    string paramN = POSTStr.Substring(0, equalindex);
                    string paramV = POSTStr.Substring(equalindex + 1);
                    SortList.Add(paramN, paramV);

                }
                else //没有传递参数过来  
                { SortList = null; }
            }
            return SortList;
        } 
    }
}
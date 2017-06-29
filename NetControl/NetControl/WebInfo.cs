using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace NetControl
{
    class WebInfo
    {
        /// <summary>
        /// 每个页面读取后休息时间（1s）
        /// </summary>
        public static float SleepOnePage = 3;

        /// <summary>
        /// Get网页信息（UTF8编码）
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <returns>网页内容</returns>
        public static string GetPageInfo(string strUrl)
        {
            return GetPageInfo(strUrl, Encoding.UTF8);
        }

        /// <summary>
        /// Get网页信息
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <param name="codeType">文字编码</param>
        /// <returns>网页内容</returns>
        public static string GetPageInfo(string strUrl, Encoding codeType)
        {
            HttpWebRequest requestGet;
            // 创建一个HTTP请求
            requestGet = (System.Net.HttpWebRequest)WebRequest.Create(strUrl);
            requestGet.Method = "get";
            requestGet.Timeout = 20 * 1000;
            requestGet.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.95 Safari/537.4";
            requestGet.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            requestGet.Headers.Add("Authorization", "Negotiate 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");
            requestGet.Headers.Add("Cookie","ASPSESSIONIDAADDDDTR=PEHDKPAAPLDAOFMMIMCDAJLO");

            ServicePointManager.ServerCertificateValidationCallback = ValidateServiceCertificate;
            HttpWebResponse response = requestGet.GetResponse() as HttpWebResponse;
            StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), codeType);
            string responseText = myreader.ReadToEnd();
            myreader.Close();

            System.Threading.Thread.Sleep((int)(SleepOnePage * 1000));
            return responseText;

        }

        /// <summary>
        /// 默认通过证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyError"></param>
        /// <returns></returns>
        private static bool ValidateServiceCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyError)
        {
            return true;
        }

        /// <summary>
        ///  Post 获取网页（UTF8编码）
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string PostPageInfo(string strUrl, IDictionary<string, string> parameters)
        {
            return PostPageInfo(strUrl, parameters, Encoding.UTF8);
        }

        /// <summary>
        /// Post 获取网页
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <param name="parameters">Post参数</param>
        /// <param name="codeType">文字编码</param>
        /// <returns></returns>
        public static string PostPageInfo(string strUrl,IDictionary<string,string> parameters, Encoding codeType)
        {
            System.Net.HttpWebRequest request;
            // 创建一个HTTP请求
            request = (System.Net.HttpWebRequest)WebRequest.Create(strUrl);
            request.Method = "post";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E)";
            request.Accept = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, */*";
            request.ContentType = "application/x-www-form-urlencoded";
            ServicePointManager.ServerCertificateValidationCallback = ValidateServiceCertificate;

            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = codeType.GetBytes(buffer.ToString());
                //byte[] data = codeType.GetBytes("__EVENTTARGET=ctl00%24ContentPlaceHolder1%24lnkBtnNext&__EVENTARGUMENT=&__VIEWSTATE=%2fwEPDwULLTIwNDA3MjA1NjEPFgYeCFNRTHdoZXJlZR4JUGFnZUNvdW50AlQeCVBhZ2VJbmRleAIBFgJmD2QWAgIDD2QWAgIDD2QWAgIRDw8WAh4EVGV4dAUu56ysMemhtS%2flhbE4NOmhtSzlhbE4NDDmnaHorrDlvZXvvIzmr4%2fpobUxMOadoWRkGAIFHl9fQ29udHJvbHNSZXF1aXJlUG9zdEJhY2tLZXlfXxYCBSZjdGwwMCRDb250ZW50UGxhY2VIb2xkZXIxJEltZ0J0blNlYXJjaAUiY3RsMDAkQ29udGVudFBsYWNlSG9sZGVyMSRJbWdidG5HbwUuY3RsMDAkQ29udGVudFBsYWNlSG9sZGVyMSRHcmlkVmlld1Byb2plY3RRdWVyeQ88KwAKAQgCAWSxTYsY8owvvZoc7TfqZh%2byMIE9tQ%3d%3d&__EVENTVALIDATION=%2fwEWDAKLzLXzBQL0jYbCAgKE%2bY66BwK5sODPAwLTyqyUCgL0j4yvDQKp8KfXDQLbpd2VCwLWl7irCAKJqeahCwKX1%2fTcAQLvn7%2fuCC4ZwVyjJND9DI4p2vLIyobfXmmd");
                request.ContentLength = data.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader myreader = new StreamReader(response.GetResponseStream(), codeType);
            string responseText = myreader.ReadToEnd();
            myreader.Close();

            System.Threading.Thread.Sleep((int)(SleepOnePage * 1000));
            return responseText;
        }
    }
}
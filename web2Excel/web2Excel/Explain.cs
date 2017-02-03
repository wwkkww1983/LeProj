using System;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace web2Excel
{
    /// <summary>
    /// 解析数据
    /// </summary>
    class Explain
    {
        private static Encoding GB2312Code = Encoding.GetEncoding("GB2312");

        /// <summary>
        /// 解析最外层项目页面
        /// </summary>
        /// <param name="strHtml">原始网页内容</param>
        /// <param name="result">解析后的结果</param>
        public static void FirstProj(string strHtml, List<string> result)
        {
            if (result == null)
                result = new List<string>();

            string strPattern = "<a href=\"[^\"]+\">进入>></a>";
            Regex regex = new Regex(strPattern);
            if (!regex.IsMatch(strHtml)) return;

            int startIdx=0, endIdx = 0;
            MatchCollection matchList = regex.Matches(strHtml);
            foreach (Match item in matchList)
            {
                startIdx = item.Value.IndexOf('"');
                endIdx = item.Value.LastIndexOf('"');
                result.Add(item.Value.Substring(startIdx+1,endIdx-startIdx-1));
            }
        }

        /// <summary>
        /// 匹配项目的页码
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="pageIdx">当前页</param>
        /// <param name="pageCount">总页数</param>
        public static void FirstPage(string strHtml, ref int pageIdx, ref int pageCount)
        {
            string strPattern = @"第\d{1,3}页/共\d{1,3}页,";
            Regex regex = new Regex(strPattern);
            Match matchItem = regex.Match(strHtml);
            if (!matchItem.Success)
            {
                pageIdx = -1;
                return;
            }

            string strResult = matchItem.Value;
            pageIdx = int.Parse(strResult.Substring(1, strResult.IndexOf('/') - 1 - 1));
            pageCount = int.Parse(strResult.Substring(strResult.IndexOf("共") + 1, strResult.Length - 2 - strResult.IndexOf("共") - 1));
        }

        /// <summary>
        /// 获取户型 - 页码
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="pageIdx"></param>
        /// <param name="pageCount"></param>
        public static void GetTypePageNumber(string strHtml, ref int pageIdx, ref int pageCount)
        {
            string strPattern = @"javascript:__doPostBack[\s\S]*?</td>";
            Regex regex = new Regex(strPattern);
            Match matchItem = regex.Match(strHtml);
            if (!matchItem.Success)
            {
                pageIdx = 1;
                pageCount = 1;
                return;
            }

            string strResult = matchItem.Value;
            regex = new Regex("__doPostBack");
            MatchCollection matchList = regex.Matches(strResult);
            pageCount = matchList.Count + 1;
            int startIdx = strResult.IndexOf("<span");
            int endIdx = strResult.IndexOf("</span>");
            if (startIdx >= endIdx)
            {
                pageIdx = 1;
            }
            else
            {
                strResult = strResult.Substring(startIdx + 6, endIdx - startIdx - 6);
                pageIdx = int.Parse(strResult);
            }
        }

        /// <summary>
        /// 解析项目换页中用到的参数
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="postData"></param>
        public static void FirstNextPage(string strHtml, IDictionary<string, string> postData)
        {
            postData.Add("__EVENTTARGET", "ctl00%24ContentPlaceHolder1%24lnkBtnNext");

            NextPagePara(strHtml, postData);
        }

        /// <summary>
        /// 解析项目换页中用到的参数
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="postData"></param>
        public static void ThirdNextPage(string strHtml, IDictionary<string, string> postData)
        {
            postData.Add("__EVENTTARGET", "lnkBtnNext");

            NextPagePara(strHtml, postData);
        }

        /// <summary>
        /// 解析户型网站 - 下一页用到的参数
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="pageNumber"></param>
        /// <param name="postData"></param>
        public static void HouseTypeNextPage(string strHtml, int pageNumber, IDictionary<string, string> postData)
        {
            postData.Add("__EVENTTARGET", "DataGrid1%24ctl19%24ctl0" + pageNumber.ToString ());
            postData.Add("__VIEWSTATEGENERATOR", "DFB943F8");

            NextPagePara(strHtml, postData);
        }

        /// <summary>
        /// 解析下一页参数
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="postData"></param>
        public static void NextPagePara(string strHtml, IDictionary<string, string> postData)
        {
            string strPatternArgument = @"id=""__EVENTARGUMENT[\s\S]*?/>";
            Regex regex = new Regex(strPatternArgument);
            Match matchItem = regex.Match(strHtml);
            string strResult = matchItem.Value;
            string strCoding = strResult.Substring(26, strResult.Length - 32);
            postData.Add("__EVENTARGUMENT", HttpUtility.UrlEncode(strCoding));

            string strPatternView = @"id=""__VIEWSTATE[\s\S]*?/>";
            regex = new Regex(strPatternView);
            matchItem = regex.Match(strHtml);
            strResult = matchItem.Value;
            strCoding = strResult.Substring(24, strResult.Length - 28);
            postData.Add("__VIEWSTATE", HttpUtility.UrlEncode(strCoding));

            string strPatternValid = @"id=""__EVENTVALIDATION[\s\S]*?/>";
            regex = new Regex(strPatternValid);
            matchItem = regex.Match(strHtml);
            strResult = matchItem.Value;
            strCoding = strResult.Substring(30, strResult.Length - 34);
            postData.Add("__EVENTVALIDATION", HttpUtility.UrlEncode(strCoding));
        }

        /// <summary>
        /// 解析房屋栋数
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="floorList"></param>
        public static void SecondBuilding(string strHtml, Dictionary<string, string> floorList)
        {
            if (floorList == null)
                floorList = new Dictionary<string, string>();

            string strPattern = @"<td>\d+栋</td>[\s\S]*?进入>></a>[\s\S]*?进入>></a>";
            Regex regex = new Regex(strPattern);
            if (!regex.IsMatch(strHtml)) return;

            int start1Idx=0,end1Idx = 0, start2Idx = 0, end2Idx=0;
            MatchCollection matchList = regex.Matches(strHtml);
            foreach (Match item in matchList)
            {
                start1Idx = item.Value.IndexOf('>');
                end1Idx = item.Value.IndexOf('栋');
                start2Idx = item.Value.LastIndexOf("href=");
                end2Idx = item.Value.LastIndexOf("target=");
                
                floorList.Add(item.Value.Substring(start1Idx + 1, end1Idx - start1Idx), item.Value.Substring(start2Idx + 6, end2Idx - start2Idx - 6 - 2));
            }
        }

        /// <summary>
        /// 解析房间详细信息
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="info"></param>
        /// <param name="infoList"></param>
        public static void ThirdDetail(string strHtml, Content info, List<Content> infoList)
        {
            if (infoList == null)
                infoList = new List<Content>();

            string strPattern = @"<td>\S*?房</td>[\s\S]*?</tr>";
            Regex regex = new Regex(strPattern);
            if (!regex.IsMatch(strHtml)) return;

            Regex regDetail = new Regex(@"<td>(?<detail>[\s\S]*?)</td>");
            MatchCollection matchList = regex.Matches(strHtml);
            int startIdx = 0, endIdx = 0;
            foreach (Match item in matchList)
            {
                MatchCollection itemDetail = regDetail.Matches(System.Web.HttpUtility.HtmlDecode(item.Value));
                info.LayerCount = itemDetail.Count > 0 ? "'" + itemDetail[0].Groups["detail"].Value : "";
                info.LayerNumber = itemDetail.Count > 1 ? itemDetail[1].Groups["detail"].Value : "";
                info.RoomNumber = itemDetail.Count > 2 ? itemDetail[2].Groups["detail"].Value : "";
                info.RoomType = itemDetail.Count > 3 ? itemDetail[3].Groups["detail"].Value : "";
                info.FunctionName = itemDetail.Count > 4 ? itemDetail[4].Groups["detail"].Value : "";
                info.AreaBuilding = itemDetail.Count > 5 ? itemDetail[5].Groups["detail"].Value : "";
                info.AreaRoom = itemDetail.Count > 6 ? itemDetail[6].Groups["detail"].Value : "";
                info.AreaPublic = itemDetail.Count > 7 ? itemDetail[7].Groups["detail"].Value : "";
                info.HouseStatus = itemDetail.Count > 8 ? itemDetail[8].Groups["detail"].Value : "";
                if (itemDetail.Count <= 9) continue;
                startIdx = itemDetail[9].Groups["detail"].Value.IndexOf("\">");
                endIdx = itemDetail[9].Groups["detail"].Value.IndexOf("</");
                info.IsPrepare = itemDetail[9].Groups["detail"].Value.Substring(startIdx + 2, endIdx - startIdx - 2);

                infoList.Add(info);
            }
        }

        /// <summary>
        /// 户型 - 项目编号
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="projName">项目名称</param>
        /// <param name="idList">检测出的所有项目ID</param>
        public static void TypeProjId(string strHtml, string projName, List<string> idList)
        {
            string strPattern = string.Format(@"<a href=""Information_2007\.aspx\S*>{0}</a>", projName);
            Regex regex = new Regex(strPattern);
            if (!regex.IsMatch(strHtml)) return;

            MatchCollection matchList = regex.Matches(strHtml);

            int startIdx = 0, endIdx = 0;
            foreach (Match item in matchList)
            {
                startIdx = item.Value.IndexOf('{');
                endIdx = item.Value.IndexOf('}');
                idList.Add(item.Value.Substring(startIdx, endIdx - startIdx + 1));
            }
        }

        /// <summary>
        /// 户型 - 项目建筑对应关系
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="buildingList"></param>
        public static void TypeProjBuilding(string strHtml, List<string> buildingList)
        {
            string strPattern = @"<a href=""Detail_2007\.aspx[\s\S]*?>进入>>></a>";
            Regex regex = new Regex(strPattern);
            if (!regex.IsMatch(strHtml)) return;

            MatchCollection matchList = regex.Matches(strHtml);

            int startIdx = 0, endIdx = 0;
            foreach (Match item in matchList)
            {
                startIdx = item.Value.IndexOf("dongNo");
                endIdx = item.Value.IndexOf("target");
                buildingList.Add(item.Value.Substring(startIdx + 7, endIdx - startIdx -9));
            }
        }

        /// <summary>
        /// 户型 - 获取房屋页面地址
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="layerNumber">所在层信息</param>
        /// <param name="roomNumber">房号信息</param>
        /// <returns>最终户型地址</returns>
        public static string TypeRoomUrl(string strHtml, string layerNumber, string roomNumber)
        {            
            string strPattern = string.Format(@"房号:{0}&nbsp;{1}[\s\S]*?</table></td>", layerNumber, roomNumber);
            Regex regex = new Regex(strPattern);
            if (!regex.IsMatch(strHtml)) return "";

            Match matchInfo = regex.Match(strHtml);

            int startIdx = 0, endIdx = 0;
            startIdx = matchInfo.Value.IndexOf("href=");
            endIdx = matchInfo.Value.IndexOf("target");
            string resultUrl= matchInfo.Value.Substring(startIdx + 6, endIdx - startIdx - 8);
            if (layerNumber != "/")
            {
                startIdx = resultUrl.IndexOf("houseNo2=");
                endIdx = resultUrl.IndexOf("&fId");
                string strTmpCode = resultUrl.Substring(startIdx + 9, endIdx-startIdx-9);
                resultUrl = resultUrl.Replace(strTmpCode, System.Web.HttpUtility.UrlEncode(strTmpCode, GB2312Code));
            }
            return resultUrl;
        }

        /// <summary>
        /// 户型 - 获取最终信息
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string TypeRoomInfo(string strHtml)
        {
            string strPattern = @"户型：[\s\S]*?</td>[\s\S]*?</td>";
            Regex regex = new Regex(strPattern);
            if (!regex.IsMatch(strHtml)) return "";

            Match matchInfo = regex.Match(strHtml);

            int startIdx = 0, endIdx = 0;
            startIdx = matchInfo.Value.IndexOf("#003399");
            endIdx = matchInfo.Value.LastIndexOf("</font");
            return matchInfo.Value.Substring(startIdx + 9, endIdx - startIdx - 9);
        }

    }
}

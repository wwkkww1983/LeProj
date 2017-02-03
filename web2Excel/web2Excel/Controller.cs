using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace web2Excel
{
    class Controller
    {
        private const string hostContentPreUrl = "https://61.143.53.130:4433/zhpubweb/";
        private const string typeHousePreUrl = "http://www.zhzgj.gov.cn/house/";
        private int pageCount = 0, pageIdx = -1;
        private int pageStart = 1, pageEnd = 1;
        private int resultIdx = 2;
        private HouseType houseType = HouseType.NONE;
        private Encoding GB2312Code = Encoding.GetEncoding("GB2312");
        private Dictionary<string, string> HouseTypeBuildingInfoList = new Dictionary<string, string>();
        public UpdateBoardValue UpdateProgess;
        public UpdateProjCount updateItemCount;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="houseType">是否需要户型信息</param>
        /// <param name="end">结束页</param>
        /// <param name="start">开始页</param>
        public Controller(HouseType houseType,int start, int end)
        {
            this.houseType = houseType;
            this.pageStart = start;
            this.pageEnd = end;
        }

        /// <summary>
        /// 当前记录数
        /// </summary>
        public int CurrentItemIdx
        {
            get { return this.resultIdx; }
        }

        /// <summary>
        /// 解析信息
        /// </summary>
        /// <param name="foldName">文件夹路径</param>
        /// <param name="fileName">文件名</param>
        public void ExplainNormalInfo(string foldName, string fileName)
        {
            ExcelInfo.CreateExcelFile(foldName, fileName);
            string addressProjectUrl = "projectQuery.aspx";
            string strHtml = WebInfo.GetPageInfo(hostContentPreUrl + addressProjectUrl);
            Explain.FirstPage(strHtml, ref pageIdx, ref pageCount);

            int pageProjIdx = 1;
            while (true)
            {
                if (pageStart <= pageIdx)
                {//仅抓取指定页码之后的页面
                    List<string> projList = new List<string>();
                    Explain.FirstProj(strHtml, projList);
                    foreach (string projItem in projList)
                    {
                        this.ExplainSecondBuilding(projItem);
                        UpdateProgess(false, pageProjIdx++);
                    }
                }
                if (pageIdx >= pageCount || pageIdx >= pageEnd) break;

                //开始下一轮循环
                Dictionary<string,string> nextKey = new Dictionary<string,string> ();
                Explain.FirstNextPage(strHtml,nextKey);
                strHtml = WebInfo.PostPageInfo(hostContentPreUrl + addressProjectUrl, nextKey);
                Explain.FirstPage(strHtml, ref pageIdx, ref pageCount);
            }
            ExcelInfo.FinishExcel();
        }

        /// <summary>
        /// 解析第二层的建筑信息
        /// </summary>
        /// <param name="pageUrl"></param>
        private void ExplainSecondBuilding(string pageUrl)
        {
            Content itemInfo = new Content();
            string strHtml = WebInfo.GetPageInfo(hostContentPreUrl + pageUrl);
            string strDataXml = WebInfo.GetPageInfo(hostContentPreUrl + pageUrl.ToLower() + "&readstate=ajax2&type=1");

            XmlDocument projInfo = new XmlDocument();
            if (strDataXml.IndexOf("<SITNUMGATHER>") < 0 && strDataXml.IndexOf("<PROJECTNAME>") < 0)
            {
                itemInfo.ProjectName = "无数据";
            }
            else
            {
                projInfo.LoadXml(strDataXml);
                XmlNode nodeInfo = projInfo.FirstChild.FirstChild;
                itemInfo.Address = nodeInfo.SelectSingleNode("SITNUMGATHER").InnerText;

                if (nodeInfo.SelectSingleNode("PROJECTNAME") == null)
                {
                    itemInfo.ProjectName = "无";
                }
                else
                {
                    itemInfo.ProjectName = nodeInfo.SelectSingleNode("PROJECTNAME").InnerText;
                    if (this.houseType == HouseType.CANSELL || this.houseType == HouseType.ALL)
                    {
                        this.GetProjectInfo(itemInfo.ProjectName);
                    }
                }
            }

            Dictionary<string, string> buildingList = new Dictionary<string, string>();
            Explain.SecondBuilding(strHtml, buildingList);
            foreach (KeyValuePair<string, string> buildingItem in buildingList)
            {
                itemInfo.BlockNumber = buildingItem.Key;
                this.ExplainThirdDetail(System.Web.HttpUtility.HtmlDecode(buildingItem.Value), itemInfo);
                //每栋楼抓完后保存数据
                ExcelInfo.SaveInfo();
            }
        }

        /// <summary>
        /// 解析第三层的详细信息
        /// </summary>
        /// <param name="pageUrl"></param>
        /// <param name="itemInfo">解析的结果</param>
        private void ExplainThirdDetail(string pageUrl, Content itemInfo)
        {            
            //HouseTable    图表形式
            //listhouse     列表形式
            pageUrl = pageUrl.Replace("HouseTable", "listhouse");
            string strHtml = WebInfo.GetPageInfo(hostContentPreUrl + pageUrl);
            int currentPageIdx = 0, currentPageCount = 0;
            Explain.FirstPage(strHtml, ref currentPageIdx, ref currentPageCount);
            while (true)
            {
                List<Content> infoList = new List<Content>();
                Explain.ThirdDetail(strHtml, itemInfo, infoList);

                int itemCount = infoList.Count;
                Content item;
                for (int i = 0; i < infoList.Count; i++)
                {
                    item = infoList[i];
                    if (this.houseType == HouseType.ALL ||
                        this.houseType == HouseType.CANSELL && item.HouseStatus == "可售")
                    {
                        item.RoomType = this.GetRoomTypeInfo(item.ProjectName, item.BlockNumber, item.LayerNumber, item.RoomNumber);
                    }
                    ExcelInfo.InsertItemInfo(1, resultIdx++, item);
                    UpdateProgess(true, resultIdx);
                }

                if (currentPageIdx >= currentPageCount) break;

                //开始下一轮循环
                Dictionary<string, string> nextKey = new Dictionary<string, string>();
                Explain.ThirdNextPage(strHtml, nextKey);
                strHtml = WebInfo.PostPageInfo(hostContentPreUrl + pageUrl, nextKey);
                Explain.FirstPage(strHtml, ref currentPageIdx, ref currentPageCount);
            }
        }

        /// <summary>
        /// 更新对比后结果的户型数据
        /// </summary>
        /// <param name="projList"></param>
        /// <param name="todayExcelName"></param>
        public void UpdateComparedProjectInfo(string todayExcelName,List<string> projList)
        {
            foreach (string strItem in projList)
            {
                GetProjectInfo(strItem);
            }
            string[,] data;

            ExcelInfo.GetNormalList(todayExcelName, out data);
            
            IDictionary<int, string> typeListByItemIdx = new Dictionary<int, string>();
            int row = data.Length/14;
            this.updateItemCount(row);

            for (int i = 0; i < row; i++)
            {
                this.UpdateProgess(false, i+1);
                string strType = this.GetRoomTypeInfo(data[i, 0], data[i, 2], data[i, 4], data[i, 5]);
                typeListByItemIdx.Add(i + 1 + 1, strType);
            }

            ExcelInfo.updateTypeInfo(2, typeListByItemIdx);
            ExcelInfo.FinishExcel();
        }

        /// <summary>
        /// 获取户型 - 准备工作
        /// </summary>
        /// <param name="strProjName"></param>
        public void GetProjectInfo(string strProjName)
        {
            string strRelatProjBuildUrl = "Information_2007.aspx?FId=", strBuildUrl = "Detail_2007.aspx?";
            string strCodeProjName = System.Web.HttpUtility.UrlEncode(strProjName, GB2312Code);
            string strUrl = string.Format("{0}List_2007.aspx?sKey={1}&sDate1=&sDate2=", typeHousePreUrl, strCodeProjName);
            string strHtml = WebInfo.GetPageInfo(strUrl, GB2312Code);

            List<string> projIdList = new List<string>();
            Explain.TypeProjId(strHtml, strProjName, projIdList);
            string strTmpBuildUrl = string.Empty, strTmpBuildInfo = string.Empty;
            foreach (string item in projIdList)
            {
                strHtml = WebInfo.GetPageInfo(typeHousePreUrl + strRelatProjBuildUrl + item, GB2312Code);
                int buildPageIdx = 0, buildPageCount = 0;
                Explain.GetTypePageNumber(strHtml, ref buildPageIdx, ref buildPageCount);
                while (true)
                {
                    List<string> tmpBuilding = new List<string>();
                    Explain.TypeProjBuilding(strHtml, tmpBuilding);
                    foreach (string tmpItem in tmpBuilding)
                    {
                        strTmpBuildUrl = string.Format("fId={0}&dongNo={1}", item, System.Web.HttpUtility.UrlEncode(tmpItem, GB2312Code));
                        strTmpBuildInfo = WebInfo.GetPageInfo(typeHousePreUrl + strBuildUrl + strTmpBuildUrl, GB2312Code);
                        if (HouseTypeBuildingInfoList.ContainsKey(strProjName + tmpItem))
                            HouseTypeBuildingInfoList[strProjName + tmpItem] += strTmpBuildInfo;
                        else
                            HouseTypeBuildingInfoList.Add(strProjName + tmpItem, strTmpBuildInfo);
                    }

                    if (buildPageIdx >= buildPageCount) break;

                    //开始下一轮循环
                    Dictionary<string, string> nextKey = new Dictionary<string, string>();
                    Explain.HouseTypeNextPage(strHtml,buildPageIdx, nextKey);
                    strHtml = WebInfo.PostPageInfo(typeHousePreUrl + strRelatProjBuildUrl + item, nextKey, GB2312Code);
                    Explain.GetTypePageNumber(strHtml, ref buildPageIdx, ref buildPageCount);
                }
            }
        }

        /// <summary>
        /// 获取户型 - 最终户型信息
        /// </summary>
        /// <param name="strProjName">项目名称</param>
        /// <param name="strBuildNumber">建筑栋号</param>
        /// <param name="strLayerNumber">所在层</param>
        /// <param name="strRoomNumber">房间号</param>
        /// <returns>最终户型信息</returns>
        public string GetRoomTypeInfo(string strProjName, string strBuildNumber,string strLayerNumber, string strRoomNumber)
        {
            if(!HouseTypeBuildingInfoList.ContainsKey (strProjName + strBuildNumber)) return string.Empty;

            string strHtml = HouseTypeBuildingInfoList[strProjName + strBuildNumber];
            string roomUrl = Explain.TypeRoomUrl(strHtml, strLayerNumber, strRoomNumber);

            strHtml = WebInfo.GetPageInfo(typeHousePreUrl + roomUrl, GB2312Code);
            return Explain.TypeRoomInfo(strHtml);
        }

    }
}

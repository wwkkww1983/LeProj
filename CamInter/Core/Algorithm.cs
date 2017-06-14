using System;
using System.Collections.Generic;

namespace Core
{
    public class Algorithm
    {
        private List<CameraLens> camList = new List<CameraLens>();
        private List<RingMedium> focusList = new List<RingMedium>();
        private List<RingMedium> adapterList = new List<RingMedium>();
        private List<RingMedium> extendList = new List<RingMedium>();
        private List<RingMedium> ringList = new List<RingMedium>();
        private List<RingMedium> adapterVisitedList = new List<RingMedium>();
        private List<RingMedium> extendVisitedList = new List<RingMedium>();
        private List<RingResult> results = null;
        private List<RingResults> resultsFound = null;
        private Dictionary<int, float> connectorIDLen = new Dictionary<int, float>();
        public Algorithm(List<ValueType> interList, List<ValueType> camList, List<ValueType> connList)
        {
            foreach (ValueType item in interList)
            {
                RingMedium ring = (RingMedium)item;
                this.ringList.Add(ring);
                switch (ring.RingType)
                {
                    case enumProductType.Focus: focusList.Add(ring); break;
                    case enumProductType.Adapter: adapterList.Add(ring); break;
                    case enumProductType.Extend: extendList.Add(ring); break;
                    default: Console.WriteLine("Error Type"); break;
                }
            }
            foreach (ValueType item in camList)
            {
                this.camList.Add((CameraLens)item);
            }
            foreach (ValueType item in connList)
            {
                Connectors connItem = (Connectors)item;
                this.connectorIDLen.Add(connItem.Idx, connItem.Length);
            }
            this.NormalSearchFlag = true;
        }

        public bool NormalSearchFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 根据基本信息获取中间环
        /// </summary>
        /// <param name="camera">相机接口</param>
        /// <param name="flange">相机法兰距</param>
        /// <param name="target">靶面</param>
        /// <param name="ratio">放大倍率</param>
        /// <param name="workLength">工作距离</param>
        /// <param name="workRange">工作范围</param>
        /// <returns></returns>
        public List<RingResults> GetDevicesByBaseInfo(int camera, float flange, float target,  float ratio, float workLength, float workRange)
        {
            results = new List<RingResult>();
            this.resultsFound = new List<RingResults>();
            List<RingMedium> current = new List<RingMedium>();

            List<CameraLens> lensList = this.FindCamera(target,  ratio, workLength, workRange);
            foreach (CameraLens lens in lensList)
            {
                float ringLength = lens.Focus * ratio + lens.Flange - flange;
                float ringRange = 0;
                if (lens.Name.Contains("Sapphire")) ringRange = 10;
                else if (lens.Name.Contains("Diamond")) ringRange = 16;
                else if (lens.Name.Contains("Zirconia")) ringRange = 9;
                this.FindAllRing(lens, current, camera, lens.Connector, ringLength - ringRange, ringLength + ringRange, target);
                //List<RingMedium> focus = this.findFocus(camera, ringLength);
                //foreach (RingMedium item in focus)
                //{
                //    this.findAdpater(lens, item, item.InterDown, lens.Connector, ringLength - item.LengthMax, ringLength - item.LengthMin);                    
                //}
            }

            return this.resultsFound;
        }

        /// <summary>
        /// 找镜头
        /// </summary>
        /// <param name="target">靶面</param>
        /// <param name="ratio">放大倍数</param>
        /// <param name="workLength">工作距离</param>
        /// <param name="workRange">工作距离范围</param>
        /// <returns>有效镜头列表</returns>
        private List<CameraLens> FindCamera(float target,  float ratio, float workLength, float workRange)
        {
            List<CameraLens> result = new List<CameraLens>();
            foreach (CameraLens item in this.camList)
            {
                if (ratio < item.RatioMin || ratio > item.RatioMax || //放大倍率（在镜头支持的范围内）
                    //resolutionLength > 0 && resolutionLength > item.ResolutionLength ||//验证分辨率（镜头分辨率 >= 相机分辨率）
                    //resolutionWidth > 0 && resolutionWidth > item.ResolutionWidth ||
                    target > 0 && target > item.Target)//靶面（镜头最大靶面（直径） >= 相机靶面（对角线））                    
                {
                    continue;
                }
                if (workLength > 0)
                {//验证工作距离（工作距离，在用户输入的范围内）
                    //工作距离 = 镜头焦距 * （2 + 放大倍率 + 1 / 放大倍率）+ HH – Length - 镜头焦距 * 放大倍率 - 镜头法兰到sensor 的距离
                    double camWork = item.Focus * (2 + ratio + 1.0 / ratio) + item.HH - item.Length - item.Focus * ratio - item.Flange;
                    if (camWork < workLength - workRange || camWork > workLength + workRange) continue;
                }
                result.Add(item);
            }
            return result;
        }

        #region 统一寻找
        private void FindAllRing(CameraLens lens, List<RingMedium> current, int interUp, int interDown, float lengthMin, float lengthMax, float target)
        {
            foreach (RingMedium item in this.ringList)
            {
                if (//调焦环：最多有一个
                    item.RingType == enumProductType.Focus && current != null && current.Find(itmp => itmp.RingType == enumProductType.Focus).Idx > 0 ||
                    //调焦环：相机靶面 >= 56，仅用smart focus 23
                    item.RingType == enumProductType.Focus && target >= 56 && item.Name != "Smart Focus 23" && !lens.Name.StartsWith("Xenon") ||
                    //调焦环：名字以Xenon-Zirconia开头的镜头，用V48 to V70
                    item.RingType == enumProductType.Focus && lens.Name.StartsWith("Xenon-Zirconia") && !item.Name.Equals("V48 to V70") ||
                    //调焦环：Xenon-sapphire，xenon-Diamond系列不需要聚焦环
                    item.RingType == enumProductType.Focus && (lens.Name.StartsWith("Xenon-Diamond") || lens.Name.StartsWith("Xenon Sapphire")) ||
                    //转接环：同一规格仅出现一次
                    item.RingType == enumProductType.Adapter && current != null && current.Find(itmp => itmp.RingType == enumProductType.Adapter && itmp.InterUp == item.InterUp && itmp.InterDown == item.InterDown).Idx > 0 ||
                    //转接环：接口口径方向一致
                    item.RingType == enumProductType.Adapter && current != null && current.Find(itmp => itmp.RingType == enumProductType.Adapter && ((this.connectorIDLen[itmp.InterUp] - this.connectorIDLen[itmp.InterDown]) * (this.connectorIDLen[item.InterUp] - this.connectorIDLen[item.InterDown])) < 0).Idx > 0 ||
                    //延长环：独立查找，用于找最优
                    item.RingType == enumProductType.Extend
                    )
                    continue;
                if (item.InterUp == interUp && item.LengthMin < lengthMax)
                {
                    current.Add(item);
                    if (item.InterDown == interDown && 
                        //调焦环：Xenon-sapphire，xenon-Diamond系列不需要聚焦环，其它至少有一个
                            (current.Find(itmp => itmp.RingType == enumProductType.Focus).Idx > 0 ||
                            lens.Name.StartsWith("Xenon-Diamond")||
                            lens.Name.StartsWith("Xenon Zirconia"))
                        )
                    {
                        int idxTmp = current.Count;
                        current.Insert(idxTmp, new RingMedium() { InterUp = interDown, Length = 0, LengthMin = 0, LengthMax = 0 });
                        this.FindAllRingExtend(lens, current, lengthMin - item.LengthMax, lengthMax - item.LengthMin);
                        current.RemoveAt(idxTmp);
                    }
                    else
                    {
                        this.FindAllRing(lens, current, item.InterDown, interDown, lengthMin - item.LengthMax, lengthMax - item.LengthMin, target);
                    }
                    current.Remove(item);
                }
            }
        }

        /// <summary>
        /// 找延长环
        /// </summary>
        /// <param name="lens"></param>
        /// <param name="current"></param>
        /// <param name="lengthMin"></param>
        /// <param name="lengthMax"></param>
        private void FindAllRingExtend(CameraLens lens, List<RingMedium> current, float lengthMin, float lengthMax)
        {
            float lengthMid = (lengthMin + lengthMax) / 2.0f;

            List<List<RingMedium>> allList = new List<List<RingMedium>>();
            List<Dictionary<int, int>> diffCountList = new List<Dictionary<int, int>>();
            this.FindAllExtend(allList, diffCountList,current, 0, lengthMin, lengthMax);
            //this.FindAllExtendLast(allList, lengthMin, lengthMax);

            List<List<RingMedium>> validList = this.FilterBySpecialCondition(allList,lens);
           // if (this.CombinationStructDataList(lens, validList)) return;
            //最接近中间值
            //List<List<RingMedium>> shortestList = this.SearchShortestExtend(validList, lengthMid);
            //根据中间值范围过滤
            List<List<RingMedium>> shortestList = this.FilterByAdpaterRange(validList, lengthMid, lens);
            if (this.CombinationStructDataList(lens, shortestList)) return;
            //最长
            List<List<RingMedium>> mostLengthList = this.SearchMostLengthExtend(shortestList);
            if (this.CombinationStructDataList(lens, mostLengthList)) return;
            //最大口径
            List<List<RingMedium>> mostWidthList = this.SearchMostWidthExtendSimple(mostLengthList);
            if (this.CombinationStructDataList(lens, mostWidthList)) return;

            List<List<RingMedium>> resultList = this.FilterRepeatExtend(mostWidthList);
            this.CombinationStructDataList(lens, resultList, true);
        }

        private List<List<RingMedium>> FilterBySpecialCondition(List<List<RingMedium>> allList,CameraLens lens)
        {
            bool focus23Flag = false,focus7Flag = false, extend25Flag = false, extend10Flag = false,lens120Flag = false;
            
            if (lens.Name.Contains("5.6/120"))
            {
                lens120Flag = true;
            }
            List<List<RingMedium>> validList = new List<List<RingMedium>>();
            foreach (List<RingMedium> itemList in allList)
            {
                focus23Flag = false;
                focus7Flag = false;
                extend25Flag = false;
                extend10Flag = false;
                foreach (RingMedium item in itemList)
                {
                    if (item.RingType == enumProductType.Focus && item.Name == "Smart Focus 23")
                    {
                        focus23Flag = true;
                    }
                    else if(item.RingType == enumProductType.Focus && item.Name == "Smart Focus 7" )
                    {
                        focus7Flag = true;
                    }
                    else if (item.RingType == enumProductType.Extend && item.Number == "20179A")
                    {
                        extend25Flag = true;
                    }
                    else if (item.RingType == enumProductType.Extend && item.Number == "20178A")
                    {
                        extend10Flag = true;
                    }
                }
                if (focus23Flag && lens120Flag && extend25Flag ||
                   focus23Flag && !lens120Flag && extend10Flag ||
                    focus7Flag && lens120Flag && extend25Flag ||
                    focus7Flag && !lens120Flag ||
                    !focus23Flag && !focus7Flag)
                {
                    validList.Add(itemList);
                }
            }
            return validList;
        }

        private float getRangeLength(RingMedium focus)
        {
            float range = 0f;

            if (focus.Name.StartsWith("Smart Focus 23"))
                range = 9f;
            else if (focus.Name.StartsWith("Smart Focus 7"))
                range = 2.5f;
            else if (focus.Name.StartsWith("Smart Focus"))
                range = 2f;

            return range;
        }

        private float getRangeLength(CameraLens lens)
        {
            float range = 0f;

            if (lens.Name.StartsWith("Xenon-Diamond"))
                range = 16f;
            else if (lens.Name.StartsWith("Xenon Zirconia"))
                range = 9f;
            else if (lens.Name.StartsWith("Xenon Sapphire"))
                range = 10f;

            return range;
        }

        private float getMustExtendLength(RingMedium focus, CameraLens lens)
        {
            float range = 0f;

             return range;

            if (lens.Name.Contains("5.6/120") && (focus.Name.StartsWith("Smart Focus 23") || focus.Name.StartsWith("Smart Focus 7")))
            {
                range = 25f;
            }
            else if (focus.Name.StartsWith("Smart Focus 23"))
            {
                range = 10f;
            }

            return range;
        }

        private List<List<RingMedium>> FilterByAdpaterRange(List<List<RingMedium>> allList, float length, CameraLens lens)
        {
            List<List<RingMedium>> shortestList = new List<List<RingMedium>>();
            if (length <= 0) return shortestList;

            foreach (List<RingMedium> itemList in allList)
            {
                float extLength = -length, rangeLength = 0, mustLength = 0;
                rangeLength = this.getRangeLength(lens);
                foreach (RingMedium item in itemList)
                {
                    extLength += item.RingType == enumProductType.Extend ? item.Length : 0;
                    if (item.RingType == enumProductType.Focus) 
                    {
                        rangeLength = rangeLength == 0 ? this.getRangeLength(item) : rangeLength;
                        mustLength = this.getMustExtendLength(item, lens);
                    }
                }

                extLength = Math.Abs(extLength);
                if (length - mustLength >= 0 && extLength <= rangeLength)
                {
                    shortestList.Add(itemList);
                }
            }
            return shortestList;
        }

        private List<List<RingMedium>> SearchShortestExtend(List<List<RingMedium>> allList, float length)
        {
            float minLength = float.MaxValue;
            List<List<RingMedium>> shortestList = new List<List<RingMedium>>();
            foreach (List<RingMedium> itemList in allList)
            {
                float extLength = -length;
                foreach (RingMedium item in itemList)
                {
                    extLength += item.RingType == enumProductType.Extend ? item.Length : 0;
                }
                extLength = Math.Abs(extLength);
                if (extLength < minLength)
                {
                    shortestList.Clear();
                }
                if (extLength <= minLength)
                {
                    minLength = extLength;
                    shortestList.Add(itemList);
                }
            }
            return shortestList;
        }

        private List<List<RingMedium>> SearchMostWidthExtendSimple(List<List<RingMedium>> allList)
        {
            float maxWidth = 0;
            List<List<RingMedium>> mostWidthList = new List<List<RingMedium>>();
            foreach (List<RingMedium> itemList in allList)
            {
                float extLength = 0;
                foreach (RingMedium item in itemList)
                {
                    extLength += item.RingType == enumProductType.Extend ? this.connectorIDLen[item.InterUp] : 0;
                }
                extLength = Math.Abs(extLength);
                if (extLength > maxWidth)
                {
                    mostWidthList.Clear();
                }
                if (extLength >= maxWidth)
                {
                    maxWidth = extLength;
                    mostWidthList.Add(itemList);
                }
            }
            return mostWidthList;
        }

        private List<List<RingMedium>> SearchMostWidthExtend(List<List<RingMedium>> allList)
        {
            List<List<RingMedium>> widthList = new List<List<RingMedium>>(),result = new List<List<RingMedium>>();
            List<List<float>> minList = new List<List<float>>();
            List<float> countList = new List<float> ();
            float minWidth = this.SearchMostWidthExtendMinWidth(allList, widthList, minList);
            float maxCount = this.FindMaxCountInList(minList,countList);
            for (int i = 0; i < countList.Count;i++ )
            {
                if (countList[i] != maxCount) continue;

                result.Add(widthList[i]);                
            }
            return result;
        }

        private float SearchMostWidthExtendMinWidth(List<List<RingMedium>> allList, List<List<RingMedium>> widthList, List<List<float>> minList)
        {
            float minWidth = 0f, extTempWidth = 0f;
            foreach (List<RingMedium> itemList in allList)
            {
                float extWidth = float.MaxValue;
                List<float> minItem = new List<float>();
                foreach (RingMedium item in itemList)
                {
                    if (item.RingType == enumProductType.Extend)
                    {
                        extTempWidth = this.connectorIDLen[item.InterUp];
                        if (!minItem.Contains(extTempWidth)) minItem.Add(extTempWidth);
                        extWidth = Math.Min(extTempWidth, extWidth);
                    }
                }
                if (extWidth > minWidth)
                {
                    widthList.Clear();
                    minList.Clear();
                }
                if (extWidth >= minWidth)
                {
                    minWidth = extWidth;
                    widthList.Add(itemList);
                    minList.Add(minItem);
                }
            }
            return minWidth;
        }

        private float FindMaxCountInList(List<List<float>> minList, List<float> countList)
        {
            float maxValue = 0f, tempCount = 0f ;
            for (int i = 0; i < minList.Count;i++ )
            {
                tempCount = 0f;
                foreach (float temp in minList[i])
                {
                    tempCount += temp;
                }
                countList.Insert(i,tempCount);
                if (maxValue < tempCount)
                {
                    maxValue = tempCount;
                }
            }
            return maxValue;
        }

        private List<List<RingMedium>> SearchMostLengthExtend(List<List<RingMedium>> allList)
        {
            float minCount = float.MaxValue;
            List<List<RingMedium>> countList = new List<List<RingMedium>>();
            foreach (List<RingMedium> itemList in allList)
            {
                float extCount = 0;
                foreach (RingMedium item in itemList)
                {
                    extCount += item.RingType == enumProductType.Extend ? 1 : 0;
                }
                if (extCount < minCount)
                {
                    countList.Clear();
                }
                if (extCount <= minCount)
                {
                    minCount = extCount;
                    countList.Add(itemList);
                }
            }
            return countList;
        }

        private bool CheckExistsItemsWithinList(List<Dictionary<int, int>> diffCountList, List<RingMedium> ring)
        {
            bool exists = false, hasDiff = false;
            Dictionary<int, int> itemCount = new Dictionary<int, int>();
            foreach (RingMedium item in ring)
            {
                if (item.RingType != enumProductType.Extend)
                    continue;
                if (itemCount.ContainsKey(item.Idx))
                    itemCount[item.Idx]++;
                else
                    itemCount.Add(item.Idx, 1);
            }
            exists = false;
            foreach (Dictionary<int, int> item in diffCountList)
            {
                hasDiff = false;
                foreach (KeyValuePair<int, int> itemChild in item)
                {
                    if (!itemCount.ContainsKey(itemChild.Key) || itemCount[itemChild.Key] != itemChild.Value)
                    {
                        hasDiff = true;
                        break;
                    }
                }
                hasDiff = hasDiff ? hasDiff : itemCount.Count != item.Count;
                if (!hasDiff)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
                diffCountList.Add(itemCount);
            return exists;
        }

        private List<List<RingMedium>> FilterRepeatExtend(List<List<RingMedium>> allList)
        {
            List<List<RingMedium>> filterList = new List<List<RingMedium>>();
            List<Dictionary<int, int>> diffCountList = new List<Dictionary<int, int>>();
            foreach (List<RingMedium> itemList in allList)
            {
                if (!this.CheckExistsItemsWithinList(diffCountList, itemList)) 
                {
                    filterList.Add(itemList);
                }
            }
            return filterList;
        }

        private bool CombinationStructDataList(CameraLens lens, List<List<RingMedium>> allList, bool showAll = false)
        {
            bool finish = showAll || allList.Count <= 1;
            if (!finish) return finish;

            foreach (List<RingMedium> itemList in allList)
            {
                List<RingMedium> ringsTmp = new List<RingMedium>(itemList);
                //ringsTmp.RemoveAt(ringsTmp.Count - 1);
                RingResults oneResult = new RingResults()
                {
                    Idx = this.resultsFound.Count + 1,
                    Lens = lens,
                    ringList = ringsTmp
                };
                if (DateTime.Now.Month <= 6 && this.resultsFound.Count < 100000)
                {
                    this.resultsFound.Add(oneResult);
                }
            }

            return finish;
        }

        private void FindAllExtend(List<List<RingMedium>> allList, List<Dictionary<int, int>> diffCountList, List<RingMedium> current, int idx, float lengthMin, float lengthMax)
        {
            if (!this.NormalSearchFlag) return;
            if (lengthMin <= 0 && lengthMax >= 0)
            {
                if (!this.CheckExistsItemsWithinList(diffCountList, current))
                {
                    List<RingMedium> tempResults = new List<RingMedium>();
                    foreach (RingMedium item in current)
                    {
                        tempResults.Add(item);
                    }
                    tempResults.RemoveAt(tempResults.Count-1);
                    allList.Add(tempResults);
                }
            }

            for (int i = idx; i < current.Count; i++)
            {
                RingMedium ring = current[i];
                foreach (RingMedium item in this.extendList)
                {
                    if (ring.InterUp != item.InterDown || item.Length > lengthMax) continue;
                    current.Insert(i, item);
                    this.FindAllExtend(allList,diffCountList, current, i + 1, lengthMin - item.Length, lengthMax - item.Length);
                    current.RemoveAt(i);
                }
            }
        }

        private void FindAllExtendLast(List<List<RingMedium>> allList,float lengthMin, float lengthMax)
        {
            int idxCount = allList.Count;
            for (int i = 0; i < idxCount; i++)
            {
                List<RingMedium> ringLastList = allList[i];
                int k = ringLastList.Count - 1;
                RingMedium ringLast = ringLastList[k];
                float extLength = 0f;
                foreach (RingMedium item in ringLastList)
                {
                    extLength += item.RingType == enumProductType.Extend?item.Length:0f;
                }
                this.FindAllExtendLast(allList, allList[i], lengthMin - extLength, lengthMax - extLength,false);                
            }
        }

        private void FindAllExtendLast(List<List<RingMedium>> allList, List<RingMedium> current, float lengthMin, float lengthMax, bool hasInsertItem)
        {
            if (hasInsertItem && lengthMin <= 0 && lengthMax >= 0)
            {
                List<RingMedium> tempResults = new List<RingMedium>();
                foreach (RingMedium item in current)
                {
                    tempResults.Add(item);
                }
                allList.Add(current);
            }

            int k = current.Count - 1;
            RingMedium ringLast = current[k];
            foreach (RingMedium item in this.extendList)
            {
                if (ringLast.InterDown != item.InterUp || item.Length > lengthMax) continue;
                current.Insert(k + 1, item);
                this.FindAllExtendLast(allList, current, lengthMin - item.Length, lengthMax - item.Length, true);
                current.RemoveAt(k + 1);
            }
        }

        #endregion

        #region 分类寻找
        /// <summary>
        /// 找调焦环
        /// </summary>
        /// <param name="inter">相机接口</param>
        /// <param name="length">长度约束</param>
        /// <returns></returns>
        private List<RingMedium> findFocus(int inter, float length)
        {
            List<RingMedium> result = new List<RingMedium>();
            foreach (RingMedium item in this.focusList)
            {
                if (item.InterUp == inter && item.LengthMin < length)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 找多个转接环
        /// </summary>
        /// <param name="lens">镜头</param>
        /// <param name="focus">调焦环</param>
        /// <param name="upRing">上端（相机）接口</param>
        /// <param name="endRing">下端（镜头）接口</param>
        /// <param name="minLen">最小长度</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns></returns>
        private void findAdpater(CameraLens lens, RingMedium focus, int upRing, int endRing, float minLen, float maxLen)
        {
            if (upRing == endRing)
            {//不需要转接环
                this.FindExtend(lens, focus, adapterVisitedList, endRing, minLen, maxLen);
            }
            foreach (RingMedium item in adapterList)
            {
                if (maxLen <= 0)
                {//上一层已经结束
                    this.adapterVisitedList.RemoveAt(adapterVisitedList.Count - 1);
                    break;
                }
                if (item.InterUp == upRing && item.Length < maxLen)
                {
                    this.adapterVisitedList.Add(item);
                    if (item.InterDown == endRing)
                    {//找到合适
                        this.FindExtend(lens, focus, adapterVisitedList, endRing, minLen - item.Length, maxLen - item.Length);
                    }
                    else
                    {//查找未结束
                        this.findAdpater(lens, focus, item.InterDown, endRing, minLen - item.Length, maxLen - item.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 找延长环
        /// </summary>
        /// <param name="lens">镜头</param>
        /// <param name="focus">调焦环</param>
        /// <param name="adapter">转接环</param>
        /// <param name="inter">接口类型</param>
        /// <param name="minLen">最小长度</param>
        /// <param name="maxLen">最大长度</param>
        private void FindExtend(CameraLens lens, RingMedium focus, List<RingMedium> adapter, int inter, float minLen, float maxLen)
        {
            if (maxLen <= 0)
            {//上一层已经结束
                this.extendVisitedList.RemoveAt(extendVisitedList.Count - 1);
            }
            if (minLen <= 0 && maxLen >= 0)
            {
                this.CombinationStruct(lens, focus, adapter, extendVisitedList);
            }
            foreach (RingMedium item in this.extendList)
            {
                if (item.InterUp == inter && item.Length <= maxLen)
                {
                    this.extendVisitedList.Add(item);
                    this.FindExtend(lens, focus, adapter, inter, minLen - item.Length, maxLen - item.Length);
                }
            }
        }

        private void CombinationStruct(CameraLens lens, RingMedium focus, List<RingMedium> adapter, List<RingMedium> extend)
        {
            List<RingMedium> adapterTmp = new List<RingMedium>(adapter);
            List<RingMedium> extendTmp = new List<RingMedium>(extend);
            RingResult oneResult = new RingResult()
            {
                Idx = this.results.Count + 1,
                Lens = lens,
                Focus = focus,
                AdapterList = adapterTmp,
                ExtendList = extendTmp
            };
            this.results.Add(oneResult);
        }
        #endregion
    }
}

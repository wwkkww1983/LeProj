using System;
using System.Collections.Generic;

namespace CamInter
{
    class Algorithm
    {
        private List<CameraLens> camList = new List<CameraLens>();
        private List<RingMedium> focusList = new List<RingMedium>();
        private List<RingMedium> adapterList = new List<RingMedium>();
        private List<RingMedium> extendList = new List<RingMedium>();
        private List<RingMedium> adapterVisitedList = new List<RingMedium>();
        private List<RingMedium> extendVisitedList = new List<RingMedium>();
        private List<RingResult> results = null;
        public Algorithm(List<ValueType> interList,List<ValueType> camList)
        {
            foreach (ValueType item in interList)
            {
                RingMedium ring = (RingMedium)item;
                switch (ring.RingType)
                {
                    case enumProductType.Focus: focusList.Add(ring); break;
                    case enumProductType.Adapter: adapterList.Add(ring); break;
                    case enumProductType.Extend: adapterList.Add(ring); break;
                    default: Console.WriteLine("Error Type"); break;
                }
            }
            foreach (ValueType item in camList)
            {
                this.camList.Add((CameraLens)item);
            }
        }

        /// <summary>
        /// 根据基本信息获取中间环
        /// </summary>
        /// <param name="lens">镜头接口</param>
        /// <param name="camera">相机接口</param>
        /// <param name="length">总长度</param>
        /// <returns></returns>
        public List<RingResult> GetDevicesByBaseInfo(int camera,float flange, float target, int resolutionLength, int resolutionWidth, float ratio, float workLength, float workRange)
        {
            results = new List<RingResult>();

            List<CameraLens> lensList = this.FindCamera(target, resolutionLength, resolutionWidth, ratio, workLength, workRange);
            foreach (CameraLens lens in lensList)
            {
                float ringLength = lens.Focus * ratio - flange;
                List<RingMedium> focus = this.findFocus(camera, ringLength);
                foreach (RingMedium item in focus)
                {
                    this.findAdpater(item, item.InterDown, lens.Connector, ringLength - item.LengthMax, ringLength - item.LengthMin);
                }
            }

            return results;
        }

        /// <summary>
        /// 找镜头
        /// </summary>
        /// <param name="target">靶面</param>
        /// <param name="resolutionLength">相机分辨率（长）</param>
        /// <param name="resolutionWidth">相机分辨率（宽）</param>
        /// <param name="ratio">放大倍数</param>
        /// <param name="workLength">工作距离</param>
        /// <param name="workRange">工作距离范围</param>
        /// <returns>有效镜头列表</returns>
        private List<CameraLens> FindCamera(float target, int resolutionLength, int resolutionWidth, float ratio, float workLength, float workRange)
        {
            List<CameraLens> result = new List<CameraLens>();
            foreach (CameraLens item in this.camList)
            {
                if (ratio > item.RatioMin || ratio < item.RatioMax || //放大倍率（在镜头支持的范围内）
                    resolutionLength > 0 && resolutionLength > item.ResolutionLength ||//验证分辨率（镜头分辨率 >= 相机分辨率）
                    resolutionWidth > 0 && resolutionWidth > item.ResolutionWidth ||
                    target > 0 && target > item.Target)//靶面（镜头最大靶面（直径） >= 相机靶面（对角线））                    
                {
                    continue;
                }
                if (workLength > 0)
                {//验证工作距离（工作距离，在用户输入的范围内）
                    //工作距离 = 镜头焦距 * （2 + 放大倍率 + 1 / 放大倍率）+ HH – Length
                    double camWork = item.Focus * (2 + ratio + 1.0 / ratio) + item.HH - item.Length;
                    if (camWork < workLength - workRange || camWork > workLength + workRange) continue;
                }
                result.Add(item);
            }
            return result;
        }

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
        /// <param name="upRing">上端（相机）接口</param>
        /// <param name="endRing">下端（镜头）接口</param>
        /// <param name="minLen">最小长度</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns></returns>
        private void findAdpater(RingMedium focus, int upRing,int endRing, float minLen,float maxLen)
        {
            if (upRing == endRing)
            {//不需要转接环
                this.FindExtend(focus, adapterVisitedList,endRing, minLen, maxLen);
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
                        this.FindExtend(focus, adapterVisitedList,endRing, minLen - item.Length, maxLen - item.Length);
                    }
                    else
                    {//查找未结束
                        this.findAdpater(focus, item.InterDown, endRing, minLen - item.Length, maxLen - item.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 找延长环
        /// </summary>
        /// <param name="focus">调焦环</param>
        /// <param name="adapter">转接环</param>
        /// <param name="inter">接口类型</param>
        /// <param name="minLen">最小长度</param>
        /// <param name="maxLen">最大长度</param>
        private void FindExtend(RingMedium focus, List<RingMedium> adapter, int inter, float minLen, float maxLen)
        {
            if (maxLen <= 0)
            {//上一层已经结束
                this.extendVisitedList.RemoveAt(extendVisitedList.Count - 1);
            }
            if (minLen <= 0 && maxLen >=0)
            {
                this.CombinationStruct(focus, adapter, extendVisitedList);
            }
            foreach (RingMedium item in this.extendList)
            {
                if (item.InterUp == inter && item.Length <= maxLen)
                {
                    this.extendVisitedList.Add(item);
                    this.FindExtend(focus, adapter, inter, minLen - item.Length, maxLen - item.Length);
                }
            }
        }

        private void CombinationStruct(RingMedium focus,List<RingMedium> adapter, List<RingMedium> extend)
        {
            List<RingMedium> adapterTmp = new List<RingMedium>(adapter);
            List<RingMedium> extendTmp = new List<RingMedium>(extend);
            RingResult oneResult = new RingResult()
            {
                Idx = this.results.Count,
                Focus = focus,
                AdapterList = adapterTmp,
                ExtendList = extendTmp
            };
            this.results.Add(oneResult);
        }
    }
}

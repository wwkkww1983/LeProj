using System;
using System.Collections.Generic;

namespace CamInter
{
    class Algorithm
    {
        private List<RingMedium> focusList = new List<RingMedium>();
        private List<RingMedium> adapterList = new List<RingMedium>();
        private List<RingMedium> extendList = new List<RingMedium>();
        public Algorithm(List<ValueType> interList)
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
        }

        /// <summary>
        /// 相机基本信息
        /// </summary>
        public CameraLens Camera
        {
            get;
            set;
        }

        /// <summary>
        /// 根据基本信息获取中间环
        /// </summary>
        /// <param name="lens">镜头接口</param>
        /// <param name="camera">相机接口</param>
        /// <param name="length">总长度</param>
        /// <returns></returns>
        public List<RingResult> GetDevicesByBaseInfo(int lens, int camera, float length)
        {
            List<RingResult> result = new List<RingResult>();

            float realLen = length - this.Camera.Flange;
            return result;
        }

        /// <summary>
        /// 找调焦环
        /// </summary>
        /// <param name="inter">镜头接口</param>
        /// <param name="length">长度约束</param>
        /// <returns></returns>
        private List<RingMedium> findFocus(int inter,int length)
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
        /// 找转接环
        /// </summary>
        /// <param name="upRing">上端接口</param>
        /// <param name="endRing">末端接口</param>
        /// <param name="minLen">最小长度</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns></returns>
        private List<RingMedium> findAdpater(int upRing,int endRing, float minLen,float maxLen)
        {
            if (upRing == endRing)
            {//找到合适
                List<RingMedium> one = new List<RingMedium>();
            }
            else if (maxLen <= 0)
            {//当前不合适

            }

            foreach (RingMedium item in adapterList)
            {
                if (item.InterUp == upRing && item.Length < maxLen)
                    findAdpater(item.InterDown, endRing, minLen - item.Length, maxLen - item.Length);
            }

        }
    }
}

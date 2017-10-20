using System;
using System.Text;
using System.Collections.Generic;

namespace ZdflCount.App_Start
{
    public class Coder 
    {
        public const int FACTORY_NORMAL = 0x5A44666C;
        
        /// <summary>
        /// 基本格式编码
        /// </summary>
        /// <param name="data"></param>
        /// <param name="buff"></param>
        private static void EncodeData(NormalDataStruct data, out byte[] buff)
        {
            buff = new byte[data.contentLen + 12];

            byte[] magicByte = ConvertHelper.Int32ToBytes(FACTORY_NORMAL, true);
            Array.Copy(magicByte, buff, 4);
            byte[] cmdByte = ConvertHelper.Int16ToBytes((Int16)data.Code, true);
            Array.Copy(cmdByte, 0, buff, 4, 2);
            byte[] conLenByte = ConvertHelper.Int32ToBytes(data.contentLen, true);
            Array.Copy(conLenByte, 0, buff, 6, 4);
            int randInfo = new Random().Next(0, 0xFF);
            byte[] randInfoByte = ConvertHelper.Int16ToBytes(randInfo, true);
            Array.Copy(randInfoByte, 0, buff, 10, 2);

            if (data.Content != null)
            {
                Array.Copy(data.Content, 0, buff, 12, data.contentLen);
            }
        }

        /// <summary>
        /// 基本格式解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private static NormalDataStruct DecodeData(byte[] buff)
        {
            NormalDataStruct data = new NormalDataStruct();

            int locIdx = 4;
            data.FactoryNumber = ConvertHelper.BytesToInt32(buff, 0, true);
            data.Code = (enumCommandType)ConvertHelper.BytesToInt16(buff, locIdx, true);
            locIdx += 2;

            data.contentLen = ConvertHelper.BytesToInt32(buff, locIdx, true);
            locIdx += 4;

            data.FillField = ConvertHelper.BytesToInt16(buff, locIdx, true);
            locIdx += 2;

            data.Content = new byte[data.contentLen];
            Array.Copy(buff, locIdx, data.Content, 0, data.contentLen);

            return data;
        }

        /// <summary>
        /// 下派施工单编码
        /// </summary>
        /// <param name="buff"></param>
        public static void EncodeSchedule(ZdflCount.Models.Schedules schedule, out byte[] buff)
        {
            byte[] content = new  byte[1024];
            int locIdx=0, tempLen;
            //施工单编号长度
            tempLen = schedule.Number.Length;
            content[locIdx++] = (byte)tempLen;
            //施工单编号
            byte[] numberBytes = Encoding.ASCII.GetBytes(schedule.Number);
            Array.Copy(numberBytes, content, tempLen);
            locIdx += tempLen;
            //施工单商品总数量
            byte[] countByte = ConvertHelper.Int32ToBytes(schedule.ProductCount, true);
            Array.Copy(countByte, content, 4);
            locIdx += 4 ;
            //详细信息数据长度
            tempLen = schedule.DetailInfo.Length;
            content[locIdx++] = (byte)tempLen;
            //详细信息
            byte[] detailBytes = Encoding.UTF8.GetBytes(schedule.DetailInfo);
            Array.Copy(detailBytes, content, tempLen);
            locIdx += tempLen;
            //注意事项数据长度
            tempLen = schedule.NoticeInfo.Length;
            content[locIdx++] = (byte)tempLen;
            //注意事项
            byte[] noticeBytes = Encoding.UTF8.GetBytes(schedule.NoticeInfo);
            Array.Copy(detailBytes, content, tempLen);
            locIdx += tempLen;

            NormalDataStruct data = new NormalDataStruct()
            {
                Code = enumCommandType.DOWN_SHEDULE_SEND,
                contentLen = locIdx,
                Content = content
            };
            EncodeData(data, out buff);
        }

        private static void DecodeChannelInfo(byte[] buff, ref int locIdx, ref ChannelInfo channel)
        {
            channel.PlanCount = ConvertHelper.BytesToInt32(buff, locIdx, true);
            locIdx += 4;
            channel.Finish = ConvertHelper.BytesToInt32(buff, locIdx, true);
            locIdx += 4;
            channel.Exception = ConvertHelper.BytesToInt32(buff, locIdx, true);
            locIdx += 4;
        }

        /// <summary>
        /// 上传生产情况解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static ProductInfo DecodeProductInfo(byte[] buff)
        {
            ProductInfo info = new ProductInfo();
            NormalDataStruct dataInfo = DecodeData(buff);
            byte[] tempData = dataInfo.Content;
            //工号长度
            int locIdx = 1,tempLen = tempData[0];
            //工号
            byte[] numberByte = new byte[tempLen];
            Array.Copy(tempData,locIdx,numberByte,0,tempLen);
            info.StaffNumber = Encoding.ASCII.GetString(numberByte);
            locIdx += tempLen;
            //姓名长度
            tempLen = tempData[locIdx++];
            //工号
            byte[] nameByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, nameByte, 0, tempLen);
            info.StaffName = Encoding.ASCII.GetString(nameByte);
            locIdx += tempLen;
            //通道
            DecodeChannelInfo(tempData, ref locIdx, ref info.Channel1);
            DecodeChannelInfo(tempData, ref  locIdx, ref info.Channel2);
            DecodeChannelInfo(tempData, ref locIdx, ref info.Channel2);
            DecodeChannelInfo(tempData, ref  locIdx, ref info.Channel2);

            return info;
        }
    }
}

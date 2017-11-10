using System;
using System.Text;
using System.Collections.Generic;

namespace ZdflCount.App_Start
{
    public class Coder 
    {
        public const int FACTORY_NORMAL = 0x5A44666C;
        public const int PROTOCOL_HEAD_COUNT = 12;
        
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
            int randInfo = new Random().Next(0, 0xFFFF);
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
        public static NormalDataStruct DecodeData(byte[] buff,ref  NormalDataStruct data)
        {
            int locIdx = 4;
            data.FactoryNumber = ConvertHelper.BytesToInt32(buff, 0, true);
            data.Code = (enumCommandType)ConvertHelper.BytesToInt16(buff, locIdx, true);
            locIdx += 2;

            data.contentLen = ConvertHelper.BytesToInt32(buff, locIdx, true);
            locIdx += 4;

            data.FillField = ConvertHelper.BytesToInt16(buff, locIdx, true);
            locIdx += 2;

            data.Content = new byte[data.contentLen];

            return data;
        }

        /// <summary>
        /// 下派施工单编码
        /// </summary>
        /// <param name="buff"></param>
        public static void EncodeSchedule(ZdflCount.Models.Schedules schedule, out byte[] buff)
        {
            byte[] content = new  byte[1024];
            int locIdx=0, tempLen = 2;
            //机器码
            byte[] machineBytes = ConvertHelper.Int16ToBytes(schedule.MachineId, true);
            Array.Copy(machineBytes, content, tempLen);
            locIdx += tempLen;
            //施工单编号
            byte[] numberBytes = Encoding.ASCII.GetBytes(schedule.Number);
            tempLen = numberBytes.Length;
            content[locIdx++] = (byte)tempLen;
            Array.Copy(numberBytes, 0, content, locIdx, tempLen);
            locIdx += tempLen;
            //施工单商品总数量
            byte[] countByte = ConvertHelper.Int32ToBytes(schedule.ProductCount, true);
            Array.Copy(countByte, 0, content, locIdx, 4);
            locIdx += 4 ;
            //详细信息
            byte[] detailBytes = Encoding.GetEncoding("GBK").GetBytes(schedule.DetailInfo);
            tempLen = detailBytes.Length;
            byte[] lengthByte = ConvertHelper.Int16ToBytes(tempLen, true);
            Array.Copy(lengthByte, 0, content, locIdx, 2);
            locIdx += 2;
            Array.Copy(detailBytes,0, content,locIdx, tempLen);
            locIdx += tempLen;
            //注意事项
            byte[] noticeBytes = Encoding.GetEncoding("GBK").GetBytes(schedule.NoticeInfo);
            tempLen = noticeBytes.Length; 
            lengthByte = ConvertHelper.Int16ToBytes(tempLen, true);
            Array.Copy(lengthByte, 0, content, locIdx, 2);
            locIdx += 2;
            Array.Copy(noticeBytes, 0, content, locIdx, tempLen);
            locIdx += tempLen;

            NormalDataStruct data = new NormalDataStruct()
            {
                Code = enumCommandType.DOWN_SHEDULE_SEND,
                contentLen = locIdx,
                Content = content
            };
            EncodeData(data, out buff);
        }

        /// <summary>
        /// 施工单操作编码
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="buff"></param>
        public static void EncodeScheHandler(ZdflCount.Models.Schedules schedule,enumCommandType command, out byte[] buff)
        {
            byte[] content = new byte[1024];
            int locIdx = 0, tempLen = 2;
            //机器码
            byte[] machineBytes = ConvertHelper.Int16ToBytes(schedule.MachineId, true);
            Array.Copy(machineBytes, content, tempLen);
            locIdx += tempLen;
            //施工单编号
            byte[] numberBytes = Encoding.ASCII.GetBytes(schedule.Number);
            tempLen = numberBytes.Length;
            content[locIdx++] = (byte)tempLen;
            Array.Copy(numberBytes, 0, content, locIdx, tempLen);
            locIdx += tempLen;

            NormalDataStruct data = new NormalDataStruct()
            {
                Code = command,
                contentLen = locIdx,
                Content = content
            };
            EncodeData(data, out buff);
        }

        /// <summary>
        /// 服务器返回结果编码
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="byteResp"></param>
        /// <param name="buff"></param>
        public static void EncodeServerResp(enumCommandType cmd, byte[] byteResp, out byte[] buff)
        {
            NormalDataStruct data = new NormalDataStruct()
            {
                Code = cmd,
                FactoryNumber = FACTORY_NORMAL,
                contentLen = byteResp.Length,
                Content = byteResp
            };
            EncodeData(data, out buff);
        }

    }


}

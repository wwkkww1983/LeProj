using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcVedio
{
    class Coder
    {
        public static void EncodeWifiSearch(out byte[] buff)
        {
            buff = new byte[5];
            int magic = 0x23696e63;

            byte[] magicByte = ConvertHelper.Int32ToBytes(magic, true);
            Array.Copy(magicByte, buff, 4);

            buff[4] = (byte)0;
        }

        public static WifiRespInfo DecodeWifiSearch(byte[] buff)
        {
            WifiRespInfo info = new WifiRespInfo();

            int locIdx = 4;
            info.Magic = ConvertHelper.BytesToInt32(buff, 0,true);
            info.Code = buff[locIdx++];
            
            int wifiIDLen = buff[locIdx++];
            byte[] wifiIDByte = new byte[wifiIDLen];
            Array.Copy(buff, locIdx, wifiIDByte, 0, wifiIDLen);
            locIdx += wifiIDLen;
            info.ID = ConvertHelper.BytesToString(wifiIDByte, Encoding.ASCII);

            int wifiVersionLen = buff[locIdx++];
            byte[] wifiVersionByte = new byte[wifiVersionLen];
            Array.Copy(buff, locIdx, wifiVersionByte, 0, wifiVersionLen);
            locIdx += wifiVersionLen;
            info.Version = ConvertHelper.BytesToString(wifiVersionByte, Encoding.ASCII);

            int wifiWebLen = buff[locIdx++];
            byte[] wifiWebByte = new byte[wifiWebLen];
            Array.Copy(buff, locIdx, wifiWebByte, 0, wifiWebLen);
            locIdx += wifiWebLen;
            info.Web = ConvertHelper.BytesToString(wifiWebByte, Encoding.ASCII);

            int wifiNameLen = buff[locIdx++];
            byte[] wifiNameByte = new byte[wifiNameLen];
            Array.Copy(buff, locIdx, wifiNameByte, 0, wifiNameLen);
            locIdx += wifiNameLen;
            info.Name = ConvertHelper.BytesToString(wifiNameByte, Encoding.ASCII);

            info.IP = string.Format("{0}.{1}.{2}.{3}", buff[locIdx++], buff[locIdx++], buff[locIdx++], buff[locIdx++]);
            info.NetMask = string.Format("{0}.{1}.{2}.{3}", buff[locIdx++], buff[locIdx++], buff[locIdx++], buff[locIdx++]);
            info.IsDhcp = buff[locIdx++] == 1;
            info.IPStatic = string.Format("{0}.{1}.{2}.{3}", buff[locIdx++], buff[locIdx++], buff[locIdx++], buff[locIdx++]);
            info.NetMaskStatic = string.Format("{0}.{1}.{2}.{3}", buff[locIdx++], buff[locIdx++], buff[locIdx++], buff[locIdx++]);
            info.NetGateStatic = string.Format("{0}.{1}.{2}.{3}", buff[locIdx++], buff[locIdx++], buff[locIdx++], buff[locIdx++]);
            info.DNS1 = string.Format("{0}.{1}.{2}.{3}", buff[locIdx++], buff[locIdx++], buff[locIdx++], buff[locIdx++]);
            info.DNS2 = string.Format("{0}.{1}.{2}.{3}", buff[locIdx++], buff[locIdx++], buff[locIdx++], buff[locIdx++]);
            byte[] wifiPort = new byte[2];
            info.Port = ConvertHelper.BytesToInt16(buff, locIdx, true);
            locIdx += 2;

            info.IsHttps = buff[locIdx++] == 1;

            int wifiNOLen = buff[locIdx++];
            byte[] wifiNOByte = new byte[wifiNameLen];
            Array.Copy(buff, locIdx, wifiNOByte, 0, wifiNOLen);
            Array.Reverse(wifiNOByte);
            locIdx += wifiNOLen;
            info.Number = ConvertHelper.BytesToString(wifiNOByte, Encoding.ASCII);

            info.Type = buff[locIdx++];

            return info;
        }

        public static void EncodeLogin1(out byte[] buff)
        {
            buff = new byte[12];

            int magic = 0x20130809;
            byte[] magicByte = ConvertHelper.Int32ToBytes(magic, true);
            Array.Copy(magicByte, buff, 4);

            int command = (int)enumCommandType.LOGIN1_REQ;
            byte[] cmdByte = ConvertHelper.Int16ToBytes(command, true);
            Array.Copy(cmdByte, 0, buff, 4, 2);

            int contentLen = 0;
            byte[] conLenByte = ConvertHelper.Int32ToBytes(contentLen, true);
            Array.Copy(conLenByte, 0, buff, 6, 4);

            int randInfo = new Random().Next(0, 0xFF);
            byte[] randInfoByte = ConvertHelper.Int16ToBytes(randInfo, true);
            Array.Copy(randInfoByte, 0, buff, 10, 2);

            byte[] content= new byte[0];
            Array.Copy(content, 0, buff, 12,0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcVedio
{
    class Coder
    {
        public const int MAGIC_NORMAL = 0x20130809;
        public const string FISH_KEY = "hello kitty and kgb/cia 2011 COPYRIGHT@REECAM 5460";
        public static Blowfish fish = new Blowfish();

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
            info.Code = (enumCommandType)buff[locIdx++];
            
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
            NormalDataStruct data = new NormalDataStruct()
            {
                Code = enumCommandType.LOGIN1_REQ,
                contentLen = 0,
                Content = null
            };

            EncodeData(data, out buff);
        }

        public static void EncodeData(NormalDataStruct data, out byte[] buff)
        {
            buff = new byte[data.contentLen + 12];

            byte[] magicByte = ConvertHelper.Int32ToBytes(MAGIC_NORMAL, true);
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

        public static NormalDataStruct DecodeData(byte[] buff)
        {
            NormalDataStruct data = new NormalDataStruct();

            int locIdx = 4;
            data.MagicNumber = ConvertHelper.BytesToInt32(buff, 0, true);
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

        public static Login1Struct DecodeLogin1(byte[] data)
        {
            Login1Struct login1 = new Login1Struct();
            login1.Length = data[0];
            int len = (login1.Length + 7) / 8 * 8;
            byte[] codeInfo = new byte[data.Length - 1];
            Array.Copy(data,1, codeInfo,0, codeInfo.Length);
            Console.WriteLine(ConvertHelper.BytesToString(codeInfo, Encoding.ASCII));
            if (!fish.Decode(codeInfo, len, FISH_KEY))
                Console.WriteLine("解码失败");
            Console.WriteLine(codeInfo);
            login1.Plain = ConvertHelper.BytesToString(codeInfo, Encoding.ASCII);
            Console.WriteLine(login1.Plain);
            return login1;
        }

        public static void EncodeLogin2(out byte[] buff)
        {
            string name = "admin",encode=string.Empty;
            string pwd = string.Empty;
            byte[] content = new byte[10];
            content[0] = (byte)(name.Length + 1);
            content[1] = 0;

            byte[] byteName =ConvertHelper.StringToBytes(name,Encoding.ASCII);
            fish.Encode(byteName, content[0], FISH_KEY);
            Array.Copy(byteName, 0, content, 2, byteName.Length);
            NormalDataStruct data = new NormalDataStruct()
            {
                Code = enumCommandType.LOGIN2_REQ,
                contentLen = 0,
                Content = content
            };
            EncodeData(data, out buff);
        }

        public static void EncodePlayVedio(out byte[] buff)
        {
            NormalDataStruct data = new NormalDataStruct()
            {
                Code = enumCommandType.PLAY_VIDEO_REQ,
                Content = null,
                contentLen = 0
            };

            EncodeData(data, out buff);
        }
    }
}

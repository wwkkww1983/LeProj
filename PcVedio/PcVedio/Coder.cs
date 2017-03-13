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
            info.Magic = ConvertHelper.BytesToInt32(buff, 0, true);
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
            Array.Copy(data, 1, codeInfo, 0, codeInfo.Length);
            //Console.WriteLine(ConvertHelper.BytesToString(codeInfo, Encoding.ASCII));
            if (!fish.Decode(codeInfo, len, FISH_KEY))
                Console.WriteLine("解码失败");
            //Console.WriteLine(codeInfo);
            login1.Plain = ConvertHelper.BytesToString(codeInfo, Encoding.ASCII);
            login1.Plain = login1.Plain.Substring(0, login1.Length - 1);
            //Console.WriteLine(login1.Plain);
            return login1;
        }

        public static void EncodeLogin2(out byte[] buff)
        {
            string name = "admin";
            string pwd = string.Empty;

            int locIdx = 0;
            int sNameLen = 0, sPwdLen = 0;
            int nameLen = 0, pwdLen = 0, conLen = 0;
            sNameLen = name.Length + 1;
            sPwdLen = pwd.Length + 1;
            nameLen = (sNameLen + 7) / 8 * 8;
            pwdLen = (sPwdLen + 7) / 8 * 8;
            conLen = 1 + nameLen + 1 + pwdLen;

            byte[] byteName = new byte[nameLen];
            for (int i = 0; i < name.Length; i++)
            {
                byteName[i] = (byte)name[i];
            }
            byteName[name.Length] = (byte)0;
            byte[] bytePwd = new byte[pwdLen];
            for (int i = 0; i < pwd.Length; i++)
            {
                bytePwd[i] = (byte)pwd[i];
            }
            bytePwd[pwd.Length] = (byte)0;
            ConvertHelper.StringToBytes(pwd, Encoding.ASCII);
            Random rand = new Random();
            for (int i = sNameLen; i < nameLen; i++)
            {
                byteName[i] = (byte)rand.Next(48, 125);
            }
            for (int i = sPwdLen; i < pwdLen; i++)
            {
                bytePwd[i] = (byte)rand.Next(48, 125);
            }

            byte[] content = new byte[conLen];
            content[locIdx++] = (byte)(sNameLen);
            fish.Encode(byteName, nameLen, FISH_KEY);
            Array.Copy(byteName, 0, content, locIdx, nameLen);
            locIdx += nameLen;
            content[locIdx++] = (byte)(sPwdLen);            
            fish.Encode(bytePwd, pwdLen, FISH_KEY);
            Array.Copy(bytePwd, 0, content, locIdx, pwdLen);

            NormalDataStruct data = new NormalDataStruct()
            {
                Code = enumCommandType.LOGIN2_REQ,
                contentLen = conLen,
                Content = content
            };
            EncodeData(data, out buff);

            //byte[] bf1 = new byte[] { 0xd0, 0xab, 0x22, 0x8a, 0x50, 0x35, 0x33, 0x11 };
            //byte[] bf2 = new byte[] { 0x92, 0x5c, 0x81, 0xf6, 0x8d, 0xdf, 0xfb, 0x60 };
            //byte[] bf3 = new byte[] { 0xed, 0xdf, 0xd8, 0x9e, 0x11, 0xce, 0x7b, 0xd5 };
            //fish.Decode(bf1, nameLen, FISH_KEY);
            //fish.Decode(bf2, nameLen, FISH_KEY);
            //fish.Decode(bf3, nameLen, FISH_KEY);
        }

        public static void EncodePlayVedio(out byte[] buff)
        {
            NormalDataStruct data = new NormalDataStruct()
            {
                Code = enumCommandType.PLAY_VIDEO_REQ,
                Content = new byte[4],
                contentLen = 4
            };

            EncodeData(data, out buff);
        }

        public static enumRespResult DecodeResult(byte[] data)
        {
            enumRespResult result = (enumRespResult)data[0];

            return result;
        }

        public static VideoData DecodeVideo(int len, byte[] data,byte[] before)
        {
            VideoData video = new VideoData();
            int tmpMagic = ConvertHelper.BytesToInt32(data, 0, true);
            video.NewFlag = (tmpMagic == MAGIC_NORMAL);
            int locIdx = 12;
            if (video.NewFlag)
            {
                video.Type = (enumVideoZipType)data[locIdx++];
                video.Resolution = (enumVideoResolution)data[locIdx++];
                video.Video = data[locIdx++];
                video.NeedPhoto = data[locIdx++];
                video.IsKeyImg = data[locIdx++];

                video.Time = ConvertHelper.BytesToInt32(data, locIdx, true);
                locIdx += 4;
                video.TakeTime = ConvertHelper.BytesToInt32(data, locIdx, true);
                locIdx += 4;
                video.Length = ConvertHelper.BytesToInt32(data, locIdx, true);
                locIdx += 4;

                if (len >= locIdx)
                {
                    video.Data = new byte[len - locIdx];
                    Array.Copy(data, locIdx, video.Data, 0, len - locIdx);
                }
            }
            else
            {
                locIdx = 0;
                video.Data = new byte[len - locIdx + before.Length];
                Array.Copy(before, video.Data, before.Length);
                Array.Copy(data, locIdx, video.Data, before.Length, len - locIdx);
            }
            //Console.WriteLine("{0}={1}", len, video.NewFlag);

            //try
            //{
            //    System.IO.MemoryStream ms2 = new System.IO.MemoryStream(video.Data);
            //    System.Drawing.Image img2 = System.Drawing.Image.FromStream(ms2);
            //    img2.Save("bbb.jpg");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            return video;
        }

        public static void EncodeKeepAlive(out byte[] buff)
        {
            buff = new byte[12];
            NormalDataStruct data = new NormalDataStruct()
            {
                Code = enumCommandType.KEEP_ALIVE,
                contentLen = 0,
                Content = null
            };

            EncodeData(data, out buff);
        }
    }
}
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

        public static void DecodeWifiSearch(byte[] buff)
        {

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

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

            buff[4] = 0;
        }

        public static void DecodeWifiSearch(byte[] buff)
        {

        }
    }
}

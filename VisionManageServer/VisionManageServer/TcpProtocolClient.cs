using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace VisionManageServer
{

    public class ImageInfo
    {
        public long PhoneNumber { get; set; }
        public long TimeStamp { get; set; }
        public int ImageLen { get; set; }
        public byte[] ImageContent { get; set; }
    }

    public class TcpProtocolClient 
    {
        private const int SERVER_PORT_NUMBER = 55557;
        private const int BUFFER_SIZE = 1024, COMMUNICATION_TIME_OUT = 5000;
        private const int PROTOCOL_HEAD_COUNT = 20;
        private static bool keepListening = false;
        private static Stopwatch sw = new Stopwatch();
        private static TcpListener serverListen = null;
        private static Dictionary<int, NetworkStream> netConnection = new Dictionary<int, NetworkStream>();

        public static bool KeepListening
        {
            get { return keepListening; }
        }



        public static string StartServer()
        {
            string strError = null;
            try
            {
                //端口监听并接受数据
                serverListen = new TcpListener(IPAddress.Any, SERVER_PORT_NUMBER);
                serverListen.Start();
                keepListening = true;

                while (keepListening)
                {
                    Console.WriteLine("开始监听："+ DateTime.Now.ToString());
                    TcpClient serverReceive = serverListen.AcceptTcpClient();
                    string clientIP = serverReceive.Client.RemoteEndPoint.ToString();
                    clientIP = clientIP.Substring(0, clientIP.IndexOf(':'));
                    Console.WriteLine("连接成功==：" + DateTime.Now.ToString());
                    NetworkStream ns = serverReceive.GetStream();
                    string strIP = ((IPEndPoint)serverReceive.Client.RemoteEndPoint).Address.ToString();
                    ReceiveByProtocol(ns, strIP);
                }
            }
            catch (SocketException socketEx)
            {
                strError = socketEx.Message;

                System.Net.NetworkInformation.IPGlobalProperties ipProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
                foreach (IPEndPoint endPoint in ipEndPoints)
                {
                    if (endPoint.Port == SERVER_PORT_NUMBER)
                    {
                        keepListening = true;
                        strError = "操作成功";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return strError;
        }

        public static void StopListen()
        {
            try
            {
                serverListen.Stop();
                keepListening = false;
                IEnumerable<int> allMachine=netConnection.Keys;
                foreach (int intId in allMachine)
                {
                    NetworkStream ns = netConnection[intId];
                    ns.Close();
                    ns.Dispose();
                    ns = null;
                }
                netConnection.Clear();
            }
            catch (Exception ex)
            {
            }
        }
        
        /// <summary>
        /// 读取数据流
        /// </summary>
        /// <param name="bt">读取到的数据流</param>
        /// <param name="nCount">待读取字节数</param>
        /// <returns>是否读取成功</returns>
        private static bool ReadBuffer(NetworkStream ns, int nCount, byte[] bt)
        {
            byte[] inread = new byte[nCount];
            int already_read = 0, this_read = 0;

            sw.Reset();
            sw.Start();
            while (already_read < nCount && ns.CanRead && sw.ElapsedMilliseconds < COMMUNICATION_TIME_OUT)
            {
                this_read = ns.Read(bt, already_read, nCount - already_read);
                already_read = already_read + this_read;
                if (already_read < nCount)
                {//若还有数据未读，则休息10毫秒等待数据
                    Thread.Sleep(10);
                }
            }
            sw.Stop();
            return nCount == already_read;
        }

        private static void HandlerByProtocol(ImageInfo dataInfo)
        {
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(dataInfo.ImageContent);                
                System.Drawing.Bitmap img = new System.Drawing.Bitmap(ms);

                string filePath = string.Format("{0}/{1}", System.Environment.CurrentDirectory,dataInfo.PhoneNumber);
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
                img.Save(string.Format("{0}/{1}.jpg", filePath, dataInfo.TimeStamp), System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Close();
            }
            catch (Exception ex)
            {
                
            }
        }

        private static bool ReceiveByProtocol(NetworkStream ns, ImageInfo info)
        {
            bool result = false;
            byte[] byteHead = new byte[PROTOCOL_HEAD_COUNT];

            if (!ReadBuffer(ns, PROTOCOL_HEAD_COUNT, byteHead))
            {
                return result;
            }
            byte[] tempData = byteHead;
            //手机
            int locIdx = 0;
            info.PhoneNumber = ConvertHelper.BytesToLong(tempData, locIdx, true);
            locIdx += 8;
            //时间戳
            info.TimeStamp = ConvertHelper.BytesToLong(tempData, locIdx, true);
            locIdx += 8;
            //图片长度
            info.ImageLen = ConvertHelper.BytesToInt32(tempData, locIdx, true);
            locIdx += 4;
            info.ImageContent = new byte[info.ImageLen];

            if (!ReadBuffer(ns, info.ImageLen, info.ImageContent))
            {
                return result;
            }
            result = true;
            return result;
        }

        private static void ReceiveByProtocol(NetworkStream ns, string strIP)
        {
            byte[] byteHead = new byte[PROTOCOL_HEAD_COUNT];
            try
            {
                ImageInfo dataInfo = new ImageInfo();
                if(!ReceiveByProtocol(ns, dataInfo))
                    return;
                Console.WriteLine("接受完成==：" + DateTime.Now.ToString());
                HandlerByProtocol(dataInfo);
                Console.WriteLine("处理OK：" + DateTime.Now.ToString());
                ns.Close();
                ns.Dispose();
            }
            catch (Exception ex)
            {                
            }
        }
        
    }
}

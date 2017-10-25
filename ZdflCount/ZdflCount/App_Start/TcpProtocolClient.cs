using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace ZdflCount.App_Start
{
    public class TcpProtocolClient 
    {
        private const int CLIENT_PORT_NUMBER = 5555, SERVER_PORT_NUMBER = 5556;
        private const int BUFFER_SIZE = 1024,COMMUNICATION_TIME_OUT = 1000;
        private static bool KeepListening = false;
        private static Stopwatch sw = new Stopwatch();
        private static Models.DbTableDbContext db = new Models.DbTableDbContext();

        /// <summary>
        /// 下发施工单
        /// </summary>
        /// <param name="deviceIP"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int SendScheduleInfo(string deviceIP, byte[] content)
        {
            int recResult = -1;
            byte[] buffReceive = new byte[BUFFER_SIZE];
            IPEndPoint IpPoint = new System.Net.IPEndPoint(IPAddress.Parse(deviceIP), SERVER_PORT_NUMBER);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                serverSocket.Connect(IpPoint);
                serverSocket.Send(content);
                int byteBuff = serverSocket.Receive(buffReceive);
                NormalDataStruct dataInfo = Coder.DecodeData(buffReceive);
                recResult = new DecodeRespInfo().DecoderHandler(dataInfo.Content);
            }
            catch (Exception ex)
            {
                db.RecordErrorInfo(ex, System.Text.Encoding.ASCII.GetString(content));
            }
            finally
            {
                serverSocket.Close();
                serverSocket = null;
            }
            return recResult;
        }

        public static void StartServer()
        {
            KeepListening = true;

            Thread socketThread = new Thread(Listening);
            socketThread.Start();
        }
        
        private static void Listening()
        {
            TcpListener serverListen = new TcpListener(IPAddress.Any, SERVER_PORT_NUMBER);
            serverListen.Start();

            while (KeepListening)
            {
                TcpClient serverReceive = serverListen.AcceptTcpClient();

                string clientIP = serverReceive.Client.RemoteEndPoint.ToString();
                clientIP = clientIP.Substring(0, clientIP.IndexOf(':'));

                NetworkStream ns = serverReceive.GetStream();
                string strIP = ((IPEndPoint)serverReceive.Client.RemoteEndPoint).Address.ToString ();
                ReceiveByProtocol(ns,strIP);
                //子线程读取数据
                //Thread receiveThread = new Thread(new ParameterizedThreadStart(Receiving));
                //receiveThread.Start(dtChild);
            }
        }

        private static interfaceDecoder<Type> GetDcoderByCommand(enumCommandType type)
        {
            interfaceDecoder<Type> typeResult = null;

            switch (type)
            {
                case enumCommandType.UP_HEART_SEND:
                    typeResult = (interfaceDecoder<Type>)new DecodeRespInfo();
                    break;

                case enumCommandType.UP_PRODUCT_SEND:
                    typeResult = (interfaceDecoder<Type>)new DecodeProductInfo();
                    break;

                default: break;
            }
            return typeResult;
        }

        private static interfaceDbRecord<Type> GetDbRecordByCommand(enumCommandType type)
        {
            interfaceDbRecord<Type> typeResult = null;

            switch (type)
            {
                case enumCommandType.UP_HEART_SEND:
                    typeResult = (interfaceDbRecord<Type>)new RecordHeart();
                    break;

                case enumCommandType.UP_PRODUCT_SEND:
                    typeResult = (interfaceDbRecord<Type>)new RecordProductInfo();
                    break;

                default: break;
            }
            return typeResult;
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

        private static void ReceiveByProtocol(NetworkStream ns, string strIP)
        {
            byte[] byteHead = new byte[Coder.PROTOCOL_HEAD_COUNT];
            try
            {
                if (!ReadBuffer(ns, Coder.PROTOCOL_HEAD_COUNT, byteHead))
                {
                    Console.WriteLine("数据头读取超时：", System.Text.Encoding.Default.GetString(byteHead));
                    return;
                }
                NormalDataStruct dataInfo = Coder.DecodeData(byteHead);

                if (!ReadBuffer(ns, dataInfo.contentLen, dataInfo.Content))
                {
                    Console.WriteLine("数据主体读取超时：", System.Text.Encoding.Default.GetString(dataInfo.Content));
                    return;
                }
                interfaceDecoder<Type> decoder = GetDcoderByCommand(dataInfo.Code);
                interfaceDbRecord<Type> record = GetDbRecordByCommand(dataInfo.Code);
                Type infoDetail = decoder.DecoderHandler(dataInfo.Content);
                record.RecordInfo(infoDetail, strIP);
            }
            catch (Exception ex)
            {
                //Logger.WriteLog("接受数据失败", ex, "请检查网络链接");
            }
        }
    }
}

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
        private const int CLIENT_PORT_NUMBER = 6000, SERVER_PORT_NUMBER = 55556;
        private const int BUFFER_SIZE = 1024,COMMUNICATION_TIME_OUT = 1000;
        private static bool keepListening = false;
        private static Stopwatch sw = new Stopwatch();
        private static Models.DbTableDbContext db = new Models.DbTableDbContext();

        public static bool KeepListening
        {
            get { return keepListening; }
        }
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
            IPEndPoint IpPoint = new System.Net.IPEndPoint(IPAddress.Parse(deviceIP), CLIENT_PORT_NUMBER);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                serverSocket.Connect(IpPoint);
                serverSocket.Send(content);
                int byteBuff = serverSocket.Receive(buffReceive);
                NormalDataStruct dataInfo = Coder.DecodeData(buffReceive);
                recResult = Coder.DecodeClientResp(dataInfo.Content);
            }
            catch (Exception ex)
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpSenderException, ex, System.Text.Encoding.ASCII.GetString(content), content);
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
            keepListening = true;

            Thread socketThread = new Thread(Listening);
            socketThread.Start();
        }
        
        private static void Listening()
        {
            try
            {
                TcpListener serverListen = new TcpListener(IPAddress.Any, SERVER_PORT_NUMBER);
                serverListen.Start();

                while (keepListening)
                {
                    TcpClient serverReceive = serverListen.AcceptTcpClient();

                    string clientIP = serverReceive.Client.RemoteEndPoint.ToString();
                    clientIP = clientIP.Substring(0, clientIP.IndexOf(':'));

                    NetworkStream ns = serverReceive.GetStream();
                    string strIP = ((IPEndPoint)serverReceive.Client.RemoteEndPoint).Address.ToString();
                    ReceiveByProtocol(ns, strIP);
                    //子线程读取数据
                    //Thread receiveThread = new Thread(new ParameterizedThreadStart(Receiving));
                    //receiveThread.Start(dtChild);
                }
            }
            catch (Exception ex)
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpListenerException, ex, "监听异常", null);
            }
        }

        private static interfaceClientHanlder  GetHandlerByCommand(enumCommandType type)
        {
            interfaceClientHanlder typeResult = null;

            switch (type)
            {
                case enumCommandType.UP_HEART_SEND:
                    typeResult = new ClientHandlerHeartBreak();
                    break;

                case enumCommandType.UP_PRODUCT_SEND:
                    typeResult = new ClientHanlderProductInfo();
                    break;

                case enumCommandType.UP_DEVICE_SETTING_RESP:
                    typeResult = new ClientHandlerDeviceSetting();
                    break;

                default:
                    typeResult = new ClientHandlerNoneDefault();
                    break;
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
                    db.RecordErrorInfo(enumSystemErrorCode.TcpRecieveErr, "数据头读取超时：" + strIP, byteHead);
                    return;
                }
                NormalDataStruct dataInfo = Coder.DecodeData(byteHead);
                if (!ReadBuffer(ns, dataInfo.contentLen, dataInfo.Content))
                {
                    db.RecordErrorInfo(enumSystemErrorCode.TcpRecieveErr, "数据主体读取超时：" + strIP, byteHead);
                    return;
                }
                //信息处理
                interfaceClientHanlder clientHandler = GetHandlerByCommand(dataInfo.Code);
                byte[] byteResult = clientHandler.HandlerClientData(dataInfo.Content);
                if (clientHandler.ShouldResponse())
                {
                    byte[] buffResp = null;
                    Coder.EncodeServerResp(dataInfo.Code + 1, byteResult, out buffResp);
                    ns.Write(buffResp, 0, buffResp.Length);
                }
            }
            catch (Exception ex)
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpHandlerException, ex, strIP, byteHead);
            }
            finally
            {
                ns.Close();
                ns.Dispose();
                ns = null;
            }
        }
    }
}

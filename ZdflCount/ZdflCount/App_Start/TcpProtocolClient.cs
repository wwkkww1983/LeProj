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
        private const int CLIENT_PORT_NUMBER = 55555, SERVER_PORT_NUMBER = 55556;
        private const int BUFFER_SIZE = 1024,COMMUNICATION_TIME_OUT = 1000;
        private static bool keepListening = false;
        private static Stopwatch sw = new Stopwatch();
        private static Models.DbTableDbContext db = new Models.DbTableDbContext();
        private static TcpListener serverListen = null;
        private static Dictionary<int, NetworkStream> netConnection = new Dictionary<int, NetworkStream>();

        public static bool KeepListening
        {
            get { return keepListening; }
        }

        private static enumErrorCode waittingSendForResp(int machineId, Dictionary<int, bool> downStatus, Dictionary<int, enumErrorCode> downResult)
        {
            enumErrorCode sendResult;
            int i = 0, iMax = 10;
            for (; i < iMax; i++)
            {
                if (downStatus[machineId])
                {
                    Thread.Sleep(200);
                    continue;
                }
                else
                {
                    break;
                }
            }
            sendResult = downStatus[machineId] ? enumErrorCode.DeviceReciveTimeOut : downResult[machineId];

            return sendResult;
        }

        /// <summary>
        /// 主动给设备下发信息
        /// </summary>
        /// <returns></returns>
        private static enumErrorCode CommunicateWithClient(int machineId, byte[] content, int userId, Dictionary<int, bool> downStatus, Dictionary<int, enumErrorCode> downResult)
        {
            enumErrorCode sendResult = enumErrorCode.HandlerSuccess;
            byte[] buffReceive = new byte[BUFFER_SIZE];
            if (!netConnection.ContainsKey(machineId))
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpSenderException, machineId.ToString(), content, userId);
                return enumErrorCode.DeviceNotWork;
            }
            NetworkStream ns = netConnection[machineId];
            try
            {
                ns.Write(content, 0, content.Length);
                if (downStatus.Keys.Contains(machineId))
                    downStatus[machineId] = true;
                else
                    downStatus.Add(machineId, true);
                sendResult = waittingSendForResp(machineId, downStatus, downResult);
            }
            catch (Exception ex)
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpSenderException, ex, machineId.ToString (), content, userId);
                sendResult = enumErrorCode.DeviceCommunicateError;
            }
            return sendResult;
        }

        /// <summary>
        /// 下发施工单
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="content"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static enumErrorCode SendScheduleInfo(int machineId, byte[] content, int userId)
        {
            return CommunicateWithClient(machineId, content, userId, GlobalVariable.DownScheduleWaitStatus, GlobalVariable.DownScheduleRespResult);
        }

        /// <summary>
        /// 关闭施工单
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="content"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static enumErrorCode SendScheduleClose(int machineId, byte[] content, int userId)
        {
            return CommunicateWithClient(machineId, content, userId, GlobalVariable.DownScheCloseWaitStatus, GlobalVariable.DownScheCloseRespResult);
        }

        /// <summary>
        /// 报废施工单
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="content"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static enumErrorCode SendScheduleDiscard(int machineId, byte[] content, int userId)
        {
            return CommunicateWithClient(machineId, content, userId, GlobalVariable.DownScheDiscardWaitStatus, GlobalVariable.DownScheDiscardRespResult);
        }

        public static string StartServer(int userId)
        {
            string strError = null;
            try
            {
                serverListen = new TcpListener(IPAddress.Any, SERVER_PORT_NUMBER);
                serverListen.Start();
                keepListening = true;

                Thread socketThread = new Thread(Listening);
                socketThread.Start();
            }
            catch (SocketException socketEx)
            {
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
                if (!keepListening)
                {
                    strError = socketEx.Message;
                    db.RecordErrorInfo(enumSystemErrorCode.TcpListenerException, socketEx, "监听异常", null, userId);
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                db.RecordErrorInfo(enumSystemErrorCode.TcpListenerException, ex, "监听异常", null, userId);
            }
            return strError;
        }
        
        private static void Listening()
        {
            try
            {
                while (keepListening)
                {
                    TcpClient serverReceive = serverListen.AcceptTcpClient();

                    string clientIP = serverReceive.Client.RemoteEndPoint.ToString();
                    clientIP = clientIP.Substring(0, clientIP.IndexOf(':'));

                    NetworkStream ns = serverReceive.GetStream();
                    string strIP = ((IPEndPoint)serverReceive.Client.RemoteEndPoint).Address.ToString();
                    ReceiveByProtocol(ns, strIP);
                }
            }
            catch (SocketException socketEx)
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpListenerException, socketEx, "正常捕获的可识别异常", null);
            }
            catch (Exception ex)
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpListenerException, ex, "监听异常", null);
            }
        }

        public static void StopListen()
        {
            try
            {
                serverListen.Stop();
                keepListening = false;
            }
            catch (Exception ex)
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpListenerException, ex, "结束监听错误", null);
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

                case enumCommandType.UP_DEVICE_SETTING_SEND:
                    typeResult = new ClientHandlerDeviceSetting();
                    break;

                case enumCommandType.DOWN_SHEDULE_RESP:
                    typeResult = new ClientHandlerDownScheduleResp();
                    break;

                case enumCommandType.DOWN_SHEDULE_CLOSE_RESP:
                    typeResult = new ClientHandlerDownScheCloseResp();
                    break;

                case enumCommandType.DOWN_SHEDULE_DISCARD_RESP:
                    typeResult = new ClientHandlerDownScheDiscardResp();
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

        /// <summary>
        /// 子线程主体
        /// </summary>
        /// <param name="objData"></param>
        private static void ThreadHandler(object objData)
        {
            NormalDataStruct dataInfo = (NormalDataStruct)objData;
            HandlerByProtocol(dataInfo);
            while (true)
            {
                try
                {
                    ReceiveByProtocol(dataInfo.stream, ref dataInfo);
                    HandlerByProtocol(dataInfo);
                }
                catch (System.IO.IOException ioEx)
                {
                    db.RecordErrorInfo(enumSystemErrorCode.TcpMachineStreamException, ioEx, dataInfo.IpAddress, dataInfo.Content);
                    break;
                }
                catch (Exception ex)
                {
                    db.RecordErrorInfo(enumSystemErrorCode.TcpMachineStreamException, ex, dataInfo.IpAddress, dataInfo.Content);
                }
            }
        }

        private static void HandlerByProtocol(NormalDataStruct dataInfo)
        {
            try
            {
                interfaceClientHanlder clientHandler = GetHandlerByCommand(dataInfo.Code);
                byte[] byteResult = clientHandler.HandlerClientData(dataInfo.Content);
                //返回信息
                byte[] buffResp = null;
                if (clientHandler.ShouldResponse())
                {
                    Coder.EncodeServerResp(dataInfo.Code + 1, byteResult, out buffResp);
                    dataInfo.stream.Write(buffResp, 0, buffResp.Length);
                }
                int tempMachineId = dataInfo.Code == enumCommandType.UP_DEVICE_SETTING_SEND ?
                    ConvertHelper.BytesToInt16(buffResp, 1, true) ://设置协议没有设备ID，所以用返回回去的ID
                    ConvertHelper.BytesToInt16(dataInfo.Content, true);//其它协议以设备ID开头

                if (netConnection.ContainsKey(tempMachineId))
                {
                    netConnection[tempMachineId] = dataInfo.stream;
                }
                else
                {
                    netConnection.Add(tempMachineId, dataInfo.stream);
                }
            }
            catch (Exception ex)
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpHandlerException, ex, dataInfo.IpAddress, dataInfo.Content);
            }
        }

        private static bool ReceiveByProtocol(NetworkStream ns, ref NormalDataStruct dataInfo)
        {
            bool result = false;
            byte[] byteHead = new byte[Coder.PROTOCOL_HEAD_COUNT];

            if (!ReadBuffer(ns, Coder.PROTOCOL_HEAD_COUNT, byteHead))
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpRecieveErr, "数据头读取超时", byteHead);
                return result;
            }
            Coder.DecodeData(byteHead, ref dataInfo);
            if (!ReadBuffer(ns, dataInfo.contentLen, dataInfo.Content))
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpRecieveErr, "数据主体读取超时", byteHead);
                return result;
            }
            result = true;
            return result;
        }

        private static void ReceiveByProtocol(NetworkStream ns, string strIP)
        {
            byte[] byteHead = new byte[Coder.PROTOCOL_HEAD_COUNT];
            try
            {
                NormalDataStruct dataInfo = new NormalDataStruct ();
                if(!ReceiveByProtocol(ns, ref dataInfo))
                    return;
                
                dataInfo.stream = ns;
                dataInfo.IpAddress = strIP;
                //子线程处理信息
                Thread ThreadHanler = new Thread(new ParameterizedThreadStart(ThreadHandler));
                ThreadHanler.Start(dataInfo); 
            }
            catch (Exception ex)
            {
                db.RecordErrorInfo(enumSystemErrorCode.TcpRecieveErr, ex, strIP, byteHead);
            }
        }

        
    }
}

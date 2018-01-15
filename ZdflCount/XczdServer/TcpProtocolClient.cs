using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace XczdServer
{
    public class TcpProtocolClient 
    {
        private const int SERVER_PORT_NUMBER = 55556;
        private const int BUFFER_SIZE = 1024, COMMUNICATION_TIME_OUT = 10000;
        private static bool keepListening = false;
        private static Stopwatch sw = new Stopwatch();
        private static DbHandler db = new DbHandler();
        private static TcpListener serverListen = null;
        private static Dictionary<int, NetworkStream> netConnection = new Dictionary<int, NetworkStream>();

        public static bool KeepListening
        {
            get { return keepListening; }
        }

        private static enumErrorCode waittingSendForResp(int machineId, Dictionary<int, bool> downStatus, Dictionary<int, enumErrorCode> downResult)
        {
            enumErrorCode sendResult;
            int i = 0, iMax = 50;
            for (; i < iMax; i++)
            {
                if (downStatus[machineId])
                {
                    Thread.Sleep(100);
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

        public static void DownScheduleHandler()
        {
            ServiceStack.Redis.IRedisClient client = GlobalVariable.RedisClient;
            while (true)
            {
                try
                {
                    int count = client.GetListCount(GlobalVariable.PAGE_SEND_CONTENT);
                    for (int i = 0; i < count; i++)
                    {
                        string strUserKey = client.DequeueItemFromList(GlobalVariable.PAGE_SEND_CONTENT);
                        int machineId = client.Get<int>(GlobalVariable.PRE_DOWN_INFO_MACHINE + strUserKey);
                        byte[] buff = client.Get<byte[]>(GlobalVariable.PRE_DOWN_INFO + strUserKey);

                        string strUser = strUserKey.Substring(0, strUserKey.LastIndexOf('-'));
                        int intUser = int.Parse(strUser);
                        enumErrorCode result = CommunicateWithClient(machineId, buff, intUser);
                        if (result != enumErrorCode.HandlerSuccess)
                        {
                            int intResult = (int)result;
                            client.Set<int>(GlobalVariable.PRE_RESP_DOWN_INFO + strUserKey, intResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    db.InsertErrorInfo(enumSystemErrorCode.TcpSenderException, ex, "", null);
                }
                Thread.Sleep(2000);
            }
        }

        private static enumErrorCode CommunicateWithClient(int machineId, byte[] content, int userId)
        {
            enumErrorCode sendResult = enumErrorCode.HandlerSuccess;
            byte[] buffReceive = new byte[BUFFER_SIZE];
            if (!netConnection.ContainsKey(machineId))
            {
                db.InsertErrorInfo(enumSystemErrorCode.TcpSenderException, null, machineId.ToString(), content);
                return enumErrorCode.DeviceNotWork;
            }
            NetworkStream ns = netConnection[machineId];
            try
            {
                ns.Write(content, 0, content.Length);
            }
            catch (Exception ex)
            {
                db.InsertErrorInfo(enumSystemErrorCode.TcpSenderException, ex, machineId.ToString(), content);
                sendResult = enumErrorCode.DeviceCommunicateError;
            }
            return sendResult;
        }


        public static string StartServer()
        {
            string strError = null;
            try
            {
                //清空原来所有实时数据
                ClearRedisServer();
                //数据下派 子线程
                Thread threadSend = new Thread(DownScheduleHandler);
                threadSend.Start();
                //异常日志写入子线程
                Thread threadErrorInsert = new Thread(db.InsertErrorInfo);
                threadErrorInsert.Start();
                //端口监听并接受数据
                serverListen = new TcpListener(IPAddress.Any, SERVER_PORT_NUMBER);
                serverListen.Start();
                keepListening = true;

                while (keepListening)
                {
                    TcpClient serverReceive = serverListen.AcceptTcpClient();
                    NetStructure.NormalDataStruct dataInfo = new NetStructure.NormalDataStruct();
                    dataInfo.stream = serverReceive.GetStream();;
                    dataInfo.IpAddress = ((IPEndPoint)serverReceive.Client.RemoteEndPoint).Address.ToString();
                    //子线程接收和处理信息
                    Thread ThreadHanler = new Thread(new ParameterizedThreadStart(ThreadHandler));
                    ThreadHanler.Start(dataInfo); 
                }
                //ClientHanlderProductInfo info = new ClientHanlderProductInfo();
                //info.HandlerTest();
            }
            catch (SocketException socketEx)
            {
                db.InsertErrorInfo(enumSystemErrorCode.TcpListenerException, socketEx, "监听异常", null);
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
                db.InsertErrorInfo(enumSystemErrorCode.TcpListenerException, ex, "监听异常", null);
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
                db.InsertErrorInfo(enumSystemErrorCode.TcpListenerException, ex, "结束监听错误", null);
            }
        }

        private static void ClearRedisServer()
        {
            using (ServiceStack.Redis.IRedisClient client = GlobalVariable.RedisClient)
            {
                client.FlushAll();
            }
        }

        private static interfaceClientHanlder GetHandlerByCommand(enumCommandType type)
        {
            interfaceClientHanlder typeResult = null;

            switch (type)
            {
                case enumCommandType.UP_HEART_SEND: typeResult = new ClientHandlerHeartBreak(); break;
                case enumCommandType.UP_PRODUCT_SEND: typeResult = new ClientHanlderProductInfo(); break;
                case enumCommandType.UP_DEVICE_SETTING_SEND: typeResult = new ClientHandlerDeviceSetting(); break;
                case enumCommandType.UP_DEVICE_REPORT_SEND: typeResult = new ClientHandlerDeviceReport(); break;
                case enumCommandType.UP_DEVICE_CALL_MATERIAL_SEND: typeResult = new ClientHandlerDeviceCallMaterial(); break;
                case enumCommandType.UP_DEVICE_STARTEND_SEND: typeResult = new ClientHandlerDeviceStartEnd(); break;
                case enumCommandType.DOWN_SHEDULE_RESP: typeResult = new ClientHandlerDownScheduleResp(); break;
                case enumCommandType.DOWN_SHEDULE_CLOSE_RESP: typeResult = new ClientHandlerDownScheCloseResp(); break;
                case enumCommandType.DOWN_SHEDULE_DISCARD_RESP: typeResult = new ClientHandlerDownScheDiscardResp(); break;

                default: break;//typeResult = new ClientHandlerNoneDefault();
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
                ns.ReadTimeout = COMMUNICATION_TIME_OUT;
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
            NetStructure.NormalDataStruct dataInfo = (NetStructure.NormalDataStruct)objData;
            while (true)
            {
                try
                {
                    ReceiveByProtocol(dataInfo.stream, ref dataInfo);
                    HandlerByProtocol(ref dataInfo);
                }
                catch (System.IO.IOException ioEx)
                {
                    if (!ioEx.Message.Contains("远程主机强迫关闭了一个现有的连接") && !ioEx.Message.Contains("没有正确答复或连接的主机没有反应"))
                    {//忽略设备断开连接的错误，因为可能是设备断电导致，下次上电会自动重连
                        db.InsertErrorInfo(enumSystemErrorCode.TcpMachineStreamException, ioEx, dataInfo.IpAddress, dataInfo.Content);
                    }
                    dataInfo.stream.Close();
                    dataInfo.stream.Dispose();
                    dataInfo.stream = null;
                    if (netConnection.Keys.Contains(dataInfo.MachineId))
                    {
                        netConnection.Remove(dataInfo.MachineId);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    db.InsertErrorInfo(enumSystemErrorCode.TcpMachineStreamException, ex, dataInfo.IpAddress, dataInfo.Content);
                }
            }
        }

        private static void HandlerByProtocol(ref NetStructure.NormalDataStruct dataInfo)
        {
            try
            {
                interfaceClientHanlder clientHandler = GetHandlerByCommand(dataInfo.Code);
                if (clientHandler == null)
                {
                    db.InsertErrorInfo(enumSystemErrorCode.TcpDefaultHandlerErr, null,  dataInfo.IpAddress, dataInfo.Content);
                    return;
                }
                byte[] byteResult = clientHandler.HandlerClientData(dataInfo.Content);
                //返回信息
                byte[] buffResp = null;
                if (clientHandler.ShouldResponse())
                {
                    Coder.EncodeServerResp(dataInfo.Code + 1, byteResult, out buffResp);
                    dataInfo.stream.Write(buffResp, 0, buffResp.Length);
                }
                int tempMachineId = dataInfo.Code == enumCommandType.UP_DEVICE_SETTING_SEND ?
                    ConvertHelper.BytesToInt16(buffResp, buffResp.Length - 2, true) ://设置协议没有设备ID，所以用返回回去的ID
                    ConvertHelper.BytesToInt16(dataInfo.Content, true);//其它协议以设备ID开头

                dataInfo.MachineId = tempMachineId;
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
                db.InsertErrorInfo(enumSystemErrorCode.TcpHandlerException, ex, dataInfo.IpAddress, dataInfo.Content);
            }
        }

        private static bool ReceiveByProtocol(NetworkStream ns, ref NetStructure.NormalDataStruct dataInfo)
        {
            bool result = false;
            byte[] byteHead = new byte[Coder.PROTOCOL_HEAD_COUNT];

            if (!ReadBuffer(ns, Coder.PROTOCOL_HEAD_COUNT, byteHead))
            {
                db.InsertErrorInfo(enumSystemErrorCode.TcpRecieveErr, null, "数据头读取超时", byteHead);
                return result;
            }
            int headIdx = 0;
            while (true)
            {
                if (byteHead[headIdx] == Coder.PROTOCOL_HEAD_START[0] && byteHead[headIdx + 1] == Coder.PROTOCOL_HEAD_START[1] &&
                    byteHead[headIdx + 2] == Coder.PROTOCOL_HEAD_START[2] && byteHead[headIdx + 3] == Coder.PROTOCOL_HEAD_START[3])
                {
                    break;
                }
                headIdx++;
                if (headIdx > Coder.PROTOCOL_HEAD_COUNT - 4)
                {
                    ReadBuffer(ns, Coder.PROTOCOL_HEAD_COUNT, byteHead);
                    headIdx = 0;
                }
            }
            if (headIdx > 0)
            {
                byte[] byteTemp = new byte[headIdx];
                ReadBuffer(ns, headIdx, byteTemp);
                for (int i = 0; i < Coder.PROTOCOL_HEAD_COUNT - headIdx; i++)
                {
                    byteHead[i] = byteHead[i + headIdx];
                }
                for (int i = Coder.PROTOCOL_HEAD_COUNT - headIdx; i < Coder.PROTOCOL_HEAD_COUNT; i++)
                {
                    byteHead[i] = byteTemp[i + headIdx - Coder.PROTOCOL_HEAD_COUNT];
                }
            }
            Coder.DecodeData(byteHead, ref dataInfo);
            if (!ReadBuffer(ns, dataInfo.contentLen, dataInfo.Content))
            {
                db.InsertErrorInfo(enumSystemErrorCode.TcpRecieveErr, null, "数据主体读取超时", byteHead);
                return result;
            }
            result = true;

            return result;
        }
    }
}

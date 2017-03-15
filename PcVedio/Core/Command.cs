/// <summary>
/// copyright:  Zac (suoxd123@126.com)
/// 2017-03-14
/// </summary>
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Core
{
    public class Command
    {
        private const int PORT_SEARCH = 9527, PORT_VEDIO = 80, BUFFER_SIZE = 1024;
        private const int PORT_SELF_SEARCH = 8888, PORT_SELF_VEDIO = 8889;
        private bool connectFlag = false;
        byte[] buffVideo = new byte[0xFFFFFF];
        private UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, PORT_SELF_SEARCH));
        private Socket socket = NetHelper.CreateTcpSocket();


        public Command()
        {
            //byte[] buff;
            //Coder.EncodePlayVedio(out buff);

            Thread threadRecev = new Thread(RespConnectWifi);
            threadRecev.Start();

            socket.Bind(new IPEndPoint(IPAddress.Any, PORT_SELF_VEDIO));

            //Thread threadTCP = new Thread(ListenPort);
            //threadTCP.Start();
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        public bool Status
        {
            get { return this.connectFlag; }
        }

        public string ConnectWifi()
        {
            string strResult = string.Empty;

            IPEndPoint targetPoint = new IPEndPoint(IPAddress.Broadcast, PORT_SEARCH); 

            byte[] buff;
            Coder.EncodeWifiSearch(out buff);
            client.EnableBroadcast = true;
            client.Send(buff, 5, targetPoint);

            return strResult;
        }

        public void RespConnectWifi()
        {
            IPEndPoint serverPoint = new IPEndPoint(IPAddress.Any, 0);

            while (!connectFlag)
            {
                try
                {
                    byte[] buff = client.Receive(ref serverPoint);
                    WifiRespInfo info = Coder.DecodeWifiSearch(buff);
                    Login(info.IP, info.Port);
                }
                catch { }
            }
        }

        public string Login(string ip, int port)
        {
            string strResult = string.Empty;
            if (!NetHelper.Connect(socket, ip, port))
            {
                strResult = "连接失败";
            }
            else
            {
                byte[] buff;
                Coder.EncodeLogin1(out buff);
                socket.Send(buff);

                byte[] buffReceive = new byte[BUFFER_SIZE];
                int byteBuff = socket.Receive(buffReceive);
                NormalDataStruct dataInfo = Coder.DecodeData(buffReceive);
                Login1Struct loginInfo = Coder.DecodeLogin1(dataInfo.Content);


                Coder.EncodeLogin2(out buff);
                socket.Send(buff);
                byteBuff = socket.Receive(buffReceive);
                dataInfo = Coder.DecodeData(buffReceive);
                enumRespResult loginResult = Coder.DecodeResult(dataInfo.Content);
                if (loginResult != enumRespResult.Success)
                {
                    strResult = loginResult.ToString();
                }
                else
                {
                    Coder.EncodePlayVedio(out buff);
                    socket.Send(buff);
                    byteBuff = socket.Receive(buffReceive);
                    dataInfo = Coder.DecodeData(buffReceive);
                    enumRespResult playResult = Coder.DecodeResult(dataInfo.Content);

                    if (playResult != enumRespResult.Success)
                    {
                        strResult = playResult.ToString();
                    }
                    else
                    {
                        connectFlag = true;
                        //byte[] buffVedio = new byte[] { };
                        //while (true)
                        //{
                        //    buffVedio = this.PlayVideo(buffVedio);
                        //}
                    }
                }

                //string strMsg = ConvertHelper.BytesToString(buff, System.Text.Encoding.UTF8);
                //Console.WriteLine(strMsg);
            }
            return strResult;
        }

        public byte[] PlayVideo(byte [] beforeVideo,ref bool newFlag)
        {
            Array.Clear(buffVideo, 0, buffVideo.Length);
            int byteBuff = socket.Receive(buffVideo);
            VideoData video = Coder.DecodeVideo(byteBuff, buffVideo, beforeVideo);
            newFlag = video.NewFlag;

            return video.Data;
        }

        public void KeepAlive()
        {
            byte[] buff;
            Coder.EncodeKeepAlive(out buff);
            socket.Send(buff);
        }

        public void ListenPort()
        {
            Socket server = NetHelper.CreateTcpSocket();
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, PORT_SELF_VEDIO);
            server.Bind(ipPoint);
            server.Listen(0);
            while (true)
            {
                Socket reader = server.Accept();
                byte[] buff = new byte[BUFFER_SIZE];

                int byteBuff = reader.Receive(buff);
                
                string strMsg = ConvertHelper.BytesToString(buff, System.Text.Encoding.UTF8);
                Console.WriteLine(strMsg);
            }

        }

        public void CloseWifi()
        {
            this.connectFlag = false;

            socket.Close();
            socket.Dispose();

            client.Close();
        }
    }
}

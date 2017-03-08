using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace PcVedio
{
    class Command
    {
        private const int PORT = 9527;
        private const string ROUTE_ADDRESS = "255.255.255.255",selfAddress = "127.0.0.1";
        private Socket socket = NetHelper.CreateTcpSocket();
        private Socket socketWiff = NetHelper.CreateUdpSocket();


        public Command()
        {
            //socketWiff.Bind(new IPEndPoint(IPAddress.Any, PORT));

            UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, PORT)); 
            Thread threadRecev = new Thread(RespConnectWifi);
            threadRecev.Start(client);
        }

        public void ConnectWifi()
        {
            UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            IPEndPoint targetPoint = new IPEndPoint(IPAddress.Parse(ROUTE_ADDRESS), PORT);
            byte[] buff;
            Coder.EncodeWifiSearch(out buff);
            //socketWiff.SendTo(buff, targetPoint);
            client.Send(buff, 5, targetPoint);
        }
        //以一种访问权限不允许的方式做了一个访问套接字的尝试。

        public void RespConnectWifi(object obj)
        {
            UdpClient client = obj as UdpClient;
            IPEndPoint serverPoint = new IPEndPoint (IPAddress.Any,0);
            byte[] buff = client.Receive(ref serverPoint);

            //byte [] buff = new  byte[1024];
            //int msgLen= socketWiff.Receive(buff);
            string strMsg = ConvertHelper.BytesToString(buff, System.Text.Encoding.UTF8);
            
            Console.WriteLine(strMsg);
        }

        public void Connect()
        {
            if (NetHelper.Connect(socket, ROUTE_ADDRESS, PORT))
            {
                byte[] buff;
                Coder.EncodeWifiSearch(out buff);
                socket.Send(buff);
            }
        }

        public void ListenPort()
        {
          TcpListener listener=  NetHelper.CreateTcpListener(selfAddress, PORT);
          listener.Start();
          
          while (true)
          {
              TcpClient tcpConnector = listener.AcceptTcpClient();

              NetworkStream ns = tcpConnector.GetStream();
          }

        }
    }
}

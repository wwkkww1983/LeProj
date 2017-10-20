using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;

namespace ZdflCount.App_Start
{
    public class TcpClient 
    {
        private const int PORT_NUMBER = 1111;
        private const int BUFFER_SIZE = 1024;

        public static int SendProduction(string deviceIP, byte[] content)
        {
            byte[] buffReceive = new byte[BUFFER_SIZE];
            IPEndPoint IpPoint = new System.Net.IPEndPoint(IPAddress.Parse(deviceIP), PORT_NUMBER);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            NetworkStream ns = null;
            try
            {
                //serverSocket.Connect(IpPoint);
                //ns = new NetworkStream(serverSocket);
                //ns.Write(content, 0, content.Length);
                serverSocket.Send(content);
                
                byte[] buffReceive = new byte[BUFFER_SIZE];
                byteBuff = socket.Receive(buffReceive);
                while(ns.Read
            }
            catch (Exception ex)
            {

            }
            finally
            {
                serverSocket.Close();
                if (ns != null)
                {
                    ns.Close();
                    ns.Dispose();
                }
                serverSocket = null;
            }
            return sendSuc;
        }
    }
}

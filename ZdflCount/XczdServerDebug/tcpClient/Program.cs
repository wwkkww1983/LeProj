using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace tcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string strClientIP = Ini.GetItemValue("client", "ip");
            int intClientIP = int.Parse(Ini.GetItemValue("client", "port"));
            int intClient = int.Parse(Ini.GetItemValue("client", "id"));
            string strServerIP = Ini.GetItemValue("server", "ip");
            int intServerPort = int.Parse(Ini.GetItemValue("server", "port"));
            byte[] respByte = new byte[12],data = new byte[] { 0x6C, 0x66, 0x44, 0x5A, 0x65, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x13, 0x00, 0x00 };
            data[12] = (byte)intClient;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Environment.CurrentDirectory + '\\' + "errlog.txt");
            for (int k = 0; k < 5000;k++ )
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(strClientIP), intClientIP + k);
                TcpClient tcpClient = new TcpClient(iep);
                tcpClient.Connect(IPAddress.Parse(strServerIP), intServerPort);
                NetworkStream ns = tcpClient.GetStream();
                for (int i = 0; i < 5000; i++)
                {
                    try
                    {
                        ns.Write(data, 0, data.Length);
                        ns.Read(respByte, 0, 12);
                        System.Threading.Thread.Sleep(1000);
                    }
                    catch(Exception ex)
                    {
                        if (ex.Message.Contains("远程主机强迫关闭了一个现有的连接") || ex.Message.Contains("您的主机中的软件中止了一个已建立的连接"))
                        {
                            ns.Close();
                            ns.Dispose();
                            tcpClient.Close();
                            System.Threading.Thread.Sleep(5000);
                            IPEndPoint tempIep = new IPEndPoint(IPAddress.Parse(strClientIP), intClientIP + k);
                            tcpClient = new TcpClient(tempIep);
                            tcpClient.Connect(IPAddress.Parse(strServerIP), intServerPort);
                            ns = tcpClient.GetStream();
                        }
                        string strError = string.Format("{0}.{1}.{2}\n\r{3}\n\r{4}\r\n", k, i, intClientIP + k, DateTime.Now.ToString("dd HH:mm:ss"), ex.Message);
                        Console.WriteLine(strError);
                        sw.Write(strError);
                    }
                }
                ns.Close();
                tcpClient.Close();
            }
        }
    }
}

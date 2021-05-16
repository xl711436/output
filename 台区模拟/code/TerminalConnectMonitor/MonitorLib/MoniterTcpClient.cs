using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using MonitorLib.Message;

namespace MonitorLib
{
    /// <summary>
    /// socket客户端对象
    /// </summary>
    public class MoniterTcpClient
    {

        private int m_serverPort;
        private string m_serverIp;

        public static int receiveCount = 0;

        private static object lockObj = new object();

        private Socket clientSocket = null;

        private int m_clientIndex;

        public int ClientIndex { get => m_clientIndex; set => m_clientIndex = value; }

        public MoniterTcpClient(string I_ServerIp,int I_ServerPort,int I_ClientIndex)
        {
            m_clientIndex = I_ClientIndex;
            m_serverIp = I_ServerIp;
            m_serverPort = I_ServerPort;

        }

        public void ConnectToServer()
        {
            IPAddress iPAddress = IPAddress.Parse(m_serverIp);
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, m_serverPort);

            clientSocket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            clientSocket.BeginConnect(localEndPoint, new AsyncCallback(ConnectCallback), clientSocket);


        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket serverSocket = (Socket)ar.AsyncState;

                serverSocket.EndConnect(ar);
            }
            catch(Exception exc)
            {
                TraceHelper.TraceInfo(exc.ToString());
            }
        }

        public void Send( String data)
        {
            data = data + "ok\r\n";
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            int bytesSend = clientSocket.Send(byteData);
            TraceHelper.TraceInfo("send " + bytesSend); 
             
        }

        public void Send(byte[] data)
        { 
            int bytesSend = clientSocket.Send(data);
        //    Console.WriteLine("send " + bytesSend); 
        }

        /// <summary>
        /// 开始异步接收消息
        /// </summary>
        public void StartReceive()
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = clientSocket;

                clientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch(Exception exc)
            {
                TraceHelper.TraceInfo("StartReceive  " + exc.Message);
            }
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                int bytesRead = client.EndReceive(ar);

                if(bytesRead > 0)
                {

                    byte[] receive = MessageCommon.ByteCopy(state.buffer, bytesRead);

                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                } 

            }
            catch(Exception e)
            {
                TraceHelper.TraceInfo(e.ToString());
            }
        }


    }
}

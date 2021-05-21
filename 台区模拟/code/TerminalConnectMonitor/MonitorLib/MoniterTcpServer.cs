using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MonitorLib.Message;
using MonitorLib.Enum;

namespace MonitorLib
{
    /// <summary>
    /// socket 服务器对象
    /// </summary>
    public class MoniterTcpServer
    {
        private ManualResetEvent allDone = new ManualResetEvent(false);
        private List<Socket> clientList = new List<Socket>();
        private Thread serverThread;


        private string m_localIp;
        private int m_localPort;

        public List<Socket> ClientList { get => clientList; set => clientList = value; }

        public MoniterTcpServer(string I_LocalIP,int I_LocalPort)
        {
            m_localIp = I_LocalIP;
            m_localPort = I_LocalPort;
        }

        /// <summary>
        /// 开始异步监听
        /// </summary>
        public  void StartListening()
        {
            serverThread = new Thread(() => {
                IPAddress iPAddress = IPAddress.Parse(m_localIp);
                IPEndPoint localEndPoint = new IPEndPoint(iPAddress, m_localPort);

                Socket listener = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(10000);

                    while(true)
                    {
                        allDone.Reset();

                        listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                        allDone.WaitOne();
                    }
                }
                catch(Exception e)
                {
                    TraceHelper.TraceInfo(e.ToString());
                } 
            });

            serverThread.IsBackground = true;
            serverThread.Start();
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            ClientList.Add(handler);

            StateObject state = new StateObject();

            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallBack), state);
             
        }

        public void ReadCallBack(IAsyncResult ar)
        {
            String content = String.Empty;

            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int bytesRead = 0;
            try { 
                bytesRead = handler.EndReceive(ar);
            }
            catch(Exception Exc)
            {
                TraceHelper.TraceInfo("ReadCallBack exception " + Exc.Message);
            }
         

            if (bytesRead > 0)
            {
         

                byte[] receive = MessageCommon.ByteCopy(state.buffer, bytesRead);
                UpMessageTypeEnum MessageType = (UpMessageTypeEnum)receive[6];
                TerminalTypeEnum TerminalType = (TerminalTypeEnum)receive[5];

                TraceHelper.TraceInfo(MessageType.ToString() + "   " + TerminalType.ToString());

                switch(MessageType)
                {
                    case UpMessageTypeEnum.Heartbeat:
                        {
                            break;
                        }
                    case UpMessageTypeEnum.UploadData:
                        {
                           uint TerminalAddress = MessageCommon.GetUint32ByBytes(receive, 8);
                            TraceHelper.TraceInfo(TerminalAddress.ToString()  );

                            switch (TerminalType)
                            {
                                case TerminalTypeEnum.VoltageChanger:
                                    {
                                        break;
                                    }
                                case TerminalTypeEnum.HeadMeter:
                                    {
                                        break;
                                    }
                                case TerminalTypeEnum.BranchMeter:
                                    {
                                        break;
                                    }
                                case TerminalTypeEnum.BoxMeter:
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                            break;
                        }
                    case UpMessageTypeEnum.TimeSearch:
                        {
                          
                            TimeSearchAnswerMessage answerMessage = new TimeSearchAnswerMessage(0);
                            answerMessage.SearcheDateTime = DateTime.Now;

                            Send(handler, answerMessage.GetAllBytes());
                            TraceHelper.TraceInfo("UpMessageTypeEnum.TimeSearch");
                            break;
                        }
                    case UpMessageTypeEnum.MeterTest_Answer:
                        {
                            break;
                        }
                    case UpMessageTypeEnum.SetChannel_Answer:
                        {
                            break;
                        }
                    case UpMessageTypeEnum.SetHeartbeat_Answer:
                        {
                            break;
                        }
                    case UpMessageTypeEnum.SetUpload_Answer:
                        {
                            break;
                        }
                    case UpMessageTypeEnum.StatusSearch_Answer:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }



                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallBack), state);


            } 
        }

        public void Send(Socket handler,String data)
        {
            data = data + "ok\r\n";
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            int bytesSend = handler.Send(byteData); 
        }


        public void Send(Socket handler, byte[] data)
        { 
            int bytesSend = handler.Send(data);
        }


    }
}

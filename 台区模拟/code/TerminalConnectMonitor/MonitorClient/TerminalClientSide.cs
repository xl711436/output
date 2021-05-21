using MonitorLib;
using MonitorLib.Enum;
using MonitorLib.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorClient
{
    public class TerminalClientSide
    {
        public string ServerIp;
        public UInt16 ServerPort;
        public string BakServerIp;
        public UInt16 BakServerPort;

        public TerminalInfo TerminalInfo;

        public Socket ClientSocket = null;


        public ClientStatusTypeEnum ClientStatus;


        public byte HardwareErrorCode;

        public byte HardwareStatusCode;

        public UInt16 HeartbeatInterval;

        public UInt16 UploadDataInterval;

        public UInt16 UploadDataDelay;



        public uint AveragePower;

        public UInt16 LineLossRate;
         

        public DateTime NextHeartBeatTime  ;

        public DateTime NextUploadDateTime  ;

        public int HeartBeatNumber;

        public int StatusSearchNumber;

        public int TimeAnswerNumber;

        public int UploadDataNumber;

        public int SetChannelNumber;


        public TerminalClientSide(string I_ServerIp, int I_ServerPort, TerminalInfo I_TerminalInfo)
        {
            ServerIp = I_ServerIp;
            ServerPort = (UInt16) I_ServerPort;

            BakServerIp = I_ServerIp;
            BakServerPort = (UInt16)I_ServerPort;
            TerminalInfo = I_TerminalInfo;
            ClientStatus = ClientStatusTypeEnum.Init;


            HardwareErrorCode = 0; 
            HardwareStatusCode = 0; 
            HeartbeatInterval = 60; 
            UploadDataInterval = 60; 
            UploadDataDelay = 60;  
            NextHeartBeatTime = DateTime.Now.AddSeconds(5);
            NextUploadDateTime = DateTime.Now.AddSeconds(5);

        }


        public void ConnectToServer()
        {
            IPAddress iPAddress = IPAddress.Parse(ServerIp);
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, ServerPort);

            ClientSocket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Thread.Sleep(1);

            ClientSocket.BeginConnect(localEndPoint, new AsyncCallback(ConnectCallback), ClientSocket);
           
        }


        public int ReConnect()
        {
            int isReConnect = 0;
           
            if (ClientStatus != ClientStatusTypeEnum.Communicated && ClientStatus != ClientStatusTypeEnum.Communicated)
            {
                ConnectToServer();
                TraceHelper.TraceInfo("ReConnect " + TerminalInfo.Address + " " + TerminalInfo.Type     + " ClientStatus" + ClientStatus.ToString()  );
                isReConnect = 1;
            }
            return isReConnect;
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket serverSocket = (Socket)ar.AsyncState;

                serverSocket.EndConnect(ar);

                ClientStatus = ClientStatusTypeEnum.Connected;
                StartReceive();
            }
            catch (Exception exc)
            {
                TraceHelper.TraceInfo("ConnectCallback  " + exc.Message);
                ClientStatus = ClientStatusTypeEnum.Error;
            }
        }

        public bool IsSendHeartBeat()
        {
            bool result = false;
            if (ClientStatus == ClientStatusTypeEnum.Connected || ClientStatus == ClientStatusTypeEnum.Communicated)
            {
                result = NextHeartBeatTime < DateTime.Now && DateTime .Now .Second ==0 ;
            }
       
            return result;

        }
        public bool IsUploadDataBeat()
        {
            bool result = false;
            if (ClientStatus == ClientStatusTypeEnum.Connected || ClientStatus == ClientStatusTypeEnum.Communicated)
            {
                result = NextUploadDateTime < DateTime.Now && DateTime.Now.Second ==0 ;
            }
            return result;

        }

        public void RefreshHeartBeat()
        {
            NextHeartBeatTime = (DateTime.Now > NextHeartBeatTime ? DateTime.Now: NextHeartBeatTime).AddSeconds(HeartbeatInterval-1);
        }

        public void RefreshUploadData()
        {
            NextUploadDateTime = (DateTime.Now > NextUploadDateTime ? DateTime.Now : NextUploadDateTime).AddSeconds(UploadDataInterval-1);
        }
 
 

        public void Send(byte[] data)
        {
            try
            {
                int bytesSend = ClientSocket.Send(data); 
                ClientStatus = ClientStatusTypeEnum.Communicated;
            }
            catch
            {

            }
        }


        public void StartReceive()
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = ClientSocket;
                ClientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception exc)
            {
                TraceHelper.TraceInfo("StartReceive  " + exc.Message);
                ClientStatus = ClientStatusTypeEnum.Error;
            }
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                { 
                    byte[] receive = MessageCommon.ByteCopy(state.buffer, bytesRead);

                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);



                    ConnectManager.ConnectManagerInstance.AddReceiveQueue();

                    //做收到的消息的处理，设置的




                    DownMessageTypeEnum MessageType = (DownMessageTypeEnum)receive[6];
                    TerminalTypeEnum TerminalType = (TerminalTypeEnum)receive[5];

                    TraceHelper.TraceInfo(MessageType.ToString() + "   " + TerminalType.ToString());




                    switch (MessageType)
                    {
                        case DownMessageTypeEnum.TimeSearch_Answer:
                            {
                                TimeSearchAnswerMessage curMessage = TimeSearchAnswerMessage.GetMessageFromBytes(receive);
                                TraceHelper.TraceInfo("DownMessageTypeEnum.TimeSearch_Answer " + curMessage.SearcheDateTime.ToString());

                                TimeAnswerNumber++;
                                break;
                            }
                        case DownMessageTypeEnum.MeterTest_Command:
                            { 
 
                                if(TerminalInfo.Type == TerminalTypeEnum.BoxMeter)
                                {
                                    MeterTestCommandMessage curMessage = MeterTestCommandMessage.GetMessageFromBytes(receive);

                                    MeterTestAnswerMessage reMessage = new MeterTestAnswerMessage(TerminalInfo);

                                    reMessage.AnswerTimestamp = MessageCommon.GetUnixstampByDateTime(DateTime.Now);

                                    reMessage.MeterID = curMessage.MeterID;

                                    reMessage.MeterAddress = TerminalInfo.Address + curMessage.MeterID + 1;

                                    reMessage.MeterDataFlag = curMessage.MeterDataFlag;

                                    reMessage.MeterDataLength = 0;

                                    TraceHelper.TraceInfo("DownMessageTypeEnum.MeterTest_Command " + TerminalInfo.Address + " " + TerminalInfo.Type
                                        + " curMessage.MeterDataFlag " + curMessage.MeterDataFlag + " curMessage.MeterID " + curMessage.MeterID);

                                    Send(reMessage.GetAllBytes());
                                    break;
                                }
                                else
                                {
                                    TraceHelper.TraceInfo("DownMessageTypeEnum.MeterTest_Command  not response " + TerminalInfo.Address + " " + TerminalInfo.Type);
                                }
                                break;
                            }
                        case DownMessageTypeEnum.SetChannel_Command:
                            { 
                                SetChannelCommandMessage curMessage = SetChannelCommandMessage.GetMessageFromBytes(receive); 
                                SetChannelAnswerMessage reMessage = new SetChannelAnswerMessage(TerminalInfo);

                                reMessage.SetResult = 0;
                                reMessage.FirstIp = curMessage.FirstIp;
                                reMessage.FirstPort = curMessage.FirstPort;
                                reMessage.BakIp = curMessage.BakIp;
                                reMessage.BakPort = curMessage.BakPort;

                                ServerIp = curMessage.FirstIp;
                                ServerPort = curMessage.FirstPort;

                                BakServerIp = curMessage.BakIp;
                                BakServerPort = curMessage.BakPort;

                                TraceHelper.TraceInfo("DownMessageTypeEnum.SetChannel_Command " + TerminalInfo.Address + " " + TerminalInfo.Type);

                                Send(reMessage.GetAllBytes());
                                SetChannelNumber++;
                                break;
                            }
                        case DownMessageTypeEnum.SetHeartbeat_Command:
                            {
                                SetHeartbeatCommandMessage curMessage = SetHeartbeatCommandMessage.GetMessageFromBytes(receive);
                                SetHeartbeatAnswerMessage reMessage = new SetHeartbeatAnswerMessage(TerminalInfo);
                                reMessage.SetResult = 0;
                                reMessage.SetInterval = curMessage.HeartbeatInterval;

                                this.HeartbeatInterval = curMessage.HeartbeatInterval;
                                TraceHelper.TraceInfo("DownMessageTypeEnum.SetHeartbeat_Command " + TerminalInfo.Address + " " + TerminalInfo.Type + " "
                              + "SetInterval " + reMessage.SetInterval );
                                Send(reMessage.GetAllBytes());
                                HeartBeatNumber++;
                                break;
                            }
                        case DownMessageTypeEnum.SetUpload_Command:
                            {
                                SetUploadCommandMessage curMessage = SetUploadCommandMessage.GetMessageFromBytes(receive);
                                SetUploadAnswerMessage reMessage = new SetUploadAnswerMessage(TerminalInfo);
                                reMessage.SetResult = 0;
                                reMessage.SetInterval = curMessage.SetInterval;
                                reMessage.SetDelay = curMessage.SetDelay;

                                this.UploadDataInterval = curMessage.SetInterval;
                                this.UploadDataDelay = curMessage.SetDelay;

                                TraceHelper.TraceInfo("DownMessageTypeEnum.SetUpload_Command " + TerminalInfo.Address + " " + TerminalInfo.Type + " " 
                                    + "SetInterval " + reMessage.SetInterval + "SetDelay " + reMessage.SetDelay);
                                Send(reMessage.GetAllBytes());
                                UploadDataNumber++;
                                break;
                            }
                        case DownMessageTypeEnum.StatusSearch_Command:
                            {
                                StatusSearchCommandMessage curMessage = StatusSearchCommandMessage.GetMessageFromBytes(receive);
                                StatusSearchAnswerMessage reMessage = StatusSearchAnswerMessage.GetSampleMessage();


                                reMessage.TerminalType = TerminalInfo.Type;
                                reMessage.MessageFormatVersion = 0; 
                                reMessage.TerminalAddress = TerminalInfo.Address; 
                                reMessage.MessageLength = 170;
                                reMessage.MessageType = UpMessageTypeEnum.StatusSearch_Answer;


                                reMessage.HeartbeatInterval = this.HeartbeatInterval; 
                                reMessage.UploadDataInterval = this.UploadDataInterval; 
                                reMessage.UploadDataDelay = this.UploadDataDelay; 
                                reMessage.FirstIp = this.ServerIp; 
                                reMessage.FirstPort = this.ServerPort; 
                                reMessage.BakIp =this.BakServerIp; 
                                reMessage.BakPort = this.BakServerPort;

                                TraceHelper.TraceInfo("DownMessageTypeEnum.StatusSearch_Command " + TerminalInfo.Address + " " + TerminalInfo.Type );

                                Send(reMessage.GetAllBytes());
                                StatusSearchNumber++;

                                break;
                            }
                       
                        default:
                            {
                                break;
                            }
                    }


                }
                ClientStatus = ClientStatusTypeEnum.Communicated;

            }
            catch (Exception e)
            {
                TraceHelper.TraceInfo(e.ToString());
                ClientStatus = ClientStatusTypeEnum.Error;
            }
        }


        public void DisConnect()
        {
            ClientStatus = ClientStatusTypeEnum.Disconneted;
            ClientSocket.Close();
        
        }

    }

    public enum ClientStatusTypeEnum
    {
        Init = 0,
        Connected =1,
        Communicated =2,
        Disconneted =3,
        Error = 4,

    }
}

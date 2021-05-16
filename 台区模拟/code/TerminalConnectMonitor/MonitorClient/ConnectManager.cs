using MonitorLib;
using MonitorLib.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorClient
{
     
    /// <summary>
    /// 连接管理类
    /// </summary>
    public class ConnectManager
    {
        public static int MaxMessageCount = 1000000;

        public static ConnectManager ConnectManagerInstance;

        //发送的报文总数
        public long TotalMessageCount;

        //错误的报文数量
        public long TotalErrorCount;

        //用于记录操作记录
        public ConcurrentQueue<String> OutPutLogInfo = new ConcurrentQueue<string>();

        //记录每条发送报文的时间
        public ConcurrentQueue<long> SendMessageQueue = new ConcurrentQueue<long>();

        //记录每条接收报文的时间
        public ConcurrentQueue<long> ReceiveMessageQueue = new ConcurrentQueue<long>();

        //当前的客户端对象
        public BlockingCollection<TerminalClientSide>  ClientList = new BlockingCollection<TerminalClientSide>();


        public int HeartBeatNumber;

        public int StatusSearchNumber;

        public int TimeAnswerNumber;

        public int UploadDataNumber;

        public int SetChannelNumber;
        public ConnectManager()
        {
            ConnectManagerInstance = this;
        }

        /// <summary>
        /// 根据配置生成连接
        /// </summary>
        public void GenerateClient()
        {
            List<TerminalClientSide> tempList = new List<TerminalClientSide>();
            if (ConfigHelper.generateType == 1)
            {
                tempList =  GetSampleClientLite();
            }
            else if (ConfigHelper.generateType == 2)
            {
                tempList = GetSampleClient();
            }
            else if (ConfigHelper.generateType == 3)
            {
                tempList = GetSampleClientMax();
            }
            else if (ConfigHelper.generateType == 4)
            {
                tempList = GetSimulationClientMax(1,ConfigHelper.headWarn,ConfigHelper.branchWarn,ConfigHelper.boxWarn,ConfigHelper.thresholdValue);
            }
            else if (ConfigHelper.generateType == 5)
            {
                for(int i =1;i<= ConfigHelper.headNumber; i++)
                {
                    tempList.AddRange( GetSimulationClientMax(i, ConfigHelper.headWarn, ConfigHelper.branchWarn, ConfigHelper.boxWarn, ConfigHelper.thresholdValue));
                } 
            }
            else if (ConfigHelper.generateType == 6)
            {
                for (int i = 0; i < 100; i++)
                {
                    tempList.AddRange(GetSimulationClientMax(i, ConfigHelper.headWarn, ConfigHelper.branchWarn, ConfigHelper.boxWarn, ConfigHelper.thresholdValue));
                }
            }

            foreach (TerminalClientSide cuClient in tempList)
            {
                ClientList.Add(cuClient);
            }
             
            RefreshPowerAndLineLoseRate(); 
        }

        /// <summary>
        /// 根据配送刷新线损信息
        /// </summary>
        public void RefreshPowerAndLineLoseRate()
        {
            Dictionary<uint, uint> powerDic = new Dictionary<uint, uint>();
            Dictionary<uint, UInt16> lossRateDic = new Dictionary<uint, ushort>();

            GetPowerDic(ConfigHelper.headWarn, ConfigHelper.branchWarn, ConfigHelper.boxWarn, ConfigHelper.thresholdValue, powerDic, lossRateDic);

            foreach (TerminalClientSide cuClient in ClientList)
            {
                if (powerDic.ContainsKey(cuClient.TerminalInfo.Address))
                {
                    cuClient.AveragePower = powerDic[cuClient.TerminalInfo.Address];
                    TraceHelper.TraceInfo("功率设置" + cuClient.TerminalInfo.Address + "  " + cuClient.AveragePower);
                } 
            }

            foreach (TerminalClientSide cuClient in ClientList)
            { 
                if (lossRateDic.ContainsKey(cuClient.TerminalInfo.Address))
                {
                    cuClient.LineLossRate = lossRateDic[cuClient.TerminalInfo.Address];
                    TraceHelper.TraceInfo("线损设置" + cuClient.TerminalInfo.Address + "  " + cuClient.LineLossRate);
                }
            }

        }

        public void ConnectServer()
        {
            foreach (TerminalClientSide curClient in ClientList)
            {
                curClient.ConnectToServer();
            }
        }

        public void CheckReceiveNumber()
        {

            int HeartBeatNumber = ClientList.Sum(p => p.HeartBeatNumber);

            int StatusSearchNumber = ClientList.Sum(p => p.StatusSearchNumber);

            int TimeAnswerNumber = ClientList.Sum(p => p.TimeAnswerNumber);

            int UploadDataNumber = ClientList.Sum(p => p.UploadDataNumber);

            int SetChannelNumber = ClientList.Sum(p => p.SetChannelNumber);

            if (HeartBeatNumber > 0)
            {
                OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  收到心跳设置报文" + HeartBeatNumber + "条");
            }

            if (StatusSearchNumber > 0)
            {
                OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  收到状态查询报文" + StatusSearchNumber + "条");
            }

            if (TimeAnswerNumber > 0)
            {
                OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  收到时间应答报文" + TimeAnswerNumber + "条");
            }

            if (UploadDataNumber > 0)
            {
                OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  收到上传数据设置报文" + UploadDataNumber + "条");
            }

            if (SetChannelNumber > 0)
            {
                OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  收到信道设置报文" + SetChannelNumber + "条");
            }


            foreach (TerminalClientSide curClient in ClientList)
            {
                curClient.HeartBeatNumber = 0;
                curClient.StatusSearchNumber = 0;
                curClient.TimeAnswerNumber = 0;
                curClient.UploadDataNumber = 0;
                curClient.SetChannelNumber = 0;
            }


        }
 
 
        public List<TerminalClientSide> GetSampleClient()
        {
            List<TerminalClientSide> result = new List<TerminalClientSide>();

            for (int i = 0; i < 10; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.VoltageChanger;
                tempInfo.Address = (uint)(1000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            for (int i = 0; i < 40; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.HeadMeter;
                tempInfo.Address = (uint)(2000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            for (int i = 0; i < 200; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.BranchMeter;
                tempInfo.Address = (uint)(3000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            for (int i = 0; i < 1000; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.BoxMeter;
                tempInfo.Address = (uint)(4000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            return result;

        }

        public List<TerminalClientSide> GetSampleClientLite()
        {
            List<TerminalClientSide> result = new List<TerminalClientSide>();

            for (int i = 0; i < 1; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.VoltageChanger;
                tempInfo.Address = (uint)(1000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            for (int i = 0; i < 1; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.HeadMeter;
                tempInfo.Address = (uint)(2000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            for (int i = 0; i < 1; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.BranchMeter;
                tempInfo.Address = (uint)(3000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            for (int i = 0; i < 1; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.BoxMeter;
                tempInfo.Address = (uint)(4000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            return result;

        }


        public List<TerminalClientSide> GetSampleClientMax()
        {
            List<TerminalClientSide> result = new List<TerminalClientSide>();

            for (int i = 0; i < 100; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.VoltageChanger;
                tempInfo.Address = (uint)(1000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            for (int i = 0; i < 400; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.HeadMeter;
                tempInfo.Address = (uint)(2000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            for (int i = 0; i < 9500; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.BranchMeter;
                tempInfo.Address = (uint)(3000000 + i);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            //for (int i = 0; i < 7500; i++)
            //{
            //    TerminalInfo tempInfo = new TerminalInfo();
            //    tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.BoxMeter;
            //    tempInfo.Address = (uint)(4000000 + i);

            //    TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
            //    result.Add(curClientSide);
            //}

            return result;

        }

        /// <summary>
        /// 根据配置生成线损信息
        /// </summary>
        /// <param name="I_HeadCount"></param>
        /// <param name="I_BranchCount"></param>
        /// <param name="I_BoxCount"></param>
        /// <param name="I_Threshold"></param>
        /// <param name="powerDic"></param>
        /// <param name="lossRateDic"></param>
        public void  GetPowerDic( int I_HeadCount, int I_BranchCount, int I_BoxCount, int I_Threshold, Dictionary<uint, uint> powerDic, Dictionary<uint, UInt16> lossRateDic )
        {
            Random curRan = new Random();
            for (int headNumber = 0; headNumber < 100; headNumber++)
            {
 
                int I_TargetLostRate = 0;

                if (I_HeadCount == 1)
                {
                    I_TargetLostRate = curRan.Next(I_Threshold + 1, 100);
                }
                else
                {
                    I_TargetLostRate = curRan.Next(0, I_Threshold - 1);
                }
                uint headPower = (uint)(curRan.Next(1000000, 10000000));
                uint branchTotalPower = (uint)(headPower * (100 - I_TargetLostRate) / 100);

                List<int> branchPowerList = MessageCommon.GetRandomList((int)branchTotalPower, 8);
                List<int> thresholdBranchList = MessageCommon.GetRandomThreshold(8, I_BranchCount, I_Threshold);
                List<int> thresholdBoxList = MessageCommon.GetRandomThreshold(90, I_BoxCount, I_Threshold);

                powerDic.Add((uint)(headNumber * 10000000), headPower);


                for (int branchNumber = 0; branchNumber < 8; branchNumber++)
                {
                    int boxSize = branchNumber == 7 ? 6 : 12;

                    powerDic.Add((uint)(headNumber * 10000000 + 1000000 + (branchNumber + 1) * 10000), (uint)branchPowerList[branchNumber]);

                    List<int> branchList = MessageCommon.GetRandomList((int)((uint)branchPowerList[branchNumber] * (100 - thresholdBranchList[branchNumber]) / 100), boxSize);


                    for (int boxNumber = 0; boxNumber < boxSize; boxNumber++)
                    { 
                        powerDic.Add((uint)(headNumber * 10000000 + 1000000 + (branchNumber + 1) * 10000 + (boxNumber + 1) * 10), (uint)branchList[boxNumber]);
                        lossRateDic.Add((uint)(headNumber * 10000000 + 1000000 + (branchNumber + 1) * 10000 + (boxNumber + 1) * 10), (UInt16)thresholdBoxList[branchNumber * 12 + boxNumber]);
                    }

                }

            }

             
        }

        /// <summary>
        /// 生成一个台区的终端对象
        /// </summary>
        /// <param name="I_ChargeID"></param>
        /// <param name="I_HeadCount"></param>
        /// <param name="I_BranchCount"></param>
        /// <param name="I_BoxCount"></param>
        /// <param name="I_Threshold"></param>
        /// <returns></returns>
        public List<TerminalClientSide> GetSimulationClientMax(int I_ChargeID, int I_HeadCount,int I_BranchCount, int I_BoxCount,int I_Threshold)
     
        {
            List<TerminalClientSide> result = new List<TerminalClientSide>();

            //台变
            for (int i = 0; i < 1; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.VoltageChanger;
                tempInfo.Address = (uint)(I_ChargeID * 10000000 + 1000000);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }

            //总表
            for (int i = 0; i < 1; i++)
            {
                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.HeadMeter;
                tempInfo.Address = (uint)(I_ChargeID * 10000000 );

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
                result.Add(curClientSide);
            }
 

            //分支
            for (int branchNumber = 0; branchNumber < 8; branchNumber++)
            {
                int boxSize = branchNumber == 7 ? 6 : 12;


                TerminalInfo tempInfo = new TerminalInfo();
                tempInfo.Type = MonitorLib.Enum.TerminalTypeEnum.BranchMeter;
                tempInfo.Address = (uint)(I_ChargeID * 10000000 + 1000000 + (branchNumber + 1) * 10000);

                TerminalClientSide curClientSide = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfo);
 
                result.Add(curClientSide);

                

                for (int boxNumber = 0; boxNumber < boxSize; boxNumber++)
                {
                    TerminalInfo tempInfoBox = new TerminalInfo();
                    tempInfoBox.Type = MonitorLib.Enum.TerminalTypeEnum.BoxMeter;
                    tempInfoBox.Address = (uint)(I_ChargeID * 10000000 + 1000000 + (branchNumber + 1) * 10000 + (boxNumber+1) * 10);


                    TerminalClientSide curClientSideBox = new TerminalClientSide(ConfigHelper.serverIP, ConfigHelper.serverPort, tempInfoBox);
 
                    result.Add(curClientSideBox);
                }



            }
 

            return result;

        }




        /// <summary>发送心跳包
        /// </summary>
        public int SendHeartBeatMessage()
        {
            int result = 0;

            foreach (TerminalClientSide curClient in ClientList)
            {
                if(curClient.IsSendHeartBeat())
                {
                    HeartbeatMessage curMessage = new HeartbeatMessage(curClient.TerminalInfo);
                    curClient.Send(curMessage.GetAllBytes());

                //    TraceHelper.TraceInfo("SendHeartBeat  " + curClient.TerminalInfo.Address  + " " + curClient.TerminalInfo.Type);

                    AddSendQueue();
                    curClient.RefreshHeartBeat();
                    result++;
                } 
            }
            return result;
        }

        // 发送上传数据包
        public int SendUploadMessage()
        {
            int result = 0;
            foreach (TerminalClientSide curClient in ClientList)
            {
                if (curClient.IsUploadDataBeat())
                {

                    byte[] sendBytes = null;
                    switch (curClient.TerminalInfo.Type)
                    {
                        case MonitorLib.Enum.TerminalTypeEnum.VoltageChanger:
                            {
                                VoltageChangerUploadDataMessage tempMessage = VoltageChangerUploadDataMessage.GetSampleMessage();
                                tempMessage.TerminalAddress = curClient.TerminalInfo.Address;

                                sendBytes = tempMessage.GetAllBytes();

                                break;
                            }
                        case MonitorLib.Enum.TerminalTypeEnum.HeadMeter:
                            {
                                HeadMeterUploadDataMessage tempMessage = HeadMeterUploadDataMessage.GetSampleMessage();
                                tempMessage.TerminalAddress = curClient.TerminalInfo.Address;
                                tempMessage.AveragePower = MessageCommon.GetPowerValue(curClient.AveragePower);
                                sendBytes = tempMessage.GetAllBytes();
                                break;
                            }
                        case MonitorLib.Enum.TerminalTypeEnum.BranchMeter:
                            {
                                BranchMeterUploadDataMessage tempMessage = BranchMeterUploadDataMessage.GetSampleMessage();
                                tempMessage.TerminalAddress = curClient.TerminalInfo.Address;
                                tempMessage.AveragePower = MessageCommon.GetPowerValue(curClient.AveragePower);
                                sendBytes = tempMessage.GetAllBytes();
                                break;
                            }
                        case MonitorLib.Enum.TerminalTypeEnum.BoxMeter:
                            {
                                BoxMeterUploadDataMessage tempMessage = BoxMeterUploadDataMessage.GetSampleMessage();
                                tempMessage.TerminalAddress = curClient.TerminalInfo.Address;
                                tempMessage.AveragePower = MessageCommon.GetPowerValue(curClient.AveragePower);
                                tempMessage.Meter0.Address = tempMessage.TerminalAddress + 1;
                                tempMessage.Meter1.Address = tempMessage.TerminalAddress + 2;
                                tempMessage.Meter2.Address = tempMessage.TerminalAddress + 3;
                                tempMessage.Meter3.Address = tempMessage.TerminalAddress + 4;
                                tempMessage.Meter4.Address = tempMessage.TerminalAddress + 5;
                                tempMessage.Meter5.Address = tempMessage.TerminalAddress + 6;
                                sendBytes = tempMessage.GetAllBytes();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    curClient.Send(sendBytes);
                    AddSendQueue();
                    curClient.RefreshUploadData();
                    result++;
                }
            }
            return result;
        }

        public void StopAll()
        {
            foreach (TerminalClientSide curClient in ClientList)
            {
                curClient.DisConnect();
            }
        }

        public void StopClient(TerminalClientSide I_Client)
        {
            I_Client.DisConnect();
        }

        public ClientStaticInfo GetClientStaticInfo()
        {
            ClientStaticInfo result = new ClientStaticInfo();

            result.VoltageChangerNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.VoltageChanger);
            result.VoltageChangerConnectNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.VoltageChanger && (p.ClientStatus == ClientStatusTypeEnum.Connected || p.ClientStatus == ClientStatusTypeEnum.Communicated));
            result.VoltageChangerNotConnectNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.VoltageChanger && (p.ClientStatus != ClientStatusTypeEnum.Connected && p.ClientStatus != ClientStatusTypeEnum.Communicated));

            result.HeadMeterNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.HeadMeter);
            result.HeadMeterConnectNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.HeadMeter && (p.ClientStatus == ClientStatusTypeEnum.Connected || p.ClientStatus == ClientStatusTypeEnum.Communicated));
            result.HeadMeterNotConnectNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.HeadMeter && (p.ClientStatus != ClientStatusTypeEnum.Connected && p.ClientStatus != ClientStatusTypeEnum.Communicated));

            result.BranchMeterNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.BranchMeter);
            result.BranchMeterConnectNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.BranchMeter && (p.ClientStatus == ClientStatusTypeEnum.Connected || p.ClientStatus == ClientStatusTypeEnum.Communicated));
            result.BranchMeterNotConnectNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.BranchMeter && (p.ClientStatus != ClientStatusTypeEnum.Connected && p.ClientStatus != ClientStatusTypeEnum.Communicated));

            result.BoxMeterNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.BoxMeter);
            result.BoxMeterConnectNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.BoxMeter && (p.ClientStatus == ClientStatusTypeEnum.Connected || p.ClientStatus == ClientStatusTypeEnum.Communicated));
            result.BoxMeterNotConnectNumber = ClientList.Count(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.BoxMeter && (p.ClientStatus != ClientStatusTypeEnum.Connected && p.ClientStatus != ClientStatusTypeEnum.Communicated));

            result.TotalNumber = ClientList.Count();
            result.TotalConnectNumber = ClientList.Count(p => p.ClientStatus == ClientStatusTypeEnum.Connected || p.ClientStatus == ClientStatusTypeEnum.Communicated);
            result.TotalNotConnectNumber = ClientList.Count(p => p.ClientStatus != ClientStatusTypeEnum.Connected && p.ClientStatus != ClientStatusTypeEnum.Communicated);

            result.HandlingCapacity = GetHndingCapacity();
            result.ErrorRate = GetErrorRate();

            return result;
        }

        public void AddSendQueue()
        {
            SendMessageQueue.Enqueue(DateTime.Now.Ticks);

            if(SendMessageQueue.Count > MaxMessageCount)
            {
                long tempValue = 0;
                SendMessageQueue.TryDequeue( out tempValue);
            }
        }

        public void AddReceiveQueue()
        {
            ReceiveMessageQueue.Enqueue(DateTime.Now.Ticks);

            if (ReceiveMessageQueue.Count > MaxMessageCount)
            {
                long tempValue = 0;
                ReceiveMessageQueue.TryDequeue(out tempValue);
            }
        }

 
        /// <summary>
        /// 统计吞吐量
        /// </summary>
        /// <returns></returns>
        private int GetHndingCapacity()
        {
            int result = 0;

            if(SendMessageQueue.Count > 0)
            {
                long firstTick = 0;
                SendMessageQueue.TryPeek(out firstTick);
                long lastTick = DateTime.Now.Ticks;

                int spanTime = (int)( (lastTick - firstTick) / 10000000);

                if(spanTime == 0)
                {
                    spanTime = 1;
                }

                result = (int)(SendMessageQueue.Count / spanTime);

            }
            return result;
        }

        private double GetErrorRate()
        {
            double result = 0;

            if(TotalMessageCount != 0)
            {
                result = (double)(TotalErrorCount * 100) / (double)TotalMessageCount;
            } 
            return result;
        }

    }
}

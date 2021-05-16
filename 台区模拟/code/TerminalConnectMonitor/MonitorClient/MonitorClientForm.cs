using MonitorLib;
using MonitorLib.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorClient
{


    public partial class Form_MonitorClient : Form
    {



        public Form_MonitorClient()
        {
            InitializeComponent();
        }

        private int clientIndex = 1;
        private List<MoniterTcpClient> clientList = new List<MoniterTcpClient>();

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            MoniterTcpClient curClient = new MoniterTcpClient(ConfigHelper.serverIP, ConfigHelper.serverPort, clientIndex);
            curClient.ConnectToServer();
            curClient.StartReceive();
            clientList.Add(curClient);
            clientIndex++; 
        }

        private void Btn_Connect1000_Click(object sender, EventArgs e)
        {
            for(int i = 0; i< 1000; i++ )
            {
                MoniterTcpClient curClient = new MoniterTcpClient(ConfigHelper.serverIP, ConfigHelper.serverPort, clientIndex);
                curClient.ConnectToServer();
                curClient.StartReceive();
                clientList.Add(curClient);
                clientIndex++;
                Thread.Sleep(20);
            }
        }

        private void Btn_Connect10000_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                MoniterTcpClient curClient = new MoniterTcpClient(ConfigHelper.serverIP, ConfigHelper.serverPort, clientIndex);
                curClient.ConnectToServer();
                curClient.StartReceive();
                clientList.Add(curClient);
                clientIndex++;
                Thread.Sleep(10);
            }
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            foreach(MoniterTcpClient curClient in clientList)
            {
                
                curClient.Send("2202<EOF>");
            }
        }

        private void Btn_Crc_Click(object sender, EventArgs e)
        {
            byte[] sourceByte = new byte[1];
            sourceByte[0] = 20;

           byte result =   CrcLib.CRC8Cal(sourceByte, 1);
        }

        private void Btn_MessageTest_Click(object sender, EventArgs e)
        {
           byte[] heartbeatBytes =   HeartbeatMessage.GetSampleMessage().GetAllBytes();
           byte[] timeSearchBytes = TimeSearchMessage.GetSampleMessage().GetAllBytes();
           byte[] voltageChangerUploadDataBytes = VoltageChangerUploadDataMessage.GetSampleMessage().GetAllBytes();
           byte[] headMeterUploadDataMessageBytes = HeadMeterUploadDataMessage.GetSampleMessage().GetAllBytes();
           byte[] branchMeterUploadDataMessageBytes = BranchMeterUploadDataMessage.GetSampleMessage().GetAllBytes();
           byte[] boxMeterUploadDataMessageBytes = BoxMeterUploadDataMessage.GetSampleMessage().GetAllBytes();

           byte[] statusSearchMessageBytes = StatusSearchCommandMessage.GetSampleMessage().GetAllBytes();

            byte[] statusSearchAnswerMessageBytes = StatusSearchAnswerMessage.GetSampleMessage().GetAllBytes();
            byte[] setHeartbeatAnswerMessageBytes = SetHeartbeatAnswerMessage.GetSampleMessage().GetAllBytes();
            byte[] setUploadAnswerMessageBytes = SetUploadAnswerMessage.GetSampleMessage().GetAllBytes();
            byte[] sSetChannelAnswerMessageBytes = SetChannelAnswerMessage.GetSampleMessage().GetAllBytes();
            

        }

        private void Btn_SendMessage_Click(object sender, EventArgs e)
        {
            foreach (MoniterTcpClient curClient in clientList)
            {
                byte[] heartbeatBytes = HeartbeatMessage.GetSampleMessage().GetAllBytes();

                curClient.Send(heartbeatBytes);
            }
        }

        private void Btn_Convert_Click(object sender, EventArgs e)
        {
            string[] tempArray = Tb_InputNum.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


            foreach(string curItem in tempArray)
            {
                Tb_OutputNum.Text = Tb_OutputNum.Text + Convert.ToInt32(curItem, 16) + ",";
            }
        }

        private void Form_MonitorClient_Load(object sender, EventArgs e)
        {

        }
    }
}

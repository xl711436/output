using MonitorLib;
using MonitorLib.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorServer
{
    public partial class Form_MonitorServer : Form
    {

        private MoniterTcpServer moniterServer;
        public Form_MonitorServer()
        {
            InitializeComponent();

            moniterServer = new MoniterTcpServer(ConfigHelper.serverIP, ConfigHelper.serverPort);

        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            moniterServer.StartListening();
        }

        private void Form_MonitorServer_Load(object sender, EventArgs e)
        {

        }

        private void Btn_GetClientCount_Click(object sender, EventArgs e)
        {
            MessageBox.Show(moniterServer.ClientList.Count.ToString());
        }

        

        private void Btn_StatusSearch_Click(object sender, EventArgs e)
        { 
 
            StatusSearchCommandMessage curMessage = new StatusSearchCommandMessage(0);
             
            foreach (Socket curClient in moniterServer.ClientList)
            {
                moniterServer.Send(curClient, curMessage.GetAllBytes());
            }
        }

        private void Btn_SetHeartbeat_Click(object sender, EventArgs e)
        {
            SetHeartbeatCommandMessage curMessage = new SetHeartbeatCommandMessage(0);
            curMessage.HeartbeatInterval = 80;


            foreach (Socket curClient in moniterServer.ClientList)
            {
                moniterServer.Send(curClient, curMessage.GetAllBytes());
            }
        }

        private void Btn_SetUpload_Click(object sender, EventArgs e)
        {
            SetUploadCommandMessage curMessage = new SetUploadCommandMessage(0);
            curMessage.SetInterval = 80;
            curMessage.SetDelay = 80;
            foreach (Socket curClient in moniterServer.ClientList)
            {
                moniterServer.Send(curClient, curMessage.GetAllBytes());
            }
        }

        private void Btn_SetChannel_Click(object sender, EventArgs e)
        {
            SetChannelCommandMessage curMessage = new SetChannelCommandMessage(0);


            curMessage.FirstIp = "192.168.1.1";
            curMessage.FirstPort = 10061;
            curMessage.BakIp = "192.168.1.2";
            curMessage.BakPort = 10061;

            foreach (Socket curClient in moniterServer.ClientList)
            {
                moniterServer.Send(curClient, curMessage.GetAllBytes());
            }

        }

        private void Btn_SetMeter_Click(object sender, EventArgs e)
        {

            MeterTestCommandMessage curMessage = new MeterTestCommandMessage(0);


            curMessage.MeterID = 0;
            curMessage.MeterDataFlag = 100; 
            foreach (Socket curClient in moniterServer.ClientList)
            {
                moniterServer.Send(curClient, curMessage.GetAllBytes());
            }
        }
    }
}

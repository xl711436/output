using log4net;
using MonitorLib;
using MonitorLib.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorClient
{
    public partial class Form_Main : Form
    {

        private ConnectManager connectManager;

       
        public Form_Main()
        {
            InitializeComponent();

            connectManager = new ConnectManager();
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {

            connectManager.GenerateClient();

            connectManager.OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  仿真启动开始");
            connectManager.ConnectServer();
            connectManager.OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  仿真启动成功");

            TraceHelper.TraceInfo("  仿真启动成功");


            //connectManager.OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  发送心跳报文开始");
            //connectManager.SendHeartBeatMessage();
            //connectManager.OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  发送心跳报文成功");

            Timer_Heartbeat.Enabled = true;
            Timer_RefreshStatus.Enabled = true;
            Timer_UploadData.Enabled = true;
            Timer_Show.Enabled = true;

        }





    
        private void Timer_RefreshStatus_Tick(object sender, EventArgs e)
        {
            ClientStaticInfo curStaticInfo =  connectManager.GetClientStaticInfo();


            Lb_VoltageChangerNumber.Text = curStaticInfo.VoltageChangerNumber.ToString() + "个";
            Lb_VoltageChangerConnectNumber.Text = curStaticInfo.VoltageChangerConnectNumber.ToString() + "个";
            Lb_VoltageChangerNotConnectNumber.Text = curStaticInfo.VoltageChangerNotConnectNumber.ToString() + "个";

            Lb_HeadMeterNumber.Text = curStaticInfo.HeadMeterNumber.ToString() + "个";
            Lb_HeadMeterConnectNumber.Text = curStaticInfo.HeadMeterConnectNumber.ToString() + "个";
            Lb_HeadMeterNotConnectNumber.Text = curStaticInfo.HeadMeterNotConnectNumber.ToString() + "个";

            Lb_BranchMeterNumber.Text = curStaticInfo.BranchMeterNumber.ToString() + "个";
            Lb_BranchMeterConnectNumber.Text = curStaticInfo.BranchMeterConnectNumber.ToString() + "个";
            Lb_BranchMeterNotConnectNumber.Text = curStaticInfo.BranchMeterNotConnectNumber.ToString() + "个";

            Lb_BoxMeterNumber.Text = curStaticInfo.BoxMeterNumber.ToString() + "个";
            Lb_BoxMeterConnectNumber.Text = curStaticInfo.BoxMeterConnectNumber.ToString() + "个";
            Lb_BoxMeterNotConnectNumber.Text = curStaticInfo.BoxMeterNotConnectNumber.ToString() + "个";

            Lb_TotalNumber.Text = curStaticInfo.TotalNumber.ToString() + "个";
            Lb_TotalConnectNumber.Text = curStaticInfo.TotalConnectNumber.ToString() + "个";
            Lb_TotalNotConnectNumber.Text = curStaticInfo.TotalNotConnectNumber.ToString() + "个";


            Lb_HandlingCapacity.Text = curStaticInfo.HandlingCapacity.ToString() + "/s";
            Lb_ErrorRate.Text = curStaticInfo.ErrorRate.ToString("0.00") + "%";
 

    }

        private void Timer_Heartbeat_Tick(object sender, EventArgs e)
        { 
            int sendresult  = connectManager.SendHeartBeatMessage(); 
            if(sendresult != 0)
            {
                connectManager.OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  发送心跳报文" + sendresult + "条");
            }
   
          //  Timer_Heartbeat.Interval = 60 * 1000;
        }

        private void Timer_UploadData_Tick(object sender, EventArgs e)
        { 
            int sendresult = connectManager.SendUploadMessage();
            if(sendresult != 0)
            {
                connectManager.OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  发送上传数据报文结束" + sendresult + "条");
            }

        //    Timer_UploadData.Interval =  60 * 1000; 
        }

        private void Btn_VoltageChangerClose_Click(object sender, EventArgs e)
        { 
            int closeCount = (int)Nup_VoltageChangerNumber.Value;
            if (closeCount == 0)
            {
                MessageBox.Show("掉线数量不能为0");
            }
            else
            {
                for (int i = 0; i < closeCount; i++)
                {
                    TerminalClientSide curClient = connectManager.ClientList.FirstOrDefault(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.VoltageChanger && (p.ClientStatus == ClientStatusTypeEnum.Connected || p.ClientStatus == ClientStatusTypeEnum.Communicated));
                    if (curClient != null)
                    {
                        connectManager.StopClient(curClient);
                    }
                }
            } 
        }

        private void Btn_HeadMeterClose_Click(object sender, EventArgs e)
        {


            int closeCount = (int)Nup_HeadMeterNumber.Value;
            if (closeCount == 0)
            {
                MessageBox.Show("掉线数量不能为0");
            }
            else
            {
                for (int i = 0; i < closeCount; i++)
                {
                    TerminalClientSide curClient = connectManager.ClientList.FirstOrDefault(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.HeadMeter && (p.ClientStatus == ClientStatusTypeEnum.Connected || p.ClientStatus == ClientStatusTypeEnum.Communicated));
                    if (curClient != null)
                    {
                        connectManager.StopClient(curClient);
                    }
                }
            }

        }

        private void Btn_BranchMeterClose_Click(object sender, EventArgs e)
        {


            int closeCount = (int)Nup_BranchMeterNumber.Value;
            if (closeCount == 0)
            {
                MessageBox.Show("掉线数量不能为0");
            }
            else
            {
                for (int i = 0; i < closeCount; i++)
                {
                    TerminalClientSide curClient = connectManager.ClientList.FirstOrDefault(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.BranchMeter && (p.ClientStatus == ClientStatusTypeEnum.Connected || p.ClientStatus == ClientStatusTypeEnum.Communicated));
                    if (curClient != null)
                    {
                        connectManager.StopClient(curClient);
                    }
                }
            }


        }

        private void Btn_BoxMeterClose_Click(object sender, EventArgs e)
        {
            int closeCount = (int)Nup_BoxMeterNumber.Value;
            if (closeCount == 0)
            {
                MessageBox.Show("掉线数量不能为0");
            }
            else
            {
                for (int i = 0; i < closeCount; i++)
                {
                    TerminalClientSide curClient = connectManager.ClientList.FirstOrDefault(p => p.TerminalInfo.Type == MonitorLib.Enum.TerminalTypeEnum.BoxMeter && (p.ClientStatus == ClientStatusTypeEnum.Connected || p.ClientStatus == ClientStatusTypeEnum.Communicated));
                    if (curClient != null)
                    {
                        connectManager.StopClient(curClient);
                    }
                } 
            } 
        }

        private void Timer_Show_Tick(object sender, EventArgs e)
        {

            connectManager.CheckReceiveNumber();
            while (connectManager.OutPutLogInfo.Count > 0)
            {
                string curLine = "";
                connectManager.OutPutLogInfo.TryDequeue(out curLine);
                Tb_Log.Text = Tb_Log.Text + curLine + Environment.NewLine; 
            }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            connectManager.StopAll();
            connectManager.OutPutLogInfo.Enqueue(DateTime.Now.ToString() + "  仿真停止");

            TraceHelper.TraceInfo( "  仿真停止");
        }

        private void Btn_TimeSearch_Click(object sender, EventArgs e)
        {
            foreach (TerminalClientSide curClient in connectManager.ClientList)
            {
                TimeSearchMessage timeSearchMessage = new TimeSearchMessage(curClient.TerminalInfo);
                if (curClient != null)
                {
                    curClient.Send(timeSearchMessage.GetAllBytes());
                }
            }

            TraceHelper.TraceInfo( "  发送较时包,终端数： " + connectManager.ClientList.Count());

        }

        private void Btn_ReConnect_Click(object sender, EventArgs e)
        {
            int reConnectCount = 0;
            foreach (TerminalClientSide curClient in connectManager.ClientList)
            {
                reConnectCount = reConnectCount + curClient.ReConnect();

            }
            TraceHelper.TraceInfo( "  重连数量： " + reConnectCount);

        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
      
            Nud_HeadLose.Value = ConfigHelper.headWarn;
            Nud_BranchLose.Value = ConfigHelper.branchWarn;
            Nud_BoxLose.Value = ConfigHelper.boxWarn;
            Nud_LoseThreshold.Value = ConfigHelper.thresholdValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigHelper.headWarn = Convert.ToInt32 ( Nud_HeadLose.Value) ;
            ConfigHelper.branchWarn = Convert.ToInt32(Nud_BranchLose.Value);
            ConfigHelper.boxWarn = Convert.ToInt32(Nud_BoxLose.Value);
            ConfigHelper.thresholdValue = Convert.ToInt32(Nud_LoseThreshold.Value);

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["headWarn"].Value = ConfigHelper.headWarn.ToString();
            config.AppSettings.Settings["branchWarn"].Value = ConfigHelper.branchWarn.ToString();
            config.AppSettings.Settings["boxWarn"].Value = ConfigHelper.boxWarn.ToString();
            config.AppSettings.Settings["thresholdValue"].Value = ConfigHelper.thresholdValue.ToString();

            // 保存修改
            config.Save(ConfigurationSaveMode.Modified);

            // 强制重新载入配置文件的连接配置节
            ConfigurationManager.RefreshSection("appSettings");
             
            connectManager.RefreshPowerAndLineLoseRate();
            TraceHelper.TraceInfo("重新生成线损信息 ");

        }

        private void Cb_AutoReConnect_CheckedChanged(object sender, EventArgs e)
        {
            Time_AutoReConnect.Enabled = Cb_AutoReConnect.Checked;
        }

        private void Time_AutoReConnect_Tick(object sender, EventArgs e)
        {
            int reConnectCount = 0;
            foreach (TerminalClientSide curClient in connectManager.ClientList)
            {
                reConnectCount = reConnectCount + curClient.ReConnect();

            }
            TraceHelper.TraceInfo("  自动重连数量： " + reConnectCount);
        }
    }
}

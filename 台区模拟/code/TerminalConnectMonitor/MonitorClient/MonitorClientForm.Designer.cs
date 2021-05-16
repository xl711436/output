namespace MonitorClient
{
    partial class Form_MonitorClient
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Connect = new System.Windows.Forms.Button();
            this.Btn_Connect1000 = new System.Windows.Forms.Button();
            this.Btn_Connect10000 = new System.Windows.Forms.Button();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.Btn_Crc = new System.Windows.Forms.Button();
            this.Btn_MessageTest = new System.Windows.Forms.Button();
            this.Btn_SendMessage = new System.Windows.Forms.Button();
            this.Tb_InputNum = new System.Windows.Forms.TextBox();
            this.Tb_OutputNum = new System.Windows.Forms.TextBox();
            this.Btn_Convert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Connect
            // 
            this.Btn_Connect.Location = new System.Drawing.Point(111, 25);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(113, 27);
            this.Btn_Connect.TabIndex = 0;
            this.Btn_Connect.Text = "Connect";
            this.Btn_Connect.UseVisualStyleBackColor = true;
            this.Btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // Btn_Connect1000
            // 
            this.Btn_Connect1000.Location = new System.Drawing.Point(111, 77);
            this.Btn_Connect1000.Name = "Btn_Connect1000";
            this.Btn_Connect1000.Size = new System.Drawing.Size(113, 27);
            this.Btn_Connect1000.TabIndex = 1;
            this.Btn_Connect1000.Text = "Connect1000";
            this.Btn_Connect1000.UseVisualStyleBackColor = true;
            this.Btn_Connect1000.Click += new System.EventHandler(this.Btn_Connect1000_Click);
            // 
            // Btn_Connect10000
            // 
            this.Btn_Connect10000.Location = new System.Drawing.Point(111, 133);
            this.Btn_Connect10000.Name = "Btn_Connect10000";
            this.Btn_Connect10000.Size = new System.Drawing.Size(113, 27);
            this.Btn_Connect10000.TabIndex = 2;
            this.Btn_Connect10000.Text = "Connect10000";
            this.Btn_Connect10000.UseVisualStyleBackColor = true;
            this.Btn_Connect10000.Click += new System.EventHandler(this.Btn_Connect10000_Click);
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(377, 25);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(113, 27);
            this.Btn_Send.TabIndex = 3;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // Btn_Crc
            // 
            this.Btn_Crc.Location = new System.Drawing.Point(377, 133);
            this.Btn_Crc.Name = "Btn_Crc";
            this.Btn_Crc.Size = new System.Drawing.Size(113, 27);
            this.Btn_Crc.TabIndex = 4;
            this.Btn_Crc.Text = "crc test";
            this.Btn_Crc.UseVisualStyleBackColor = true;
            this.Btn_Crc.Click += new System.EventHandler(this.Btn_Crc_Click);
            // 
            // Btn_MessageTest
            // 
            this.Btn_MessageTest.Location = new System.Drawing.Point(531, 133);
            this.Btn_MessageTest.Name = "Btn_MessageTest";
            this.Btn_MessageTest.Size = new System.Drawing.Size(113, 27);
            this.Btn_MessageTest.TabIndex = 5;
            this.Btn_MessageTest.Text = "MessageTest";
            this.Btn_MessageTest.UseVisualStyleBackColor = true;
            this.Btn_MessageTest.Click += new System.EventHandler(this.Btn_MessageTest_Click);
            // 
            // Btn_SendMessage
            // 
            this.Btn_SendMessage.Location = new System.Drawing.Point(377, 77);
            this.Btn_SendMessage.Name = "Btn_SendMessage";
            this.Btn_SendMessage.Size = new System.Drawing.Size(113, 27);
            this.Btn_SendMessage.TabIndex = 6;
            this.Btn_SendMessage.Text = "SendMessage";
            this.Btn_SendMessage.UseVisualStyleBackColor = true;
            this.Btn_SendMessage.Click += new System.EventHandler(this.Btn_SendMessage_Click);
            // 
            // Tb_InputNum
            // 
            this.Tb_InputNum.Location = new System.Drawing.Point(51, 239);
            this.Tb_InputNum.Multiline = true;
            this.Tb_InputNum.Name = "Tb_InputNum";
            this.Tb_InputNum.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Tb_InputNum.Size = new System.Drawing.Size(560, 76);
            this.Tb_InputNum.TabIndex = 53;
            // 
            // Tb_OutputNum
            // 
            this.Tb_OutputNum.Location = new System.Drawing.Point(51, 362);
            this.Tb_OutputNum.Multiline = true;
            this.Tb_OutputNum.Name = "Tb_OutputNum";
            this.Tb_OutputNum.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Tb_OutputNum.Size = new System.Drawing.Size(560, 76);
            this.Tb_OutputNum.TabIndex = 54;
            // 
            // Btn_Convert
            // 
            this.Btn_Convert.Location = new System.Drawing.Point(294, 329);
            this.Btn_Convert.Name = "Btn_Convert";
            this.Btn_Convert.Size = new System.Drawing.Size(113, 27);
            this.Btn_Convert.TabIndex = 55;
            this.Btn_Convert.Text = "MessageTest";
            this.Btn_Convert.UseVisualStyleBackColor = true;
            this.Btn_Convert.Click += new System.EventHandler(this.Btn_Convert_Click);
            // 
            // Form_MonitorClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_Convert);
            this.Controls.Add(this.Tb_OutputNum);
            this.Controls.Add(this.Tb_InputNum);
            this.Controls.Add(this.Btn_SendMessage);
            this.Controls.Add(this.Btn_MessageTest);
            this.Controls.Add(this.Btn_Crc);
            this.Controls.Add(this.Btn_Send);
            this.Controls.Add(this.Btn_Connect10000);
            this.Controls.Add(this.Btn_Connect1000);
            this.Controls.Add(this.Btn_Connect);
            this.Name = "Form_MonitorClient";
            this.Text = "电力终端仿真工具";
            this.Load += new System.EventHandler(this.Form_MonitorClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Connect;
        private System.Windows.Forms.Button Btn_Connect1000;
        private System.Windows.Forms.Button Btn_Connect10000;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.Button Btn_Crc;
        private System.Windows.Forms.Button Btn_MessageTest;
        private System.Windows.Forms.Button Btn_SendMessage;
        private System.Windows.Forms.TextBox Tb_InputNum;
        private System.Windows.Forms.TextBox Tb_OutputNum;
        private System.Windows.Forms.Button Btn_Convert;
    }
}


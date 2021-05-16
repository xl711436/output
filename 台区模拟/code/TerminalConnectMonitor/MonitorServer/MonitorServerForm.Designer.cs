namespace MonitorServer
{
    partial class Form_MonitorServer
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
            this.Btn_Start = new System.Windows.Forms.Button();
            this.Btn_GetClientCount = new System.Windows.Forms.Button();
            this.Btn_StatusSearch = new System.Windows.Forms.Button();
            this.Btn_SetHeartbeat = new System.Windows.Forms.Button();
            this.Btn_SetUpload = new System.Windows.Forms.Button();
            this.Btn_SetChannel = new System.Windows.Forms.Button();
            this.Btn_SetMeter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Start
            // 
            this.Btn_Start.Location = new System.Drawing.Point(95, 44);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(124, 48);
            this.Btn_Start.TabIndex = 0;
            this.Btn_Start.Text = "start";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // Btn_GetClientCount
            // 
            this.Btn_GetClientCount.Location = new System.Drawing.Point(615, 44);
            this.Btn_GetClientCount.Name = "Btn_GetClientCount";
            this.Btn_GetClientCount.Size = new System.Drawing.Size(124, 48);
            this.Btn_GetClientCount.TabIndex = 2;
            this.Btn_GetClientCount.Text = "clientCount";
            this.Btn_GetClientCount.UseVisualStyleBackColor = true;
            this.Btn_GetClientCount.Click += new System.EventHandler(this.Btn_GetClientCount_Click);
            // 
            // Btn_StatusSearch
            // 
            this.Btn_StatusSearch.Location = new System.Drawing.Point(185, 359);
            this.Btn_StatusSearch.Name = "Btn_StatusSearch";
            this.Btn_StatusSearch.Size = new System.Drawing.Size(124, 48);
            this.Btn_StatusSearch.TabIndex = 3;
            this.Btn_StatusSearch.Text = "状态查询";
            this.Btn_StatusSearch.UseVisualStyleBackColor = true;
            this.Btn_StatusSearch.Click += new System.EventHandler(this.Btn_StatusSearch_Click);
            // 
            // Btn_SetHeartbeat
            // 
            this.Btn_SetHeartbeat.Location = new System.Drawing.Point(34, 359);
            this.Btn_SetHeartbeat.Name = "Btn_SetHeartbeat";
            this.Btn_SetHeartbeat.Size = new System.Drawing.Size(124, 48);
            this.Btn_SetHeartbeat.TabIndex = 4;
            this.Btn_SetHeartbeat.Text = "设置心跳周期";
            this.Btn_SetHeartbeat.UseVisualStyleBackColor = true;
            this.Btn_SetHeartbeat.Click += new System.EventHandler(this.Btn_SetHeartbeat_Click);
            // 
            // Btn_SetUpload
            // 
            this.Btn_SetUpload.Location = new System.Drawing.Point(338, 359);
            this.Btn_SetUpload.Name = "Btn_SetUpload";
            this.Btn_SetUpload.Size = new System.Drawing.Size(124, 48);
            this.Btn_SetUpload.TabIndex = 5;
            this.Btn_SetUpload.Text = "设置采集周期";
            this.Btn_SetUpload.UseVisualStyleBackColor = true;
            this.Btn_SetUpload.Click += new System.EventHandler(this.Btn_SetUpload_Click);
            // 
            // Btn_SetChannel
            // 
            this.Btn_SetChannel.Location = new System.Drawing.Point(480, 359);
            this.Btn_SetChannel.Name = "Btn_SetChannel";
            this.Btn_SetChannel.Size = new System.Drawing.Size(124, 48);
            this.Btn_SetChannel.TabIndex = 6;
            this.Btn_SetChannel.Text = "设置通讯信道";
            this.Btn_SetChannel.UseVisualStyleBackColor = true;
            this.Btn_SetChannel.Click += new System.EventHandler(this.Btn_SetChannel_Click);
            // 
            // Btn_SetMeter
            // 
            this.Btn_SetMeter.Location = new System.Drawing.Point(629, 359);
            this.Btn_SetMeter.Name = "Btn_SetMeter";
            this.Btn_SetMeter.Size = new System.Drawing.Size(124, 48);
            this.Btn_SetMeter.TabIndex = 7;
            this.Btn_SetMeter.Text = "电表召测";
            this.Btn_SetMeter.UseVisualStyleBackColor = true;
            this.Btn_SetMeter.Click += new System.EventHandler(this.Btn_SetMeter_Click);
            // 
            // Form_MonitorServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_SetMeter);
            this.Controls.Add(this.Btn_SetChannel);
            this.Controls.Add(this.Btn_SetUpload);
            this.Controls.Add(this.Btn_SetHeartbeat);
            this.Controls.Add(this.Btn_StatusSearch);
            this.Controls.Add(this.Btn_GetClientCount);
            this.Controls.Add(this.Btn_Start);
            this.Name = "Form_MonitorServer";
            this.Text = "MonitorServer";
            this.Load += new System.EventHandler(this.Form_MonitorServer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.Button Btn_GetClientCount;
        private System.Windows.Forms.Button Btn_StatusSearch;
        private System.Windows.Forms.Button Btn_SetHeartbeat;
        private System.Windows.Forms.Button Btn_SetUpload;
        private System.Windows.Forms.Button Btn_SetChannel;
        private System.Windows.Forms.Button Btn_SetMeter;
    }
}


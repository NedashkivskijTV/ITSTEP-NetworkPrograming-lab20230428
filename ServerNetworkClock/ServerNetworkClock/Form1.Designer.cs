namespace ServerNetworkClock
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbNetworkClock = new System.Windows.Forms.Label();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.tbServerInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbNetworkClock
            // 
            this.lbNetworkClock.AutoSize = true;
            this.lbNetworkClock.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbNetworkClock.Location = new System.Drawing.Point(194, 20);
            this.lbNetworkClock.Name = "lbNetworkClock";
            this.lbNetworkClock.Size = new System.Drawing.Size(112, 32);
            this.lbNetworkClock.TabIndex = 0;
            this.lbNetworkClock.Text = "00:00:00";
            this.lbNetworkClock.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(194, 300);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(75, 23);
            this.btnStartServer.TabIndex = 1;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // tbServerInfo
            // 
            this.tbServerInfo.Location = new System.Drawing.Point(12, 71);
            this.tbServerInfo.Multiline = true;
            this.tbServerInfo.Name = "tbServerInfo";
            this.tbServerInfo.PlaceholderText = "Server Information";
            this.tbServerInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbServerInfo.Size = new System.Drawing.Size(464, 223);
            this.tbServerInfo.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 335);
            this.Controls.Add(this.tbServerInfo);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.lbNetworkClock);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Label lbNetworkClock;
        private Button btnStartServer;
        private TextBox tbServerInfo;
    }
}
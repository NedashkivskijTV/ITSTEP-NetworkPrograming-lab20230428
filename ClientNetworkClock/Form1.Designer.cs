namespace ClientNetworkClock
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
            this.lbNetworkClock = new System.Windows.Forms.Label();
            this.btnGetTime = new System.Windows.Forms.Button();
            this.tbClientQuery = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbNetworkClock
            // 
            this.lbNetworkClock.AutoSize = true;
            this.lbNetworkClock.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbNetworkClock.Location = new System.Drawing.Point(73, 23);
            this.lbNetworkClock.Name = "lbNetworkClock";
            this.lbNetworkClock.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbNetworkClock.Size = new System.Drawing.Size(199, 37);
            this.lbNetworkClock.TabIndex = 0;
            this.lbNetworkClock.Text = "NetworkClock";
            this.lbNetworkClock.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnGetTime
            // 
            this.btnGetTime.Location = new System.Drawing.Point(106, 199);
            this.btnGetTime.Name = "btnGetTime";
            this.btnGetTime.Size = new System.Drawing.Size(75, 23);
            this.btnGetTime.TabIndex = 1;
            this.btnGetTime.Text = "Get Time";
            this.btnGetTime.UseVisualStyleBackColor = true;
            this.btnGetTime.Click += new System.EventHandler(this.btnGetTime_Click);
            // 
            // tbClientQuery
            // 
            this.tbClientQuery.Location = new System.Drawing.Point(16, 114);
            this.tbClientQuery.Multiline = true;
            this.tbClientQuery.Name = "tbClientQuery";
            this.tbClientQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbClientQuery.Size = new System.Drawing.Size(256, 62);
            this.tbClientQuery.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 234);
            this.Controls.Add(this.tbClientQuery);
            this.Controls.Add(this.btnGetTime);
            this.Controls.Add(this.lbNetworkClock);
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbNetworkClock;
        private Button btnGetTime;
        private TextBox tbClientQuery;
    }
}
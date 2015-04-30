namespace MissionPlanner.GCSViews.ConfigurationView
{
    partial class ConfigSmartAir
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigSmartAir));
            this.numIntervall = new System.Windows.Forms.NumericUpDown();
            this.myLabel5 = new MissionPlanner.Controls.MyLabel();
            this.myLabel4 = new MissionPlanner.Controls.MyLabel();
            this.txtJSPassword = new System.Windows.Forms.TextBox();
            this.myLabel3 = new MissionPlanner.Controls.MyLabel();
            this.myLabel2 = new MissionPlanner.Controls.MyLabel();
            this.btnOpenExternalFlightData = new MissionPlanner.Controls.MyButton();
            this.txtJSUser = new System.Windows.Forms.TextBox();
            this.txtJSUrl = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnJSStop = new MissionPlanner.Controls.MyButton();
            this.btnJSStart = new MissionPlanner.Controls.MyButton();
            this.btnStopWS = new MissionPlanner.Controls.MyButton();
            this.txtWSUrl = new System.Windows.Forms.TextBox();
            this.btnStartWS = new MissionPlanner.Controls.MyButton();
            this.myLabel1 = new MissionPlanner.Controls.MyLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numIntervall)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // numIntervall
            // 
            resources.ApplyResources(this.numIntervall, "numIntervall");
            this.numIntervall.Name = "numIntervall";
            this.numIntervall.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numIntervall.ValueChanged += new System.EventHandler(this.numIntervall_ValueChanged);
            // 
            // myLabel5
            // 
            resources.ApplyResources(this.myLabel5, "myLabel5");
            this.myLabel5.Name = "myLabel5";
            this.myLabel5.resize = false;
            // 
            // myLabel4
            // 
            resources.ApplyResources(this.myLabel4, "myLabel4");
            this.myLabel4.Name = "myLabel4";
            this.myLabel4.resize = false;
            // 
            // txtJSPassword
            // 
            this.txtJSPassword.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtJSPassword, "txtJSPassword");
            this.txtJSPassword.Name = "txtJSPassword";
            this.txtJSPassword.TextChanged += new System.EventHandler(this.txtJSPassword_TextChanged);
            // 
            // myLabel3
            // 
            resources.ApplyResources(this.myLabel3, "myLabel3");
            this.myLabel3.Name = "myLabel3";
            this.myLabel3.resize = false;
            // 
            // myLabel2
            // 
            resources.ApplyResources(this.myLabel2, "myLabel2");
            this.myLabel2.Name = "myLabel2";
            this.myLabel2.resize = false;
            // 
            // btnOpenExternalFlightData
            // 
            resources.ApplyResources(this.btnOpenExternalFlightData, "btnOpenExternalFlightData");
            this.btnOpenExternalFlightData.Name = "btnOpenExternalFlightData";
            this.btnOpenExternalFlightData.UseVisualStyleBackColor = true;
            this.btnOpenExternalFlightData.Click += new System.EventHandler(this.btnOpenExternalFlightData_Click);
            // 
            // txtJSUser
            // 
            this.txtJSUser.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtJSUser, "txtJSUser");
            this.txtJSUser.Name = "txtJSUser";
            this.txtJSUser.TextChanged += new System.EventHandler(this.txtJSUser_TextChanged);
            // 
            // txtJSUrl
            // 
            this.txtJSUrl.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtJSUrl, "txtJSUrl");
            this.txtJSUrl.Name = "txtJSUrl";
            this.txtJSUrl.TextChanged += new System.EventHandler(this.txtJSUrl_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numIntervall);
            this.groupBox2.Controls.Add(this.myLabel5);
            this.groupBox2.Controls.Add(this.myLabel4);
            this.groupBox2.Controls.Add(this.txtJSPassword);
            this.groupBox2.Controls.Add(this.myLabel3);
            this.groupBox2.Controls.Add(this.myLabel2);
            this.groupBox2.Controls.Add(this.txtJSUser);
            this.groupBox2.Controls.Add(this.btnJSStop);
            this.groupBox2.Controls.Add(this.txtJSUrl);
            this.groupBox2.Controls.Add(this.btnJSStart);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnJSStop
            // 
            resources.ApplyResources(this.btnJSStop, "btnJSStop");
            this.btnJSStop.Name = "btnJSStop";
            this.btnJSStop.UseVisualStyleBackColor = true;
            this.btnJSStop.Click += new System.EventHandler(this.btnJSStop_Click);
            // 
            // btnJSStart
            // 
            resources.ApplyResources(this.btnJSStart, "btnJSStart");
            this.btnJSStart.Name = "btnJSStart";
            this.btnJSStart.UseVisualStyleBackColor = true;
            this.btnJSStart.Click += new System.EventHandler(this.btnJSStart_Click);
            // 
            // btnStopWS
            // 
            resources.ApplyResources(this.btnStopWS, "btnStopWS");
            this.btnStopWS.Name = "btnStopWS";
            this.btnStopWS.UseVisualStyleBackColor = true;
            this.btnStopWS.Click += new System.EventHandler(this.btnStopWS_Click);
            // 
            // txtWSUrl
            // 
            this.txtWSUrl.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtWSUrl, "txtWSUrl");
            this.txtWSUrl.Name = "txtWSUrl";
            this.txtWSUrl.TextChanged += new System.EventHandler(this.txtWSUrl_TextChanged);
            // 
            // btnStartWS
            // 
            resources.ApplyResources(this.btnStartWS, "btnStartWS");
            this.btnStartWS.Name = "btnStartWS";
            this.btnStartWS.UseVisualStyleBackColor = true;
            this.btnStartWS.Click += new System.EventHandler(this.btnStartWS_Click);
            // 
            // myLabel1
            // 
            resources.ApplyResources(this.myLabel1, "myLabel1");
            this.myLabel1.Name = "myLabel1";
            this.myLabel1.resize = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.myLabel1);
            this.groupBox1.Controls.Add(this.btnStopWS);
            this.groupBox1.Controls.Add(this.txtWSUrl);
            this.groupBox1.Controls.Add(this.btnStartWS);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ConfigSmartAir
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOpenExternalFlightData);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConfigSmartAir";
            this.Load += new System.EventHandler(this.ConfigSmartAir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numIntervall)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numIntervall;
        private Controls.MyLabel myLabel5;
        private Controls.MyLabel myLabel4;
        private System.Windows.Forms.TextBox txtJSPassword;
        private Controls.MyLabel myLabel3;
        private Controls.MyLabel myLabel2;
        private Controls.MyButton btnOpenExternalFlightData;
        private System.Windows.Forms.TextBox txtJSUser;
        private System.Windows.Forms.TextBox txtJSUrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.MyButton btnJSStop;
        private Controls.MyButton btnJSStart;
        private Controls.MyButton btnStopWS;
        private System.Windows.Forms.TextBox txtWSUrl;
        private Controls.MyButton btnStartWS;
        private Controls.MyLabel myLabel1;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}

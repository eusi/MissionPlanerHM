namespace MissionPlanner.SmartAir.Logging
{
    partial class SmartAirLogger
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
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.lblLogFilePath = new MissionPlanner.Controls.MyLabel();
            this.gridLogging = new System.Windows.Forms.DataGridView();
            this.cLogStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clogMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridLogging)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Location = new System.Drawing.Point(268, 25);
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.Size = new System.Drawing.Size(100, 20);
            this.txtLogFilePath.TabIndex = 0;
            // 
            // lblLogFilePath
            // 
            this.lblLogFilePath.Location = new System.Drawing.Point(109, 25);
            this.lblLogFilePath.Name = "lblLogFilePath";
            this.lblLogFilePath.resize = false;
            this.lblLogFilePath.Size = new System.Drawing.Size(75, 23);
            this.lblLogFilePath.TabIndex = 1;
            this.lblLogFilePath.Text = "Log file path";
            // 
            // gridLogging
            // 
            this.gridLogging.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLogging.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cLogStatus,
            this.clogMessage});
            this.gridLogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLogging.Location = new System.Drawing.Point(0, 0);
            this.gridLogging.Name = "gridLogging";
            this.gridLogging.Size = new System.Drawing.Size(585, 285);
            this.gridLogging.TabIndex = 2;
            // 
            // cLogStatus
            // 
            this.cLogStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cLogStatus.HeaderText = "Log status";
            this.cLogStatus.Name = "cLogStatus";
            // 
            // clogMessage
            // 
            this.clogMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clogMessage.HeaderText = "Message";
            this.clogMessage.Name = "clogMessage";
            // 
            // SmartAirLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridLogging);
            this.Controls.Add(this.lblLogFilePath);
            this.Controls.Add(this.txtLogFilePath);
            this.Name = "SmartAirLogger";
            this.Size = new System.Drawing.Size(585, 285);
            ((System.ComponentModel.ISupportInitialize)(this.gridLogging)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogFilePath;
        private Controls.MyLabel lblLogFilePath;
        private System.Windows.Forms.DataGridView gridLogging;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLogStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn clogMessage;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}

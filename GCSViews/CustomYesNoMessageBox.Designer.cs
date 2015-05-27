namespace MissionPlanner.GCSViews
{
    partial class CustomYesNoMessageBox
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomYesNoMessageBox));
            this.btnYes = new MissionPlanner.Controls.MyButton();
            this.myBtnNo = new MissionPlanner.Controls.MyButton();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Location = new System.Drawing.Point(16, 76);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            // 
            // myBtnNo
            // 
            this.myBtnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.myBtnNo.Location = new System.Drawing.Point(170, 76);
            this.myBtnNo.Name = "myBtnNo";
            this.myBtnNo.Size = new System.Drawing.Size(75, 23);
            this.myBtnNo.TabIndex = 1;
            this.myBtnNo.Text = "No";
            this.myBtnNo.UseVisualStyleBackColor = true;
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new System.Drawing.Point(17, 105);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(107, 17);
            this.chkRemember.TabIndex = 2;
            this.chkRemember.Text = "Do not ask again";
            this.chkRemember.UseVisualStyleBackColor = true;
            this.chkRemember.CheckedChanged += new System.EventHandler(this.chkRemember_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 68);
            this.label1.TabIndex = 4;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // CustomYesNoMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 130);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkRemember);
            this.Controls.Add(this.myBtnNo);
            this.Controls.Add(this.btnYes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomYesNoMessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MyButton btnYes;
        private Controls.MyButton myBtnNo;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Label label1;
    }
}
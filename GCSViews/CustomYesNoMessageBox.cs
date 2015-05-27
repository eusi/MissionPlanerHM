
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MissionPlanner.GCSViews
{
    public partial class CustomYesNoMessageBox : Form
    {
        public CustomYesNoMessageBox(String message, string formname)
        {
            InitializeComponent();

            this.label1.Text = message;
            this.Text = formname;
            this.TopMost = true;
        }

        bool askNotAgain = false;

        public bool DoNotAskAgain
        {
            get { return askNotAgain; }
            set { askNotAgain = value; }
        }

        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRemember.Checked)
            {
                askNotAgain = true;
                this.myBtnNo.Enabled = false;
            }

            else
            {
                askNotAgain = false;
                this.myBtnNo.Enabled = true;
            }
        }


    }
}

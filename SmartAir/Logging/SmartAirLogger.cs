using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net.Core;

namespace MissionPlanner.SmartAir.Logging
{
    
    public partial class SmartAirLogger : UserControl
    {
        private BindingList<LoggingEvent> _logEntries = new BindingList<LoggingEvent>();

        public BindingList<LoggingEvent> LogEntries
        {
            get { return _logEntries; }
            set { _logEntries = value; }
        }
        public SmartAirLogger()
        {

            InitializeComponent();
            this.gridLogging.DataSource = LogEntries;
        }
    }
}

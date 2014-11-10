using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeServerInterface
{
    /// <summary>
    /// This class includes the current server information.
    /// </summary>
    public class ServerInfo
    {
        private string _serverMessage;

        /// <summary>
        /// A unique message stored on the server that proves the team has correctly downloaded the server information. This information must be displayed as part of interoperability.
        /// </summary>
        public string ServerMessage
        {
            get { return _serverMessage; }
            set { _serverMessage = value; }
        }

        private string _messageTimeStamp;

        /// <summary>
        /// The time the unique message was created. This information must be displayed as part of interoperability.
        /// </summary>
        public string MessageTimeStamp
        {
            get { return _messageTimeStamp; }
            set { _messageTimeStamp = value; }
        }

        private string _serverTime;

        /// <summary>
        /// The current time on the server. This information must be displayed as part of interoperability.
        /// </summary>
        public string ServerTime
        {
            get { return _serverTime; }
            set { _serverTime = value; }
        }

    }
}

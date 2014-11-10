using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

namespace JudgeServerInterface
{
    public class JsonServerInfo
    {
        public String message;
        public String message_timestamp;
    }

    public class JsonServerMessage
    {
        public JsonServerInfo server_info;
        public string server_time;



    }

    public static class JsonDeserializer
    {
        private static T Deserialize<T>(String json) where T : new()
        {
            T obj = new T();

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Deserialize<T>(json);
        }

        public static ServerInfo GetServerInfo(String json) {
            ServerInfo si = new ServerInfo();

            JsonServerMessage jsm = JsonDeserializer.Deserialize<JsonServerMessage>(json);

            si.ServerMessage = jsm.server_info.message;
            si.MessageTimeStamp = jsm.server_info.message_timestamp;
            si.ServerTime = jsm.server_time;

            return si;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;

namespace JudgeServerInterface
{
    public class JudgeServer : IJudgeServer
    {
        private String username;
        private String password;

        private String server = "http://smartair-JudgingServer:8080";

        enum ServerLinks {
            Login,
            ServerInfo
        }

        Dictionary<ServerLinks, String> serverLinks = new Dictionary<ServerLinks, string>() {
            {ServerLinks.Login, "/api/login"},
            {ServerLinks.ServerInfo, "/api/interop/server_info"}
        };

        private HttpClient httpClient;

        private HttpResponseMessage Request(ServerLinks app, List<KeyValuePair<String, String>> post_parameters=null) {
            String address = serverLinks[app];

            HttpResponseMessage response;

            if (post_parameters == null) response = httpClient.GetAsync(address).Result;
            else { 
                HttpContent httpContent = new FormUrlEncodedContent(post_parameters);
                response = httpClient.PostAsync(address, httpContent).Result;
            }
            return response;
        }

        public JudgeServer() {
            CookieContainer cookieContainer = new CookieContainer();
            HttpClientHandler httpClientHandler = new HttpClientHandler() { CookieContainer = cookieContainer };
            httpClient = new HttpClient(httpClientHandler) { BaseAddress = new System.Uri(server) };
        }

        public void Connect(string userName, string password)
        {
            this.username = userName;
            this.password = password;

            List<KeyValuePair<string, string>> post_parameters = new List<KeyValuePair<string, string>>();
            post_parameters.Add(new KeyValuePair<string, string>("username", this.username));
            post_parameters.Add(new KeyValuePair<string, string>("password", this.password));

            this.Request(ServerLinks.Login, post_parameters);
        }

        public ServerInfo GetServerInfo()
        {
            ServerInfo si = new ServerInfo();

            HttpResponseMessage response = this.Request(ServerLinks.ServerInfo);

            if (response.Content != null) si = JsonDeserializer.GetServerInfo(response.Content.ReadAsStringAsync().Result);;

            return si;
        }

        public JudgeServerInterface.Obstacles GetObstacles()
        {
            return new Obstacles();
        }

        public void setUASTelemetry(float latitude, float longitude, float altitude, float heading)
        {

        }
    }
}

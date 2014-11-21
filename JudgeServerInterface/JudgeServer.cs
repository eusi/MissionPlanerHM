using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Net;
using System.Net.Http;

namespace JudgeServerInterface
{
    public class JudgeServer : IJudgeServer
    {
        private String username;
        private String password;
        private String server;

        enum ServerLinks {
            Login,
            ServerInfo,
            Obstacles,
            Telemetry
        }

        Dictionary<ServerLinks, String> serverLinks = new Dictionary<ServerLinks, string>() {
            {ServerLinks.Login, "/api/login"},
            {ServerLinks.ServerInfo, "/api/interop/server_info"},
            {ServerLinks.Obstacles, "/api/interop/obstacles"},
            {ServerLinks.Telemetry, "/api/interop/uas_telemetry"}
        };

        private HttpClient httpClient;

        private HttpResponseMessage Request(ServerLinks app, List<KeyValuePair<String, String>> post_parameters=null) {
            String address = this.server + serverLinks[app];

            HttpResponseMessage response;

            if (post_parameters == null) response = httpClient.GetAsync(address).Result;
            else { 
                HttpContent httpContent = new FormUrlEncodedContent(post_parameters);
                response = httpClient.PostAsync(address, httpContent).Result;
            }
            return response;
        }

        public void Connect(string server, string userName, string password)
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpClientHandler httpClientHandler = new HttpClientHandler() { CookieContainer = cookieContainer };

            this.server = server;
            this.username = userName;
            this.password = password;

            httpClient = new HttpClient(httpClientHandler) { BaseAddress = new System.Uri(server) };

            List<KeyValuePair<string, string>> post_parameters = new List<KeyValuePair<string, string>>();
            post_parameters.Add(new KeyValuePair<string, string>("username", this.username));
            post_parameters.Add(new KeyValuePair<string, string>("password", this.password));

            this.Request(ServerLinks.Login, post_parameters);
        }

        public ServerInfo GetServerInfo()
        {
            ServerInfo si = new ServerInfo();

            HttpResponseMessage response = this.Request(ServerLinks.ServerInfo);

            if (response.Content != null)
                si = JsonDeserializer.GetServerInfo(response.Content.ReadAsStringAsync().Result);

            return si;
        }

        public JudgeServerInterface.Obstacles GetObstacles()
        {
            Obstacles obs = new Obstacles();
            
            HttpResponseMessage response = this.Request(ServerLinks.Obstacles);

            if (response.Content != null)
                obs = JsonDeserializer.GetObstacles(response.Content.ReadAsStringAsync().Result);
            
            return obs;
        }

        public bool setUASTelemetry(double latitude, double longitude, double altitude, double heading)
        {
            CultureInfo culture = new CultureInfo("en-US");

            List<KeyValuePair<string, string>> post_parameters = new List<KeyValuePair<string, string>>();
            post_parameters.Add(new KeyValuePair<string, string>("latitude", latitude.ToString(culture)));
            post_parameters.Add(new KeyValuePair<string, string>("longitude", longitude.ToString(culture)));
            post_parameters.Add(new KeyValuePair<string, string>("altitude_msl", altitude.ToString(culture)));
            post_parameters.Add(new KeyValuePair<string, string>("uas_heading", heading.ToString(culture)));

            HttpResponseMessage response = this.Request(ServerLinks.Telemetry, post_parameters);

            if (response.StatusCode == HttpStatusCode.OK) return true;
            return false;
        }
    }
}

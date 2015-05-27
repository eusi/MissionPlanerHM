using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Net;
using RestSharp;

namespace JudgeServerInterface
{
    /// <summary>
    /// Implementation of the Interface for the communication with the JudgeServer
    /// </summary>
    public class JudgeServer : IJudgeServer
    {
        private String username;
        private String password;
        private String server;

        public IRestResponse LastResponse;

        public enum ServerLinks {
            Login,
            ServerInfo,
            Obstacles,
            Telemetry,
            EvaluateTeams
        }

        private Dictionary<ServerLinks, String> serverLinks = new Dictionary<ServerLinks, string>() {
            {ServerLinks.Login, "/api/login"},
            {ServerLinks.ServerInfo, "/api/interop/server_info"},
            {ServerLinks.Obstacles, "/api/interop/obstacles"},
            {ServerLinks.Telemetry, "/api/interop/uas_telemetry"},
             {ServerLinks.EvaluateTeams, "/auvsi_admin/evaluate_teams.csv"}
        };

        private RestClient httpClient;

        /// <summary>
        /// Creates and handles a request to the JudgeServer
        /// </summary>
        /// <param name="app">Application which is requested on the JudgeServer</param>
        /// <param name="post_parameters">List of parameters which are sent to the judge server</param>
        /// <returns>Response of the JudgeServer</returns>
        private IRestResponse Request(ServerLinks app, List<KeyValuePair<String, String>> post_parameters=null) {
            RestRequest request;

            if (post_parameters == null) request = (new RestRequest(serverLinks[app]));
            else {
                request = new RestRequest(serverLinks[app], Method.POST);
                foreach(KeyValuePair<String, String> p in post_parameters)
                    request.AddParameter(p.Key, p.Value);
            }

            LastResponse = httpClient.Execute(request);

            if (LastResponse.StatusCode == 0)
                throw new TimeoutException("Timeout for http request!");
            if (LastResponse.StatusCode == HttpStatusCode.BadRequest)
                throw new InvalidRequest(LastResponse.Content);
            if (LastResponse.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidUrl(LastResponse.Content);
            if (LastResponse.StatusCode == HttpStatusCode.InternalServerError)
                throw new InternalServerError(LastResponse.Content);
            if (LastResponse.StatusCode != HttpStatusCode.OK)
                throw new UnknownError(LastResponse.Content);

            return LastResponse;
        }

        /// <summary>
        /// Opens a connection to the JudgeServer and authenticates at the JudgeServer
        /// </summary>
        /// <param name="server">URL of the JudgeServer</param>
        /// <param name="userName">username for authentification</param>
        /// <param name="password">password for autehntification</param>
        public void Connect(string server, string userName, string password)
        {
            this.server = server;
            this.username = userName;
            this.password = password;

            httpClient = new RestClient() { BaseUrl = new System.Uri(server), CookieContainer = new CookieContainer() };

            List<KeyValuePair<string, string>> post_parameters = new List<KeyValuePair<string, string>>();
            post_parameters.Add(new KeyValuePair<string, string>("username", this.username));
            post_parameters.Add(new KeyValuePair<string, string>("password", this.password));

            this.Request(ServerLinks.Login, post_parameters);
        }

        /// <summary>
        /// Fetches the server informations from the JudgeServer
        /// </summary>
        /// <returns>server informations</returns>
        public ServerInfo GetServerInfo()
        {
            ServerInfo si = new ServerInfo();

            IRestResponse response = this.Request(ServerLinks.ServerInfo);

            if (response.Content != null)
                si = JsonDeserializer.GetServerInfo(response.Content);

            return si;
        }

        /// <summary>
        /// Fetches the Obstacles from the JudgeServer
        /// </summary>
        /// <returns>Obstacles</returns>
        public JudgeServerInterface.Obstacles GetObstacles()
        {
            Obstacles obs = new Obstacles();

            IRestResponse response = this.Request(ServerLinks.Obstacles);

            if (response.Content != null)
                obs = JsonDeserializer.GetObstacles(response.Content);
            
            return obs;
        }

        /// <summary>
        /// Fetches the Obstacles from the JudgeServer
        /// </summary>
        /// <returns>Obstacles</returns>
        public string EvaluateTeam()
        {
          

            IRestResponse response = this.Request(ServerLinks.EvaluateTeams);
            if (response != null)
                return response.Content;
            else
                return "";
            

        
        }

        /// <summary>
        /// Send the telemetry data of the airplane to the JudgeServer
        /// </summary>
        /// <param name="latitude">Latitude of the airplane</param>
        /// <param name="longitude">Longitude of the airplane</param>
        /// <param name="altitude">Altitude of the airplane in MSL</param>
        /// <param name="heading">Heading of the airplane</param>
        public void setUASTelemetry(double latitude, double longitude, double altitude, double heading)
        {
            CultureInfo culture = new CultureInfo("en-US");

            List<KeyValuePair<string, string>> post_parameters = new List<KeyValuePair<string, string>>();
            post_parameters.Add(new KeyValuePair<string, string>("latitude", latitude.ToString(culture)));
            post_parameters.Add(new KeyValuePair<string, string>("longitude", longitude.ToString(culture)));
            post_parameters.Add(new KeyValuePair<string, string>("altitude_msl", altitude.ToString(culture)));
            post_parameters.Add(new KeyValuePair<string, string>("uas_heading", heading.ToString(culture)));

            IRestResponse response = this.Request(ServerLinks.Telemetry, post_parameters);
        }
    }
}

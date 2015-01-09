using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;

namespace JSUnitTests
{
    /// <summary>
    /// Mock-Up of the real JudgeServer for unit testing purpose
    /// for testing the response string and response code can be set
    /// </summary>
    public class JudgeServerStub
    {
        public HttpListenerRequest LastRequest;

        private readonly HttpListener _listener = new HttpListener();
        private readonly String _responseString;
        private readonly HttpStatusCode _responseStatusCode;

        /// <summary>
        /// Initialise the JudgeServer-MockUp
        /// </summary>
        /// <param name="responseString">response string of the JudgeServer-MockUP</param>
        /// <param name="responseStatusCode">Response code of the JudgeServer-MockUp</param>
        public JudgeServerStub(String responseString, HttpStatusCode responseStatusCode)
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            _responseStatusCode = responseStatusCode;
            _responseString = responseString;

            _listener.Prefixes.Add("http://localhost:8080/");
            _listener.Prefixes.Add("http://localhost:8080/api/login/");
            _listener.Prefixes.Add("http://localhost:8080/api/interop/server_info/");
            _listener.Prefixes.Add("http://localhost:8080/api/interop/obstacles/");
            _listener.Prefixes.Add("http://localhost:8080/api/interop/uas_telemetry/");

            _listener.Start();
        }

        /// <summary>
        /// starts the JudgeServer-MockUp
        /// </summary>
        public void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            LastRequest = ctx.Request;
                            try
                            {
                                byte[] buf = Encoding.UTF8.GetBytes(_responseString);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.StatusCode = (int)_responseStatusCode;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch { } // suppress any exceptions
                            finally
                            {
                                // always close the stream
                                ctx.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch { } // suppress any exceptions
            });
        }

        /// <summary>
        /// Stops the JudgeServer-MockUp
        /// </summary>
        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}

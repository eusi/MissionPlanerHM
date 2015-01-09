using System;
using System.Net;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JudgeServerInterface;
using RestSharp;

namespace JSUnitTests
{
    /// <summary>
    /// Unit tests for the JudgeServerInterface
    /// </summary>
    [TestClass]
    public class JudgeServerTest
    {
        /// <summary>
        /// test if a correct request is working
        /// </summary>
        [TestMethod]
        public void TestOKRequest()
        {
            String responseString = "CorrectResponse";
            JudgeServerStub jss = new JudgeServerStub(responseString, HttpStatusCode.OK);
            jss.Run();

            JudgeServer js = new JudgeServer();
            js.Connect("http://localhost:8080", "", "");

            jss.Stop();

            Assert.AreEqual<String>(js.LastResponse.Content, responseString);
            Assert.AreEqual<HttpStatusCode>(js.LastResponse.StatusCode, HttpStatusCode.OK);
        }

        /// <summary>
        /// test if a request with an time out throws the right exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void TestTimeOutException()
        {
            JudgeServer js = new JudgeServer();
            js.Connect("http://localhost:8080", "", "");
        }

        /// <summary>
        /// test if a invalid request throws the right exception
        /// </summary>
        [TestMethod]
        public void TestInvalidRequest()
        {
            String responseString = "CorrectResponse";
            JudgeServerStub jss = new JudgeServerStub(responseString, HttpStatusCode.BadRequest);
            jss.Run();

            JudgeServer js = new JudgeServer();
            try {
                js.Connect("http://localhost:8080", "", "");
                Assert.Fail("No Exception");
            } catch (Exception e) {
                Assert.IsInstanceOfType(e, typeof(InvalidRequest));
            }
            
            jss.Stop();
        }

        /// <summary>
        /// test if a request with an non exising url throws the right exception
        /// </summary>
        [TestMethod]
        public void TestNotFoundRequest()
        {
            String responseString = "CorrectResponse";
            JudgeServerStub jss = new JudgeServerStub(responseString, HttpStatusCode.NotFound);
            jss.Run();

            JudgeServer js = new JudgeServer();
            try
            {
                js.Connect("http://localhost:8080", "", "");
                Assert.Fail("No Exception");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InvalidUrl));
            }
            jss.Stop();
        }

        /// <summary>
        /// test if a request with an internal server error throws the right exception
        /// </summary>
        [TestMethod]
        public void TestInternalServerErrorRequest()
        {
            String responseString = "CorrectResponse";
            JudgeServerStub jss = new JudgeServerStub(responseString, HttpStatusCode.InternalServerError);
            jss.Run();

            JudgeServer js = new JudgeServer();
            try
            {
                js.Connect("http://localhost:8080", "", "");
                Assert.Fail("No Exception");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InternalServerError));
            }
            jss.Stop();
        }
    }
}

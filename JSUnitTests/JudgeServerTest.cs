using System;
using System.Net;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JudgeServerInterface;
using RestSharp;

namespace JSUnitTests
{
    [TestClass]
    public class JudgeServerTest
    {
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

        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void TestTimeOutException()
        {
            JudgeServer js = new JudgeServer();
            js.Connect("http://localhost:8080", "", "");
        }

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

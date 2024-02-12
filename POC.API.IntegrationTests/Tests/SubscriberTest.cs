using POC.API.IntegrationTests.Http.Clients;
using System.Net;

namespace POC.API.IntegrationTests.Tests
{
    public class SubscriberTests:TestsSetUpBase
    {

        [Test]
        public void Test1()
        {
           var response = PocHttpClient!.RetrieveSubscriberById(CancellationToken.None).Result;
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
using FluentAssertions;
using Newtonsoft.Json;
using POC.API.IntegrationTests.Http.Clients;
using POC.API.IntegrationTests.Models.Requests;
using POC.API.IntegrationTests.Models.Responses;
using System.Net;
using System.Net.Http.Headers;

namespace POC.API.IntegrationTests.Tests
{
    public class ValidateAuthTests:TestsSetUpBase
    {
        public ValidateAuthTests() : base("")
        {
            }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerifyNotAuthorizedCall()
        {
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var payload = new CreateSubscribeReq
            {
                email = currentTimestamp + "@123.com",
                first_name = "a",
                last_name = "b",
                service = "basic"

            };

            var response = PocHttpClient!.CreateSubscriber(payload, CancellationToken.None).Result;
            //check if the response status code is 401
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);                    
                
        }
        
    }
}
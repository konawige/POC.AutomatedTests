using FluentAssertions;
using Newtonsoft.Json;
using POC.API.IntegrationTests.Http.Clients;
using POC.API.IntegrationTests.Models.Requests;
using POC.API.IntegrationTests.Models.Responses;
using System.Net;
using System.Net.Http.Headers;

namespace POC.API.IntegrationTests.Tests
{
    public class SubscriberTests:TestsSetUpBase
    {
        public SubscriberTests() : base(ConfigurationConstants.Endpoints.OAuth)
        {
            }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerifySubscription()
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
            response.EnsureSuccessStatusCode();
                
            //deserialize the response as json of type CreateSubscriberRes
            var result = response.Content.ReadAsStringAsync().Result;
            var subscriber = JsonConvert.DeserializeObject<CreateSubscribeResp>(result);

            //use fluent assertion to validate the service with the payload
            subscriber.service.Should().Be(payload.service);
            //assert that service subscriber_id is not null
            subscriber.subscriber_id.Should().NotBe(null);
            //parse date_subscriber as date and validate it corresponds to today's date
            var date_subscribed = DateTime.Parse(subscriber.date_subscribed);
            date_subscribed.Date.Should().Be(DateTime.Now.Date);
                
        }

        [Test]
        public void GivenNewSubscriberGetSubscriberInfo()
        {
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var payload = new CreateSubscribeReq
            {
                email = currentTimestamp + "@123.com",
                first_name = "c",
                last_name = "d",
                service = "advanced"

            };

            var response = PocHttpClient!.CreateSubscriber(payload, CancellationToken.None).Result;
            response.EnsureSuccessStatusCode();

            //deserialize the response as json of type CreateSubscriberRes
            var result = response.Content.ReadAsStringAsync().Result;
            var subscriberResp = JsonConvert.DeserializeObject<CreateSubscribeResp>(result);

            int id = subscriberResp.subscriber_id;
            //get the subscriber by id
            var subscriberinfo = PocHttpClient!.RetrieveSubscriberById(id, CancellationToken.None).Result;
            //validate the response status code
            subscriberinfo.StatusCode.Should().Be(HttpStatusCode.OK);
            //deserialize the response as json of type Subscriber
            var subscriber = JsonConvert.DeserializeObject<Subscriber>(subscriberinfo.Content.ReadAsStringAsync().Result);
            //use fluent assertion to validate subscriber and the payload match
            subscriber.service.Should().Be(payload.service);
            subscriber.email.Should().Be(payload.email);
            subscriber.first_name.Should().Be(payload.first_name);
            subscriber.last_name.Should().Be(payload.last_name);


        }
    }
}
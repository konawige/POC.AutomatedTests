using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POC.API.IntegrationTests.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Http.Clients
{
    public class PocHttpClient: HttpClientBase, IPocHttpClient
    {
        public PocHttpClient(IHttpClientFactory httpClientFactory, ILogger<HttpClientBase> logger, IOptionsSnapshot<EndpointsOption> option) :
            base(httpClientFactory, logger, option, ConfigurationConstants.Endpoints.Poc)
        {          
        }

        public async Task<HttpResponseMessage> CreateSubscriber(CreateSubscribeReq payload, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Option.CreateSubscriber);
            request.Content = JsonContent.Create(payload);
            var response = await GetClient().SendAsync(request,cancellationToken);
            return response;
        }

        public async Task<HttpResponseMessage> RetrieveSubscriberById(int id, CancellationToken cancellationToken)
        {
            var response = await GetClient().GetAsync(Option.GetSubscriberById.Replace(":id", id.ToString()), cancellationToken);
            return response;
        }
        public override HttpClient GetClient()
        {
            HttpClient client = _httpClientFactory.CreateClient(typeof(IPocHttpClient).Name);
            client.BaseAddress = Option.BaseUrl;
            return client;
        }
    }
    
}

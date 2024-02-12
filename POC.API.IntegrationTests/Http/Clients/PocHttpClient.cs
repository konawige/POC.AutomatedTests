using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<HttpResponseMessage> CreateSubscriber()
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> RetrieveSubscriberById(CancellationToken cancellationToken)
        {
            var response = await GetClient().GetAsync(Option.GetSubscriberByIdEndpoint, cancellationToken);
            return response;
        }
        public override HttpClient GetClient()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = Option.BaseUrl;
            return client;
        }
    }
    
}

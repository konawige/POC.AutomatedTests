using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Http.Clients
{
    public abstract class HttpClientBase: IHttpClient
    {
        protected IHttpClientFactory _httpClientFactory;
        protected readonly ILogger<HttpClientBase> _logger;

        protected readonly EndpointsOption Option;

        protected HttpClientBase(IHttpClientFactory httpClientFactory, ILogger<HttpClientBase> logger, IOptionsSnapshot<EndpointsOption> option, string configEndpointKey)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            Option = option.Get(configEndpointKey);
            //Configuration.GetSection(configEndpointKey).Bind(Option);
        }
        public abstract HttpClient GetClient();
    }
}

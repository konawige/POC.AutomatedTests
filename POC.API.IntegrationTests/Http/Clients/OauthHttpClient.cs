using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Http.Clients
{
    public class OauthHttpClient: HttpClientBase, IOauthHttpClient
    {
        private readonly OauthParamValue _oauthParam;
        private readonly OauthCredentialValue _oauthCredential;
        public OauthHttpClient(IHttpClientFactory httpClientFactory, ILogger<HttpClientBase> logger, IOptionsSnapshot<EndpointsOption> option, 
            IOptions<OauthParamValue> oauthParam, IOptions<OauthCredentialValue> oauthCredential) :
            base(httpClientFactory, logger, option, ConfigurationConstants.Endpoints.OAuth)
        {
            _oauthParam = oauthParam.Value;
            _oauthCredential = oauthCredential.Value;
        }

        public async Task<HttpResponseMessage> ObtainToken(CancellationToken cancellationToken)
        {
            _oauthParam.ClientSecret = _oauthCredential.ClientSecret;
            _oauthParam.GrantType = "client_credentials";
            var response = await GetClient().PostAsJsonAsync(Option.ObtainToken, _oauthParam, cancellationToken);
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

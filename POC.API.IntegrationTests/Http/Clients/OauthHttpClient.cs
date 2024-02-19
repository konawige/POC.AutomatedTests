using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POC.API.IntegrationTests.Models.Responses;
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
        //private readonly OauthCredentialValue _oauthCredential;
        public OauthHttpClient(IHttpClientFactory httpClientFactory, ILogger<HttpClientBase> logger, IOptionsSnapshot<EndpointsOption> option, 
            IOptions<OauthParamValue> oauthParam) :
            base(httpClientFactory, logger, option, ConfigurationConstants.Endpoints.OAuth)
        {
            _oauthParam = oauthParam.Value;
        }

        public async Task<TokenResponse> ObtainToken(CancellationToken cancellationToken)
        {
            _oauthParam.GrantType = "client_credentials";
            var response = await GetClient().PostAsJsonAsync(Option.ObtainToken, _oauthParam, cancellationToken);
            response.EnsureSuccessStatusCode();
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken);
            return tokenResponse!;
        }
        public override HttpClient GetClient()
        {
            HttpClient client = _httpClientFactory.CreateClient(typeof(IOauthHttpClient).Name);
            client.BaseAddress = Option.BaseUrl;
            return client;
        }
    } 
}

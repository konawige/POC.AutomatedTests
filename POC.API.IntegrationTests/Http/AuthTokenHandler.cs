using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using POC.API.IntegrationTests.Http.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Http
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly IOauthHttpClient _authService;
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheToken="IOauthToken";
        public AuthTokenHandler(IOauthHttpClient authService, IMemoryCache memoryCache)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Check if the access token is in the cache
            if (!_memoryCache.TryGetValue(_cacheToken, out string accessToken))
            {
                // Call the authentication service to get an access token
                var tokenResponse = await _authService.ObtainToken(cancellationToken);
                accessToken = tokenResponse.access_token;
                // Cache the access token
                _memoryCache.Set(_cacheToken, accessToken, TimeSpan.FromSeconds((tokenResponse.expires_in-300)));
                
            }
            
            request.Headers.Add("Authorization", "Bearer " + accessToken);
           
            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}

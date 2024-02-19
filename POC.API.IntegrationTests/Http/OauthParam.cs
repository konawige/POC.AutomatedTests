using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace POC.API.IntegrationTests.Http
{
    public class OauthParamValue
    {
        public const string OauthParam = "OauthParam";

        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; set; }

        [JsonPropertyName("grant_type")]
        public string? GrantType { get; set; }

        [JsonPropertyName("audience")]
        public string? Audience { get; set; }
    }
}

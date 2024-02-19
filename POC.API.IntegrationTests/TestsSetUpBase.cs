using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using POC.API.IntegrationTests.Http;
using POC.API.IntegrationTests.Http.Clients;
using System.Net.Http;

namespace POC.API.IntegrationTests
{
    [TestFixture]
    public abstract class TestsSetUpBase
    {
        private readonly string _tokenSection;
        protected TestsSetUpBase(string tokenSection)
        {
            _tokenSection = tokenSection;
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var builder = WebApplication.CreateBuilder();
            var environment = builder.Configuration.GetValue<string>("DOTNET_ENV");

            builder.Configuration
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.{environment}.json", true)
                .AddUserSecrets<TestsSetUpBase>()
                .AddEnvironmentVariables();

            builder.Services.Configure<OauthParamValue>(
                builder.Configuration.GetSection(OauthParamValue.OauthParam));

            builder.Services.AddHttpClient();
            builder.Services.ConfigureClients(ConfigurationConstants.Endpoints.Poc);
            builder.Services.ConfigureClients(ConfigurationConstants.Endpoints.OAuth);

            builder.Services.AddTransient<AuthTokenHandler>();
            builder.Services.AddMemoryCache();

            if (_tokenSection == ConfigurationConstants.Endpoints.OAuth)
            {
                builder.Services.AddHttpClient<IPocHttpClient>()
                    .AddHttpMessageHandler<AuthTokenHandler>();
            }
                
            builder.Services.ScanAndRegisterHttpClients();

            var host = builder.Build();

            OauthHttpClient = host?.Services.GetRequiredService<IOauthHttpClient>();
            PocHttpClient = host?.Services.GetRequiredService<IPocHttpClient>();
           

        }
        protected IPocHttpClient? PocHttpClient { get;  set; }
        protected IOauthHttpClient? OauthHttpClient { get;  set; }
       
    }
    

}

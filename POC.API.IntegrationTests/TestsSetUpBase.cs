using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using POC.API.IntegrationTests.Http;
using POC.API.IntegrationTests.Http.Clients;

namespace POC.API.IntegrationTests
{
    [TestFixture]
    public abstract class TestsSetUpBase
    {
        
        protected TestsSetUpBase()
        {      
        }

        [SetUp]
        public void Setup()
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

            builder.Services.Configure<OauthCredentialValue>(
                builder.Configuration.GetSection(OauthCredentialValue.OauthCredential));

            builder.Services.AddHttpClient();
            builder.Services.ConfigureClients(ConfigurationConstants.Endpoints.Poc);
            builder.Services.ConfigureClients(ConfigurationConstants.Endpoints.OAuth);
            builder.Services.ScanAndRegisterHttpClients();

            var host = builder.Build();

            OauthHttpClient = host?.Services.GetRequiredService<IOauthHttpClient>();

        }
        protected IPocHttpClient? PocHttpClient { get;  set; }
        protected IOauthHttpClient? OauthHttpClient { get;  set; }
    }
}

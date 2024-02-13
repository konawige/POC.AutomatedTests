using Microsoft.Extensions.DependencyInjection;
using POC.API.IntegrationTests.Http.Clients;


namespace POC.API.IntegrationTests.Http
{
    public static class ClientsConfiguration
    {
        public static IServiceCollection ConfigureClients(this IServiceCollection services, string endPoint)
        {
           services.AddOptions<EndpointsOption>(endPoint)
                .BindConfiguration(endPoint);
            //services.ScanAndRegisterHttpClients();

            return services;
        }
        // 
        public static IServiceCollection ScanAndRegisterHttpClients(this IServiceCollection services)
        {
            
            services.Scan(scan => scan
            .FromAssemblyOf<IHttpClient>()
            .AddClasses(classes => classes.AssignableTo<IHttpClient>()).AsMatchingInterface()
            .WithScopedLifetime());

            return services;
        }
        
    }
}

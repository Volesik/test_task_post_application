using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostApp.Web.Common.HttpClients;
using Refit;

namespace PostApp.Web.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddWebCommonServices(this IServiceCollection services, IConfiguration manager)
    {
        services.AddRefitClients(manager);
    }
    
    private static void AddRefitClients(this IServiceCollection services, IConfiguration manager)
    {
        services.AddRefitClient<IDataServiceApiClient>()
            .ConfigureHttpClient(httpClient =>
            {
                httpClient.BaseAddress = new Uri(manager["UserDataApiIntegrationSettings:BaseUrl"]!);
            });
    }
}
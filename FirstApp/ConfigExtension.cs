using Azure;
using Azure.Search.Documents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FirstApp
{
    public static class ConfigExtension
    {
        public static IServiceCollection AddCognitiveSearchClient(this IServiceCollection services, IConfiguration configuration)
        {
            var ApiKey = configuration.GetValue<string>("AzureSearchAppKey");
            var LocationsIndex = configuration.GetValue<string>("AzureSearchIndex");
            var ServiceEndpoint = configuration.GetValue<string>("AzureSearchUri");
            var credential = new AzureKeyCredential(ApiKey);
            var uri = new Uri(ServiceEndpoint);
            var indexName = LocationsIndex;

            services.AddScoped(client => new SearchClient(uri, indexName, credential));
            return services;
        }
    }
}
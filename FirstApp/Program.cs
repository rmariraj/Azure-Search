using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureAppConfiguration((context, config) =>
             {
                 var keyVaultUrl = config.Build().GetValue<string>("KeyVaultUrl");
                 var azureServiceTokenProvider = new AzureServiceTokenProvider();
                 var keyVaultClient = new KeyVaultClient(
                     new KeyVaultClient.AuthenticationCallback(
                         azureServiceTokenProvider.KeyVaultTokenCallback));

                 config.AddAzureKeyVault(
                     keyVaultUrl,
                     keyVaultClient,
                     new DefaultKeyVaultSecretManager());
             })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

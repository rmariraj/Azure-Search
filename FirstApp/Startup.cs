using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApp;
using Microsoft.EntityFrameworkCore;
using Azure;
using Azure.Search.Documents;

namespace FirstApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISqlRepository, SqlRepository>();
            services.AddTransient<ICognitiveRepository, CognitiveRepository>();

            services.AddControllers();

            //services.AddScoped(AzureRetryPolicy, SalesLTContext);

            services.AddDbContext<SalesLTContext>(s => s.UseSqlServer(Configuration.GetValue<string>("customerDbConnection")
                //,sqlServerOptionsAction: sqlOptions =>
                //{
                //    sqlOptions.EnableRetryOnFailure(
                //    maxRetryCount: 10,
                //    maxRetryDelay: TimeSpan.FromSeconds(30),
                //    errorNumbersToAdd: null);
                //}));
                , sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.ExecutionStrategy(dependencies =>
                        new CustomRetryPolicy(
                        dependencies,
                        Configuration.GetValue<int>("maxRetryCount"),
                        TimeSpan.FromSeconds(Configuration.GetValue<int>("maxRetryDelay"))));
                }));
            services.AddSwaggerDocument();

            services.AddCognitiveSearchClient(Configuration);

            //services.AddApplicationInsightsTelemetry(Configuration);

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}

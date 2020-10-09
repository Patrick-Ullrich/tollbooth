using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSwag;
using NSwag.Generation.Processors.Security;
using Persistence;
using Serilog;
using WebUI.Extensions;
using WebUI.Middleware;

namespace WebUI
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
            services.AddControllers();

            // 5. Https Redirect
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            // Adding In Memory Database
            services.AddPersistence(Configuration);

            // Adding Application Layer
            services.AddApplication();

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
            .AddApiKeySupport(options => { });

            // 2. - Service to generate Swagger 3 Documentation
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "TollBooth";
                // TODO: Adjust based on our Security
                configure.OperationProcessors.Add(new OperationSecurityScopeProcessor("ApiKey"));
                configure.AddSecurity("ApiKey", new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Name = "X-Api-Key",
                    Description = "Api Key"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                // 5. Add HTTPS
                app.UseHsts();
            }

            app.UseCustomExceptionHandler();
            // 5. Add HTTPS
            app.UseHttpsRedirection();

            // 4. Authentication
            app.UseAuthentication();


            // 2. - Register Swagger Documentation
            app.UseOpenApi();
            app.UseSwaggerUi3();

            // 3. Add Routing To Requests
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

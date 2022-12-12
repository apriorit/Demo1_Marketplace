using Autofac;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Marketplace.Api.Helpers;
using Marketplace.Api.Helpers.Validation;
using Marketplace.Api.Middleware;
using Marketplace.Bootstrapper;
using Marketplace.Data;
using Marketplace.Data.Infrastructure;
using Marketplace.Integrations.ElasticSearch.Models;
using Marketplace.Services.Elastic;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Logging;

namespace Marketplace.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        private IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext();

            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(new ErrorResponse("Validation Failed", new ValidationResultModel(actionContext.ModelState)));
                };
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Marketplace API",
                    Description = "Marketplace ASP.NET Core Web API"
                });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{Configuration.GetValue<string>("IdentityProviderBaseUrl")}/connect/authorize"),
                            TokenUrl = new Uri($"{Configuration.GetValue<string>("IdentityProviderBaseUrl")}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                ["Marketplaceapi_scope"] = "Product Stock API"
                            }
                        }
                    }
                });
                c.OperationFilter<AuthorizeOperationFilter>();
                c.IncludeXmlComments("Marketplace.Api.xml");
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.ApiName = "Marketplaceapi";
                        options.Authority = Configuration.GetValue<string>("IdentityProviderBaseUrl");
                        options.RequireHttpsMetadata = Environment.IsProduction();
                    });

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCorsPolicy", corsOptions =>
                {
                    corsOptions.SetIsOriginAllowed(host => true)
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //get settigns as object from config
            var elkSettings = Configuration.GetSection(typeof(ElasticSettings).Name).Get<ElasticSettings>();
            builder.RegisterModule(new ElasticSearchModule(elkSettings));
            builder.RegisterModule(new ServiceContainer());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext db)
        {
            app.UseDeveloperExceptionPage();
            IdentityModelEventSource.ShowPII = true;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
                options.OAuthClientId("Marketplaceapi_swagger");
                options.OAuthAppName("Product Stock API");
                options.OAuthUsePkce();
                options.OAuthScopes("Marketplaceapi_scope");
            });

            app.UseCors("DefaultCorsPolicy");

            app.UseErrorHandlingMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

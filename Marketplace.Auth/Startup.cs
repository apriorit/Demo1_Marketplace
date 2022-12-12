using IdentityServer.Api.Helpers;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace IdentityServer
{
    public class Startup
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        #endregion

        private IConfiguration Configuration { get; }
        private IHostEnvironment Environment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => AddDbContext(options, Configuration));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddAspNetIdentity<ApplicationUser>()
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = builder => AddDbContext(builder, Configuration);
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder => AddDbContext(builder, Configuration);
                    });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Authentication Server API",
                    Description = "Authentication Server API"
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
            });

            services.AddScoped<AccountService>();

            services.AddControllersWithViews();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application configuration provider</param>
        /// <param name="env">Application hosting environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();


            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });

        }

        #region Helpers

        /// <summary>
        /// Configures the database options
        /// </summary>
        /// <param name="builder">Database options builder</param>
        /// <param name="configuration">Application configuration provider</param>
        /// <returns>Configured database options builder</returns>
        private static DbContextOptionsBuilder AddDbContext(DbContextOptionsBuilder builder,
            IConfiguration configuration) =>
            builder.UseSqlServer(configuration.GetConnectionString("Default"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });

        #endregion
    }
}

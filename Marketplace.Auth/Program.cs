using IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();

            try
            {
                var host = CreateHostBuilder(args).Build();

                using var scope = host.Services.CreateScope();

                var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await MigrateDatabaseAsync(applicationDbContext, () => Task.CompletedTask);

                var configurationContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                await MigrateDatabaseAsync(configurationContext,
                    async () => await new ConfigurationDataSeeder().SeedAsync(configurationContext, configuration));

                var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                await MigrateDatabaseAsync(persistedGrantDbContext, () => Task.CompletedTask);

                await host.RunAsync();
            }
            catch
            {
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddLog4Net("log4net.config");
                });
                
        #region Helpers

        /// <summary>
        /// Creates the application configuration container
        /// </summary>
        /// <returns><see cref="IConfiguration"/></returns>
        private static IConfiguration GetConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

        /// <summary>
        /// Migrates the database and runs the data seeder
        /// </summary>
        /// <typeparam name="TContext">Type of data context</typeparam>
        /// <param name="context">Database context</param>
        /// <param name="seeder">Data seeder action to run</param>
        /// <returns>Task object representing the asynchronous operation</returns>
        private static async Task MigrateDatabaseAsync<TContext>(TContext context, Func<Task> seeder)
            where TContext : DbContext
        {
            await context.Database.MigrateAsync();
            await seeder();
        }

        #endregion
    }
}
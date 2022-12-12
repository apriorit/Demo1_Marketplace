using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Data.Infrastructure
{
    /// <summary>
    /// Represents configuration to be applied at bootstrapping for DI configuration.
    /// </summary>
    public static class DependencyResolverExtensions
    {
        /// <summary>
        /// Adds database context to DI service collection.
        /// </summary>
        /// <param name="services">DI services collection.</param>
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
        }

        /// <summary>
        /// Adds services to DI service collection.
        /// </summary>
        /// <param name="services">DI services collection.</param>
        public static void RegisterServices(this IServiceCollection services)
        {

        }
    }
}

using IdentityServer4.Models;

namespace IdentityServer.Data
{
    public static class Config
    {
        /// <summary>
        /// Adds the available scopes for the apis
        /// </summary>
        /// <returns>List of scopes</returns>
        public static IEnumerable<ApiScope> Scopes() =>
            new List<ApiScope> { new ApiScope("Marketplaceapi_scope", "Full access to product stock api") };

        /// <summary>
        /// Gets the list of apis defined in the system
        /// </summary>
        /// <returns>List of apis</returns>
        public static IEnumerable<ApiResource> Apis() =>
            new List<ApiResource>
            {
                new ApiResource("Marketplaceapi", "Product Stock"){Scopes = {"Marketplaceapi_scope"}}
            };

        /// <summary>
        /// Gets a list of identity resources for a user. For example
        /// user id, email, profile information etc
        /// </summary>
        /// <returns>List of identity resources</returns>
        public static IEnumerable<IdentityResource> Resources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        /// <summary>
        /// Gets the list of approved clients with access to api resources
        /// </summary>
        /// <returns>List of approved clients</returns>
        public static IEnumerable<Client> Clients(string apiUrl, string identityUrl) =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "Marketplaceapi_swagger",
                    ClientName = "Product Stock API Swagger UI",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = {$"{apiUrl}/oauth2-redirect.html", $"{identityUrl}/oauth2-redirect.html"},
                    AllowedCorsOrigins = {$"{apiUrl}"},
                    PostLogoutRedirectUris = {$"{apiUrl}", $"{identityUrl}"},
                    AllowedScopes = { "Marketplaceapi_scope" }
                }
            };
    }
}
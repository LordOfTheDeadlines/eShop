using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.API
{
    public class Config
    {
        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl) =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "adminClient",
                    ClientName = "Admin",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("AdminSecrets".Sha256())
                    },
                    ClientUri = $"{clientsUrl["AdminWebApp"]}",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    RedirectUris = {$"{clientsUrl["AdminWebApp"]}/signin-oidc"},
                    PostLogoutRedirectUris = {$"{clientsUrl["AdminWebApp"]}/signout-callback-oidc"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "AdminWebApp",
                    },
                    // for dev need to set 'false' [GrantTypes.Hybrid]
                    RequirePkce = false,
                },
                new Client
                {
                    ClientId = "shopClient",
                    ClientName = "Catalog.API",
                    ClientSecrets = { new Secret("CatalogAPISecrets".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"ShopWebApp"},
                },
            };
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("AdminWebApp"),
                new ApiScope("ShopWebApp")
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("AdminWebApp", "Admin Service"){ Scopes = { "AdminWebApp" }},
                new ApiResource("ShopWebApp", "Catalog Service"){ Scopes = { "ShopWebApp" }},
            };
    }
}

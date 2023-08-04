using IdentityServer.Configurations.Client;
using IdentityServer.Extensions;
using IdentityServer.Models.Constants;
using IdentityServer.Models.Enums;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer
{
    public class IdentityConfig
    {
        public static IEnumerable<Client> Clients(ClientSettings settings)
        {
            return new Client[]
            {
                new Client
                {
                    ClientName = settings.MovieAppClient.ClientName,
                    ClientId = settings.MovieAppClient.ClientId,
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(settings.MovieAppClient.ClientSecret.Sha512())
                    },
                    AllowedScopes =
                    {
                        ApprovedScopes.MovieApi.ToString(),
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        ApprovedScopes.Roles.GetEnumMemberAttributeValue()
                    },
                    RequirePkce = true,
                    RedirectUris = new List<string> { settings.MovieAppClient.RedirectUris, "https://localhost:4200/movies" },
                    PostLogoutRedirectUris = new List<string> { settings.MovieAppClient.RedirectUris },
                    RequireClientSecret = false,
                    AccessTokenType = AccessTokenType.Jwt,
                }
            };
        }


        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(ApprovedScopes.MovieApi.ToString(), ScopeName.MovieApi),
                new ApiScope(ApprovedScopes.JobApi.ToString(), ScopeName.JobApi)
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("movieApi", "Movie Api")
                {
                    Scopes = { ApprovedScopes.MovieApi.ToString() }
                },
                new ApiResource("jobApi", "Movie Api")
                {
                    Scopes = { ApprovedScopes.JobApi.ToString() }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User role(s)", new List<string>{ "role" })
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "abc1",
                    Username = "Frank",
                    Password = "FrankPass",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Frank"),
                        new Claim("family_name", "Ozz"),
                        new Claim("address", "132 sample road, India"),
                        new Claim("role", UserRoles.Admin.ToString())
                    },
                },
                new TestUser
                {
                    SubjectId = "abc2",
                    Username = "Seros",
                    Password = "SerosPass",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Seros"),
                        new Claim("family_name", "Ozz"),
                        new Claim("role", UserRoles.Viewer.ToString())
                    }
                }
            };
    }
}

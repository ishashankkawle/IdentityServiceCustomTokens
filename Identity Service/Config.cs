using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Secret = IdentityServer4.Models.Secret;

namespace Identity_Service
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1" , "TodoApi")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "MVC Client",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = { "custom" },
                    //RedirectUris = { "http://localhost:7002/api/TaskItems" },


                    //secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                    //AllowedScopes = new List<string>
                    //{
                    //    IdentityServerConstants.StandardScopes.OpenId,
                    //    IdentityServerConstants.StandardScopes.Profile
                    //}
                }
            };
        }
    }
}
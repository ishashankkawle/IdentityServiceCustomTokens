using Client.Models;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TokenResponse = IdentityModel.Client.TokenResponse;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Program t = new Program();
         
            t.requestTokenAsync();

            Console.Read();
        }

        public async void requestTokenAsync()
        {
            // discover endpoints from metadata

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5001/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            //request token
            var response = await client.RequestTokenAsync(new TokenRequest
            {
                Address = disco.TokenEndpoint,
                GrantType = "custom",
                ClientId = "client",
                ClientSecret = "secret",
                Parameters = {
                                { "Scope" , "api1" },
                                { "token", "1" }
                             }
            });

            //var response = await client.GetAsync("http://localhost:7002/api/TaskItems?id=123");

            if (response.IsError)
            {
                Console.WriteLine(response.Error);
            }

            var resp = JsonConvert.DeserializeObject<IdentityResp>(response.Raw);
            //Console.WriteLine(resp.UserId.UsId);
            CallAPIAsync(response , resp.UserId.UsId);


        }

      

        public async void CallAPIAsync(TokenResponse tokenResponse , int UserId)
        {
            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:7002/api/TaskItems/" + UserId);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }

       
}

using Microsoft.Extensions.Logging;
using ShopWebApp.Extentions;
using ShopWebApp.Models;
using ShopWebApp.Services.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShopWebApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;

        public AccountService(HttpClient client, ILogger<AccountService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<HttpResponseHeaders> Login(LoginModel model)
        {

            var response = await _client.PostAsJson($"/Login", model);
            if (response.IsSuccessStatusCode)
                return response.Headers;
            else
            {
                throw new Exception("something went wrong when calling api.");
            }
        }

        public async Task<HttpResponseHeaders> Register(RegisterModel model)
        {
            var response = await _client.PostAsJson($"/Register", model);
            if (response.IsSuccessStatusCode)
                return response.Headers;
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}

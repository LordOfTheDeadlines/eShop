using AdminWebApp.Extensions;
using AdminWebApp.Models;
using AdminWebApp.Models.Account;
using AdminWebApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminWebApp.Services
{
    public class AccountService:IAccountService
    {
        private readonly HttpClient _client;

        public AccountService(HttpClient client, ILogger<AccountService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<AuthResponse<UserModel>> Login(LoginModel model)
        {
            var response = await _client.PostAsJson($"/api/v1/Account/Login", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<AuthResponse<UserModel>>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public async Task<AuthResponse<UserModel>> Register(RegisterModel model)
        {
            var response = await _client.PostAsJson($"/api/v1/Account/Register", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<AuthResponse<UserModel>>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}

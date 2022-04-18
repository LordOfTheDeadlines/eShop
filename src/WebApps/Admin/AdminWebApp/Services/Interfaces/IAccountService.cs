using AdminWebApp.Models;
using AdminWebApp.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AdminWebApp.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<HttpResponseHeaders> Register(RegisterModel registerModel);
        public Task<HttpResponseHeaders> Login(LoginModel loginModel);
    }
}

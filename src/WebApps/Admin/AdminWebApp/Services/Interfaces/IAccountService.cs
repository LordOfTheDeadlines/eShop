using AdminWebApp.Models;
using AdminWebApp.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApp.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<AuthResponse<UserModel>> Register(RegisterModel registerModel);
        public Task<AuthResponse<UserModel>> Login(LoginModel loginModel);
    }
}

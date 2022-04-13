using Auth.API.Data.Entities;
using Auth.API.Data.Models.Requests;
using Auth.API.Data.Models.Responses;

namespace Auth.API.Services.Interfaces
{
    public interface IAccountService
    {
        public ResultResponse<AppUser> Register(RegisterModel registerModel);
        public ResultResponse<AppUser> Login(LoginModel loginModel);
    }
}

using ShopWebApp.Models;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShopWebApp.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<HttpResponseHeaders> Register(RegisterModel registerModel);
        public Task<HttpResponseHeaders> Login(LoginModel loginModel);
    }
}

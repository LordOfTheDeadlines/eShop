using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Models;
using ShopWebApp.Services.Interfaces;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync([Bind("Username,Password")] LoginModel model)
        {
            var headers = await _accountService.Login(model);
            SetCookies("Authorization", headers);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync([Bind("DisplayName,Email,Password,Username")] RegisterModel model)
        {
            var headers = await _accountService.Register(model);
            SetCookies("Authorization", headers);
            return View();
        }
        private void SetCookies(string key, HttpResponseHeaders headers)
        {
            HttpContext.Response.Cookies
               .Append(key,
               headers.TryGetValues(key, out var value) ? value.First().Replace("Bearer ", "") : null,
               new CookieOptions()
               {
                   Expires = DateTime.Now.AddDays(5)
               });
        }
    }
}

using AuthWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApp.Controllers
{
    public class AccountController : Controller
    {
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
        public IActionResult Login([Bind("Username,Password")] LoginModel model)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("DisplayName,Email,Password,Username")] RegisterModel model)
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Models;
using ShopWebApp.Models.Account;
using ShopWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopWebApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public async Task<ActionResult> Index()
        {
            var userId = HttpContext.User; 
            return View(await _basketService.GetBasket(GetUserData().Id));
        }

        public async Task<ActionResult> DeleteFromBasket(int productId)
        {
            await _basketService.DeleteFromBasket(GetUserData().Id, productId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> AddToBasket(int productId)
        {
            await _basketService.AddToBasket(GetUserData().Id, productId);
            return RedirectToAction(nameof(Index));
        }

        private UserModel GetUserData()
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(HttpContext.Request.Cookies["Authorization"]);
            var jti = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;
            return JsonSerializer.Deserialize<UserModel>(jti);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Models;
using ShopWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var userName = "swn";
            return View(await _basketService.GetBasket(1));
        }

        public async Task<ActionResult> EditAsync(int productId)
        {
            var userName = "swn";
            await _basketService.DeleteFromBasket(1, productId);
            return View(await _basketService.GetBasket(1));
        }

        public async Task<ActionResult> AddToBasket(int productId)
        {
            var userName = "swn";
            await _basketService.AddToBasket(1, productId);
            return View(await _basketService.GetBasket(1));
        }

        private void GetCookies()
        {
            var authCookies = HttpContext.Request.Cookies["Authorization"];
        }
    }
}

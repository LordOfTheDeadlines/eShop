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

        public async Task<ActionResult> IndexAsync()
        {
            var userName = "swn";
            return View(await _basketService.GetBasket(userName));
        }

        public async Task<ActionResult> EditAsync(string productId)
        {
            var userName = "swn";
            var basket = await _basketService.GetBasket(userName);

            var item = basket.Items.Single(x => x.ProductId == productId);
            basket.Items.Remove(item);

            var basketUpdated = await _basketService.UpdateBasket(basket);
            return View(basket);
        }
    }
}

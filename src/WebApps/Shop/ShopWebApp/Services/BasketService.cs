using ShopWebApp.Extentions;
using ShopWebApp.Models;
using ShopWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopWebApp.Services
{
    public class BasketService:IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Cart> AddToBasket(int userId, int productId)
        {
            var response = await _client.GetAsync($"/api/v1/Basket/{userId}/{productId}");
            return await response.ReadContentAs<Cart>();
        }

        public async Task DeleteFromBasket(int userId, int productId)
        {
            await _client.DeleteAsync($"/api/v1/Basket/{userId}");
        }


        public async Task<Cart> GetBasket(int userId)
        {
            var response = await _client.GetAsync($"/api/v1/Basket/{userId}");
            return await response.ReadContentAs<Cart>();
        }
    }
}

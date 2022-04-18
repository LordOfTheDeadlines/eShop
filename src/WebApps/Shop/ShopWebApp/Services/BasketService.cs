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

        public async Task<Cart> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/Basket/{userName}");
            return await response.ReadContentAs<Cart>();
        }

        public async Task<Cart> UpdateBasket(Cart model)
        {
            var response = await _client.PostAsJson($"/Basket", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<Cart>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        //public async Task CheckoutBasket(BasketCheckoutModel model)
        //{
        //    var response = await _client.PostAsJson($"/Basket/Checkout", model);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Something went wrong when calling api.");
        //    }
        //}
    }
}

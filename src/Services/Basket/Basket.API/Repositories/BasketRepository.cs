using Basket.API.Data.Context.Interfaces;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Cart> GetBasket(int userId)
        {
            return await _context.Baskets.Find(b => b.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Cart> AddToBasket(int userId, Product product)
        {
            var basket = await _context.Baskets.Find(b => b.UserId == userId).FirstOrDefaultAsync();
            if (basket == null)
            {
                basket.Items.Add(product);
                Save(basket);
            }

            return await GetBasket(basket.UserId);
        }

        public async Task DeleteFromBasket(int userId, int productId)
        {
            var basket = await _context.Baskets.Find(b => b.UserId == userId).FirstOrDefaultAsync();
            if(basket == null)
            {
                var productToDelete = basket.Items.Find(p=>p.Id==productId);
                basket.Items.Remove(productToDelete);
                Save(basket);
            }
            
        }
        private void Save(Cart basket)
        {
            _context.Baskets.ReplaceOne(x => x.UserId == basket.UserId, basket);
        }
    }
}

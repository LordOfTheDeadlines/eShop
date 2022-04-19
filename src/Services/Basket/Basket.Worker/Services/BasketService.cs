using Basket.Worker.Data.Context.Interfaces;
using Basket.Worker.Data.Models;
using Basket.Worker.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Basket.Worker.Services
{
    public class BasketService
    {
        private readonly ILogger<BasketService> _logger;
        private readonly IBasketContext _context;

        public BasketService(IBasketContext context, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BasketService>();

            _logger.LogInformation("Connecting to mongodb...");
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger.LogInformation("Connected to mongodb");
        }


        public void UpdateProductInBaskets(ProductModel product)
        {
            var item = Product.From(product);
            _logger.LogInformation($"Updating baskets with item {item.Id} - {item.Name}");
            var baskets = FindBasketsWithProduct(item);
            if (baskets.Count>0)
            {
                foreach (var basket in baskets){
                    var oldProduct = basket.Items.Find(p => p.Id == item.Id);
                    oldProduct.Name = item.Name;        
                    oldProduct.Price = item.Price;  
                    oldProduct.ImageUrl = item.ImageUrl;    
                    oldProduct.CategoryId = item.CategoryId;
                    Save(basket);
                }
            }
            else
            {
                Fail($"Item {item.Id} was not updated: buskets with this item don't exist");
            }
        }

        public void DeleteProductInBaskets(ProductModel product)
        {
            var item = Product.From(product);
            _logger.LogInformation($"Deleting item {item.Id} - {item.Name} from baskets");
            var baskets = FindBasketsWithProduct(item);
            if (baskets.Count>0)
            {
                foreach (var basket in baskets)
                {
                    var oldProduct = basket.Items.Find(p => p.Id == item.Id);
                    basket.Items.Remove(oldProduct);
                    Save(basket);
                }
            }
            else
            {
                Fail($"Item {item.Id} was not deleted: buskets with this item don't exist");
            }
        }

        private ICollection<Cart> FindBasketsWithProduct(Product product)
        {
            var filter = Builders<Cart>.Filter.ElemMatch(x=>x.Items,x => x.Id == product.Id);
            var baskets = _context.Baskets.Find(filter);
            return baskets.ToList();
        }

        private void Save(Cart basket)
        {
            _context.Baskets.ReplaceOne(x => x.UserId == basket.UserId, basket);
            _logger.LogInformation($"Category {basket.UserId} updated");
        }

        private void Fail(string message = "failed")
        {
            _logger.LogWarning(message);
        }
    }
}

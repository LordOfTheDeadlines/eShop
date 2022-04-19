using Basket.API.Data.Context.Interfaces;
using Basket.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Basket.API.Data.Context
{
    public class BasketContext:IBasketContext
    {
        public BasketContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Baskets = database.GetCollection<ShoppingCart>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

        }

        public IMongoCollection<ShoppingCart> Baskets { get; }
    }
}

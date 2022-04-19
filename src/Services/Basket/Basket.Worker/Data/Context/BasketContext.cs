using Basket.Worker.Data.Context.Interfaces;
using Basket.Worker.Data.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Basket.Worker.Data.Context
{
    public class BasketContext:IBasketContext
    {
        public BasketContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Baskets = database.GetCollection<Cart>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }

        public BasketContext()
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"));
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE"));

            Baskets = database.GetCollection<Cart>(Environment.GetEnvironmentVariable("MONGO_COLLECTION"));

        }

        public IMongoCollection<Cart> Baskets { get; }
    }
}

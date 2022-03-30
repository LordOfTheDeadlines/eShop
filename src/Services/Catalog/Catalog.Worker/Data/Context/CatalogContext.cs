using Catalog.Worker.Data.Context.Interfaces;
using Catalog.Worker.Data.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Catalog.Worker.Data.Context
{
    class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Catalog = database.GetCollection<CategoryAssortment>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }

        public CatalogContext()
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"));
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE"));

            Catalog = database.GetCollection<CategoryAssortment>(Environment.GetEnvironmentVariable("MONGO_COLLECTION"));

        }

        public IMongoCollection<CategoryAssortment> Catalog { get; }
    }
}

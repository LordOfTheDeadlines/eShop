using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Data.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data.Context
{
    class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Catalog = database.GetCollection<CategoryAssortment>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

        }

        public IMongoCollection<CategoryAssortment> Catalog { get; }
    }
}

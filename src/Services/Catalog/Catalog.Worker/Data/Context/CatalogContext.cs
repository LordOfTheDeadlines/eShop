using Catalog.Worker.Data.Context.Interfaces;
using Catalog.Worker.Data.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

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

        public IMongoCollection<CategoryAssortment> Catalog { get; }
    }
}

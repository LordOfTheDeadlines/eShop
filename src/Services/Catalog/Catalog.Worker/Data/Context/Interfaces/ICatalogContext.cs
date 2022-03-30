using Catalog.Worker.Data.Entities;
using MongoDB.Driver;

namespace Catalog.Worker.Data.Context.Interfaces
{
    interface ICatalogContext
    {
        IMongoCollection<CategoryAssortment> Catalog { get; }
    }
}

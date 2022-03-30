using Catalog.API.Data.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Context.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<CategoryAssortment> Catalog { get; }
    }
}

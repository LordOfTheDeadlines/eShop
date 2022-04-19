using Basket.Worker.Data.Entities;
using MongoDB.Driver;

namespace Basket.Worker.Data.Context.Interfaces
{
    public interface IBasketContext
    {
        IMongoCollection<Cart> Baskets { get; }
    }
}

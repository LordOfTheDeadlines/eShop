using Basket.API.Entities;
using MongoDB.Driver;

namespace Basket.API.Data.Context.Interfaces
{
    public interface IBasketContext
    {
        IMongoCollection<Cart> Baskets { get; }
    }
}

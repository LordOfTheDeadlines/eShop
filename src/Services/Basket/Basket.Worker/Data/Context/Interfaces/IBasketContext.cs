using Basket.Worker.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Worker.Data.Context.Interfaces
{
    public interface IBasketContext
    {
        IMongoCollection<ShoppingCart> Baskets { get; }
    }
}

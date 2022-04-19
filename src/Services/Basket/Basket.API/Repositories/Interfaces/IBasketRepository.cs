using Basket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<Cart> GetBasket(int userId);
        Task<Cart> AddToBasket(int userId, Product product);
        Task DeleteFromBasket(int userId, int productId);
    }
}

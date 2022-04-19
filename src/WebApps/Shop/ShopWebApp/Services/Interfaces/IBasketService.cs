using ShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Cart> GetBasket(int userId);
        Task<Cart> AddToBasket(int userId, int productId);
        Task DeleteFromBasket(int userId, int productId);
    }
}

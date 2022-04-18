using ShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Cart> GetBasket(string userName);
        Task<Cart> UpdateBasket(Cart model);
        //Task CheckoutBasket(BasketCheckoutModel model);
    }
}

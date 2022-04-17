using ShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetBasket(string userName);
        Task<Basket> UpdateBasket(Basket model);
        //Task CheckoutBasket(BasketCheckoutModel model);
    }
}

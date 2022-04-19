using Basket.Worker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Worker.Repositories.Interfaces
{
    interface IBasketRepository
    {
        Task<Data.Entities.Cart> GetBasket(string userName);
        Task<Data.Entities.Cart> UpdateBasket(Data.Entities.Cart basket);
        Task DeleteBasket(string userName);
    }
}

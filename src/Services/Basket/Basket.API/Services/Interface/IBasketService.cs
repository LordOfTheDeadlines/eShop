using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Services.Interface
{
    public interface IBasketService
    {
        Task<Cart> GetBasket(int userId);
        Task<Cart> AddToBasket(int userId, int productId);
        Task DeleteFromBasket(int userId, int productId);
    }
}

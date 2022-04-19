using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Basket.API.Services.Interface;
using System;
using System.Threading.Tasks;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;

        public BasketService(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<Cart> AddToBasket(int userId, int productId)
        {
            //return  _repository.AddToBasket(userId, )
            return null;
        }

        public Task DeleteFromBasket(int userId, int productId)
        {
            return _repository.DeleteFromBasket(userId, productId);       
        }

        public Task<Cart> GetBasket(int userId)
        {
            return _repository.GetBasket(userId);   
        }
    }
}

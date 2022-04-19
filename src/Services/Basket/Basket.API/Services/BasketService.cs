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
        private readonly IProductService _productService;

        public BasketService(IBasketRepository repository, IProductService productService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public Task<Cart> AddToBasket(int userId, int productId)
        {
            var product = _productService.GetProduct(productId).Result;
            return _repository.AddToBasket(userId, product);
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

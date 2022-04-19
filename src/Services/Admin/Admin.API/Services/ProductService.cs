using Admin.API.Data;
using Admin.API.Data.Entites;
using Admin.API.RabbitMQ;
using Admin.API.Repository.Interfaces;
using Admin.API.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Admin.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly RabbitService _rabbitMq;
        private readonly IProductRepository _repository;

        public ProductService(ILogger<ProductService> logger, RabbitService rabbitMq, IProductRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _rabbitMq = rabbitMq;
        }

        public Task<bool> CreateProduct(Product product)
        {
            _logger.LogDebug("Creating product...");
            var result = _repository.CreateProduct(product);
            if (result.Result)
            {
                _logger.LogDebug("Product was created");

                _rabbitMq.Publish("products", JsonSerializer.Serialize(ProductMessage.CreateAdd(product)));
            }

            return result;
        }

        public Task<bool> DeleteProduct(int id)
        {
            _logger.LogDebug("Deleting product...");
            var deletedProduct = _repository.GetProduct(id);
            var result = _repository.DeleteProduct(id);
            if (result.Result)
            {
                _logger.LogDebug("Product was deleted");
                _rabbitMq.Publish("products", JsonSerializer.Serialize(ProductMessage.CreateDelete(deletedProduct.Result)));
                _rabbitMq.Publish("basket", JsonSerializer.Serialize(BasketMessage.CreateDelete(deletedProduct.Result)));
            }
            return result;
        }

        public Task<Product> GetProduct(int id)
        {
            _logger.LogDebug("Get product by id = " + id);
            return _repository.GetProduct(id);
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            _logger.LogDebug("Get products");
            return _repository.GetProducts();
        }

        public Task<bool> UpdateProduct(Product product)
        {
            _logger.LogDebug("Updating product");
            var result = _repository.UpdateProduct(product);
            if (result.Result)
            {
                _logger.LogDebug("Product was updated");
                var updatedProduct = _repository.GetProduct(product.Id);
                _rabbitMq.Publish("products", JsonSerializer.Serialize(ProductMessage.CreateUpdate(updatedProduct.Result)));
                _rabbitMq.Publish("basket", JsonSerializer.Serialize(BasketMessage.CreateUpdate(updatedProduct.Result)));
            }

            return result;
        }
    }
}

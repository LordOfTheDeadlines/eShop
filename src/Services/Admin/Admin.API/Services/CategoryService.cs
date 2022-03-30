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
    public class CategoryService:ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly RabbitService _rabbitMq;
        private readonly ICategoryRepository _repository;

        public CategoryService(ILogger<CategoryService> logger, RabbitService rabbitMq, ICategoryRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _rabbitMq = rabbitMq;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        public Task<IEnumerable<Category>> GetCategories()
        {
            _logger.LogDebug("Get categories");
            return _repository.GetCategories();
        }

        public Task<Category> GetCategory(int id)
        {
            _logger.LogDebug("Get category by id = " + id);
            return _repository.GetCategory(id);
        }

        public Task<bool> CreateCategory(Category category)
        {
            _logger.LogDebug("Creating category...");
            var result = _repository.CreateCategory(category);
            if (result.Result)
            {
                _logger.LogDebug("Category was created");

                _rabbitMq.Publish("categories", JsonSerializer.Serialize(CategoryMessage.CreateAdd(category)));

            }

            return result;
        }

        public Task<bool> UpdateCategory(Category category)
        {
            _logger.LogDebug("Updating category");
            var result = _repository.UpdateCategory(category);
            if (result.Result)
            {
                _logger.LogDebug("Category was updated");
                var updatedCategory = _repository.GetCategory(category.Id);
                _rabbitMq.Publish("categories", JsonSerializer.Serialize(CategoryMessage.CreateUpdate(updatedCategory.Result)));

            }

            return result;
        }

        public Task<bool> DeleteCategory(int id)
        {
            _logger.LogDebug("Deleting category...");
            var deletedCategory = _repository.GetCategory(id);
            var result = _repository.DeleteCategory(id);
            if (result.Result)
            {
                _logger.LogDebug("Category was deleted");
                _rabbitMq.Publish("categories", JsonSerializer.Serialize(CategoryMessage.CreateDelete(deletedCategory.Result)));
            }
            return result;
        }
    }
}

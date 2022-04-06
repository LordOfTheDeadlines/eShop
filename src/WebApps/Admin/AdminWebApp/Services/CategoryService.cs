using AdminWebApp.Extensions;
using AdminWebApp.Models;
using AdminWebApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminWebApp.Services
{
    public class CategoryService:ICategoryService
    {

        private readonly HttpClient _client;

        public CategoryService(HttpClient client, ILogger<CategoryService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Category> CreateCategory(Category model)
        {
            var response = await _client.PostAsJson($"/api/v1/Category", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<Category>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
        public async Task<bool> DeleteCategory(Category model)
        {
            var response = await _client.DeleteAsync($"/api/v1/Category/{model.Id}");
            return await response.ReadContentAs<bool>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var response = await _client.GetAsync("/api/v1/Category");
            return await response.ReadContentAs<List<Category>>();
        }

        public async Task<Category> GetCategory(int id)
        {
            var response = await _client.GetAsync($"/api/v1/Category/{id}");
            return await response.ReadContentAs<Category>();
        }

        public async Task<bool> UpdateCategory(Category model)
        {
            var response = await _client.PutAsJson($"/api/v1/Category", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}

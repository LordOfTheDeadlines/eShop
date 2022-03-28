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

        public async Task<CategoryModel> CreateCategory(CategoryModel model)
        {
            var response = await _client.PostAsJson($"/Category", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CategoryModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
        public Task<CategoryModel> DeleteCategory(CategoryModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            var response = await _client.GetAsync("/api/v1/Category");
            return await response.ReadContentAs<List<CategoryModel>>();
        }

        public async Task<CategoryModel> GetCategory(int id)
        {
            var response = await _client.GetAsync($"/Category/{id}");
            return await response.ReadContentAs<CategoryModel>();
        }

        public async Task<CategoryModel> UpdateCategory(CategoryModel model)
        {
            var response = await _client.PostAsJson($"/Category", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CategoryModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}

using AdminWebApp.Extensions;
using AdminWebApp.Models;
using AdminWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AdminWebApp.Services
{
    public class CategoryService:ICategoryService
    {

        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _context;

        public CategoryService(HttpClient client, IHttpContextAccessor context, ILogger<CategoryService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Category> CreateCategory(Category model)
        {
            var response = await _client.PostAsJson(_context,$"/api/v1/Category", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<Category>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
        public async Task<bool> DeleteCategory(Category model)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _context.HttpContext.Request.Cookies["Authorization"]);
            var response = await _client.DeleteAsync($"/api/v1/Category/{model.Id}");
            return await response.ReadContentAs<bool>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _context.HttpContext.Request.Cookies["Authorization"]);
            var response = await _client.GetAsync("/api/v1/Category");
            return await response.ReadContentAs<List<Category>>();
        }

        public async Task<Category> GetCategory(int id)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _context.HttpContext.Request.Cookies["Authorization"]);
            var response = await _client.GetAsync($"/api/v1/Category/{id}");
            return await response.ReadContentAs<Category>();
        }

        public async Task<bool> UpdateCategory(Category model)
        {
            var response = await _client.PutAsJson(_context,$"/api/v1/Category", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}

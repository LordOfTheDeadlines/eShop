using AdminWebApp.Extensions;
using AdminWebApp.Models;
using AdminWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AdminWebApp.Services
{
    public class ProductService:IProductService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _context;

        public ProductService(HttpClient client, IHttpContextAccessor context, ILogger<ProductService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Product> CreateProduct(Product model)
        {
            var response = await _client.PostAsJson(_context,$"/api/v1/Product", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<Product>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
        public async Task<bool> DeleteProduct(int id)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _context.HttpContext.Request.Cookies["Authorization"]);
            var response = await _client.DeleteAsync($"/api/v1/Product/{id}");
            return await response.ReadContentAs<bool>();
        }
        public async Task<Product> GetProduct(int id)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _context.HttpContext.Request.Cookies["Authorization"]);
            var response = await _client.GetAsync($"/api/v1/Product/{id}");
            return await response.ReadContentAs<Product>();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _context.HttpContext.Request.Cookies["Authorization"]);
            var response = await _client.GetAsync("/api/v1/Product");
            return await response.ReadContentAs<List<Product>>();
        }
        public async Task<bool> UpdateProduct(Product model)
        {
            var response = await _client.PutAsJson(_context,$"/api/v1/Product", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}

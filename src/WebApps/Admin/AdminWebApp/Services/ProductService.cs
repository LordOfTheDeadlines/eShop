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
    public class ProductService:IProductService
    {
        private readonly HttpClient _client;

        public ProductService(HttpClient client, ILogger<ProductService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = await _client.PostAsJson($"/api/v1/Product", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
        public async Task<ProductModel> DeleteProduct(int id)
        {
            var response = await _client.DeleteAsync($"/api/v1/Product/{id}");
            return await response.ReadContentAs<ProductModel>();
        }
        public async Task<ProductModel> GetProduct(int id)
        {
            var response = await _client.GetAsync($"/api/v1/Product/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            var response = await _client.GetAsync("/api/v1/Product");
            return await response.ReadContentAs<List<ProductModel>>();
        }
        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _client.PostAsJson($"/api/v1/Product", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}

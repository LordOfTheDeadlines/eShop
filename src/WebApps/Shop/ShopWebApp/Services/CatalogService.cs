using Microsoft.Extensions.Logging;
using ShopWebApp.Extentions;
using ShopWebApp.Models;
using ShopWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopWebApp.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client, ILogger<CatalogService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<CategoryAssortment>> GetCatalog()
        {
            var response = await _client.GetAsync("/api/v1/Catalog");
            return await response.ReadContentAs<List<CategoryAssortment>>();
        }

        public async Task<CategoryAssortment> GetCategoryAssortment(int id)
        {
            var response = await _client.GetAsync($"/api/v1/Catalog/{id}");
            return await response.ReadContentAs<CategoryAssortment>();
        }

        public async Task<Product> GetProductDetails(int id)
        {
            var response = await _client.GetAsync($"/api/v1/Catalog/Product/{id}");
            return await response.ReadContentAs<Product>();
        }
    }
}

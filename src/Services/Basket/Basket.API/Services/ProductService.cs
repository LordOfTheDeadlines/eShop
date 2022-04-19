using Basket.API.Entities;
using Basket.API.Services.Interface;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.API.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<Product> GetProduct(int id)
        {
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _context.HttpContext.Request.Cookies["Authorization"]);
            var response = await _client.GetAsync($"/api/v1/Product/{id}");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<Product>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}

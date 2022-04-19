using Basket.API.Data.Entities;
using Basket.API.Entities;
using Basket.API.Services.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.API.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration; 

        public ProductService(HttpClient client, IConfiguration configuration)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<Product> GetProduct(int id)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.GetSection("Secret").Value);
            var response = await _client.GetAsync($"/api/v1/Product/{id}");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var producrModel = JsonSerializer.Deserialize<ProductModel>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return Product.From(producrModel);
        }
    }
}

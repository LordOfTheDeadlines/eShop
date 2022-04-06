using ShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<CategoryAssortment>> GetCatalog();
        Task<CategoryAssortment> GetCategoryAssortment(int id);
        Task<Product> GetProductDetails(int categoryId, int productId);
    }
}

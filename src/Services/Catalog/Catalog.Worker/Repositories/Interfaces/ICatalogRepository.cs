using Catalog.Worker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Worker.Repositories.Interfaces
{
    interface ICatalogRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);

        Task<bool> CreateCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);

        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);

        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}

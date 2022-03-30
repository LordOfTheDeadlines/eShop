using Catalog.Worker.Data;
using Catalog.Worker.Data.Entities;
using Catalog.Worker.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Worker.Repositories
{
    class CatalogRepository : ICatalogRepository
    {
        private readonly IMongoCollection<CategoryAssortment> _context;

        public CatalogRepository(IMongoCollection<CategoryAssortment> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<bool> CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}

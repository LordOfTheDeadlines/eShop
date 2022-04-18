using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Data.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class CatalogRepository:ICatalogRepository
    {
        private readonly ICatalogContext _context;

        public CatalogRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Product> GetProduct(int productId)
        {
            var catAssort = await _context.Catalog.Find(c =>
                c.Items.Any(i => i.Id == productId)).FirstAsync();
            return catAssort.Items.Find(i => i.Id == productId);
        }

        public async Task<CategoryAssortment> GetCategoryAssortment(int id)
        {
            return await _context.Catalog.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CategoryAssortment>> GetCatalog()
        {
            return await _context.Catalog.Find(c => true).ToListAsync();
        }

    }
}

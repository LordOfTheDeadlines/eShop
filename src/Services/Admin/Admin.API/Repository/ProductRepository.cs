using Admin.API.Data.Context;
using Admin.API.Data.Entites;
using Admin.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AdminDB _context;

        public ProductRepository(AdminDB context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products
                .Where(i => i.Id == id)
                .Include(i => i.Category)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products
                .Include(i => i.Category)
                .ToListAsync();
        }

        public async Task<bool> CreateProduct(Product product)
        {
            _context.Add(product);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var productToDelete = await _context.Products
                .FindAsync(id);
            if (productToDelete != null)
                _context.Products.Remove(productToDelete);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var productToUpdate = await _context
                .Products
                .FirstOrDefaultAsync(s => s.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.ImageUrl = product.ImageUrl;
                productToUpdate.Category = _context.Categories
                    .Where(c => c.Id == product.CategoryId)
                    .FirstOrDefault();

            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}

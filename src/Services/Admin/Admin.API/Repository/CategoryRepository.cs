using Admin.API.Data;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AdminDB _context;

        public CategoryRepository(AdminDB context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateCategory(Category category)
        {
            _context.Add(category);
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

        public async Task<bool> DeleteCategory(int id)
        {
            var categoryToDelete = await _context
                .Categories
                .Where(c => c.Id==id)
                .Include(c => c.Parent)
                .FirstOrDefaultAsync();

            _context.Categories
                .Where(c => c.Parent != null && c.Parent.Id == id).ToList()
                .ForEach(c => c.Parent = categoryToDelete.Parent);


            _context.Products
                .Where(i => i.Category != null && i.Category.Id == id).ToList()
                .ForEach(i => i.Category = null);

            _context.Categories.Remove(categoryToDelete);

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

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var categoryToUpdate = await _context
                .Categories
                .FirstOrDefaultAsync(s => s.Id == category.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                if (category.Parent == null)
                    categoryToUpdate.Parent = null;
                else
                    categoryToUpdate.Parent = await GetCategory(category.Parent.Id);

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

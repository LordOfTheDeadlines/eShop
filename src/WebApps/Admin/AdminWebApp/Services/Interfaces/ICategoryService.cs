using AdminWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<Category> CreateCategory(Category model);
        Task<bool> UpdateCategory(Category model);
        Task<bool> DeleteCategory(Category model);
    }
}

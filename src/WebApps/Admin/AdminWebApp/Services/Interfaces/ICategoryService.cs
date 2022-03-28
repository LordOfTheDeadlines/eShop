using AdminWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetCategories();
        Task<CategoryModel> GetCategory(int id);
        Task<CategoryModel> CreateCategory(CategoryModel model);
        Task<CategoryModel> UpdateCategory(CategoryModel model);
        Task<CategoryModel> DeleteCategory(CategoryModel model);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApp.Models;
using AdminWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminWebApp.Pages
{
    public class CategoriesModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CategoriesModel(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public IEnumerable<Category> CategoryList { get; set; } = new List<Category>();


        public async Task OnGetAsync()
        {
            CategoryList = await _categoryService.GetCategories();
        }
    }
}

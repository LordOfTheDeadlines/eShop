using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApp.Models;
using AdminWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminWebApp.Pages.Categories
{
    public class CreateModel : PageModel
    {

        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public SelectList Categories { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _categoryService.GetCategories();
            Categories = new SelectList(categories, nameof(CategoryModel.Id), nameof(CategoryModel.Name));
            return Page();
        }

        [BindProperty]
        public CategoryModel Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _categoryService.CreateCategory(Category);

            return RedirectToPage("./Index");
        }
    }
}

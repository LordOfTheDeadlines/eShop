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
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private Category category;

        public EditModel(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public SelectList Categories { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var categories = await _categoryService.GetCategories();
            category = await _categoryService.GetCategory(id);

            Categories = new SelectList(categories, nameof(CategoryModel.Id), nameof(CategoryModel.Name));
            return Page();
        }

        [BindProperty]
        public CategoryModel CategoryMod { get; set; }

        public async Task<IActionResult> OnPutAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var parent = _categoryService.GetCategory(category.Id).Result;
            await _categoryService.UpdateCategory(new Category(CategoryMod.Name, parent, parent.Id));

            return RedirectToPage("./Index");
        }
    }
}

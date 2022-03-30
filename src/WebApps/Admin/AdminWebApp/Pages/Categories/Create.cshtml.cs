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
        public IEnumerable<Category> CategoryList { get; set; } = new List<Category>();
        public async Task<IActionResult> OnGetAsync()
        {
            CategoryList = await _categoryService.GetCategories();
            Categories = new SelectList(CategoryList, nameof(Category.Id), nameof(Category.Name));
            return Page();
        }

        [BindProperty]
        public Category CategoryMod { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _categoryService.CreateCategory(new Category(CategoryMod.Name, CategoryMod.ParentId));

            return RedirectToPage("./Index");
        }
    }
}
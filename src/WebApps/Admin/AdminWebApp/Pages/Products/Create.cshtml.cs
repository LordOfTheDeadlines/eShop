using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApp.Models;
using AdminWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminWebApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public CreateModel(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
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
        public ProductModel ProductMod { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productService.CreateProduct(new ProductModel(ProductMod.Id, ProductMod.Name, ProductMod.Price, ProductMod.ImageUrl, ProductMod.Category, ProductMod.CategoryId));

            return RedirectToPage("./Index");
        }
    }
}

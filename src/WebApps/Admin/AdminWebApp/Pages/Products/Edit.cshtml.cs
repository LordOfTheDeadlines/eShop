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
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private Product productToUpdate;

        public EditModel(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public SelectList Categories { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var categories = await _categoryService.GetCategories();

            Categories = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
            return Page();
        }

        [BindProperty]
        public Product ProductMod { get; set; }

        public async Task<IActionResult> OnPutAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ProductMod.CategoryId != null)
            {
                await _productService.UpdateProduct(new Product(ProductMod.Id, ProductMod.Name, ProductMod.Price, ProductMod.ImageUrl, ProductMod.Category, ProductMod.CategoryId));
            }
            else
                await _productService.UpdateProduct(new Product(ProductMod.Id, ProductMod.Name, ProductMod.Price, ProductMod.ImageUrl));

            return RedirectToPage("./Index");
        }
    }
}

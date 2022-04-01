using AdminWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminWebApp.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProductService _productService;

        public ProductsModel(IProductService categoryService)
        {
            _productService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public IEnumerable<Models.ProductModel> ProductList { get; set; } = new List<Models.ProductModel>();


        public async Task OnGetAsync()
        {
            ProductList = await _productService.GetProducts();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _productService.DeleteProduct(id);
            return RedirectToPage();
        }
    }
}

using AdminWebApp.Models;
using AdminWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        // GET: ProductController
        public async Task<ActionResult> IndexAsync()
        {
            return View(await _productService.GetProducts());
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            return View(await _productService.GetProduct(id));
        }

        // GET: ProductController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var categoryList = await _categoryService.GetCategories();
            var selectList = new SelectList(categoryList, nameof(Category.Id), nameof(Category.Name));
            ViewBag.Categories = selectList;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Name,CategoryId,Price,ImageUrl")]Product product)
        {
            try
            {
                await _productService.CreateProduct(new Product(product.Name, product.CategoryId, product.Price, product.ImageUrl));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var product = await _productService.GetProduct(id);
            var categoryList = await _categoryService.GetCategories();
            var selectList = new SelectList(categoryList, nameof(Category.Id), nameof(Category.Name));
            ViewBag.Categories = selectList;
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, [Bind("Id,Name,CategoryId,Price,ImageUrl")] Product product)
        {
            try
            {
                await _productService.UpdateProduct(new Product(id, product.Name, product.CategoryId, product.Price, product.ImageUrl));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _productService.GetProduct(id));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(await _productService.GetProduct(id));
            }
        }
    }
}

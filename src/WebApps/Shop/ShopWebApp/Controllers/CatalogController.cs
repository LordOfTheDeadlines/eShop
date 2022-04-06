using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Controllers
{
    public class CatalogController : Controller
    {

        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService??throw new ArgumentNullException(nameof(catalogService));
        }

        // GET: CatalogController
        public async Task<ActionResult> IndexAsync()
        {
            return View(await _catalogService.GetCatalog());
        }

        // GET: CatalogController/CategoryAssortment/5
        public async Task<ActionResult> CategoryAssortmentAsync(int id)
        {
            return View(await _catalogService.GetCategoryAssortment(id));
        }

        public async Task<ActionResult> ProductDetails(int categoryId, int productId)
        {
            var product = await _catalogService.GetProductDetails(categoryId, productId);
            return View(product);
        }
    }
}

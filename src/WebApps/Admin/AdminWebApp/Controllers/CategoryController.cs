using AdminWebApp.Models;
using AdminWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }
        public async Task LogTokenAndClaims()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }

        // GET: CategoryController
        public async Task<ActionResult> IndexAsync()
        {
            //await LogTokenAndClaims();
            return View(await _categoryService.GetCategories());
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            return View(await _categoryService.GetCategory(id));
        }

        // GET: CategoryController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var categoryList = await _categoryService.GetCategories();
            var selectList = new SelectList(categoryList, nameof(Category.Id), nameof(Category.Name));
            ViewBag.Categories = selectList;
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Name,ParentId")]Category category)
        {
            try
            {
                await _categoryService.CreateCategory(new Category(category.Name, category.ParentId));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var category =  await _categoryService.GetCategory(id);
            var categoryList = await _categoryService.GetCategories();
            var selectList = new SelectList(categoryList, nameof(Category.Id), nameof(Category.Name));
            ViewBag.Categories = selectList;
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, [Bind("Id,Name,ParentId")] Category category)
        {
            try
            {
                await _categoryService.UpdateCategory(new Category(id, category.Name, category.ParentId));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _categoryService.GetCategory(id));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategory(id);
            try
            {
                await _categoryService.DeleteCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }
    }
}

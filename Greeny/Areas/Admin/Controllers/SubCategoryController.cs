using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Admin.SubCategory;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;

        public SubCategoryController(ISubCategoryService subCategoryService,
                                     ICategoryService categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;

        }

        public async Task<IActionResult> Index()
        {
            var subCategories = await _subCategoryService.GetAllWithIncludesAsync();

            return View(subCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await GetCategories();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _subCategoryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<SelectList> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            await _subCategoryService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var subCategory = await _subCategoryService.GetByIdWithIncludesAsync((int)id);

            if (subCategory is null) return NotFound();

            ViewBag.categories = await GetCategories();

            SubCategoryEditVM model = new()
            {
                CategoryId = subCategory.CategoryId,
                Name = subCategory.Name,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategoryEditVM request)
        {
            ViewBag.categories = await GetCategories();

            var subCategory = await _subCategoryService.GetByIdWithIncludesAsync(id);

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage);
                foreach (var item in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(request);
            }

            await _subCategoryService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
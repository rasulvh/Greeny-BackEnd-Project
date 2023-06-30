using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;
using System.Diagnostics;

namespace Greeny.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IBlogService _blogService;

        public HomeController(ISliderService sliderService,
                              IProductService productService,
                              ICategoryService categoryService,
                              IBrandService brandService,
                              IBlogService blogService)
        {
            _sliderService = sliderService;
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var products = await _productService.GetAllWithIncludesAsync();
            var categories = await _categoryService.GetAllWithIncludesAsync();
            var brands = await _brandService.GetAllWithIncludesAsync();
            var blogs = await _blogService.GetAllAsync();

            int count = products.Count();

            ViewBag.count = count;

            HomeVM model = new()
            {
                Sliders = sliders.Where(m => m.Status).ToList(),
                Products = products.Where(m => !m.SoftDelete).ToList().Take(5),
                Categories = categories.Where(m => !m.SoftDelete).ToList(),
                Brands = brands.Where(m => !m.SoftDelete).ToList(),
                Blogs = blogs.Where(m => !m.SoftDelete).Take(5).ToList(),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ShowMoreOrLess(int skip)
        {
            IEnumerable<Product> products = await _productService.GetAllWithIncludesAsync();
            return PartialView("_ProductsPartial", products.Where(m => !m.SoftDelete).Skip(skip).Take(5));
        }
    }
}
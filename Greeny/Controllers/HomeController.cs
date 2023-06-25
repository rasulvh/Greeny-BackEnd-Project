using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ISliderService sliderService,
                              IProductService productService,
                              ICategoryService categoryService,
                              IBrandService brandService)
        {
            _sliderService = sliderService;
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var products = await _productService.GetAllWithIncludesAsync();
            var categories = await _categoryService.GetAllWithIncludesAsync();
            var brands = await _brandService.GetAllWithIncludesAsync();

            HomeVM model = new()
            {
                Sliders = sliders.Where(m => m.Status).ToList(),
                Products = products.Where(m => !m.SoftDelete).ToList(),
                Categories = categories.Where(m => !m.SoftDelete).ToList(),
                Brands = brands.Where(m => !m.SoftDelete).ToList(),
            };

            return View(model);
        }
    }
}
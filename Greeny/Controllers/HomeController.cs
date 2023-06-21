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

        public HomeController(ISliderService sliderService,
                              IProductService productService)
        {
            _sliderService = sliderService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var products = await _productService.GetAllWithIncludesAsync();

            HomeVM model = new()
            {
                Sliders = sliders.Where(m => m.Status).ToList(),
                Products = products.Where(m => !m.SoftDelete).ToList()
            };

            return View(model);
        }
    }
}
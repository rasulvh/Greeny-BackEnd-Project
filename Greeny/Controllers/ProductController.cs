using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;

namespace Greeny.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _productService.GetByIdWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            var categoryId = product.Category.Id;

            var relatedProducts = await _productService.GetAllWithIncludesAsync();

            ProductDetailPageVM model = new()
            {
                Product = product,
                RelatedProducts = relatedProducts.Where(m => m.CategoryId == categoryId).ToList()
            };

            return View(model);
        }
    }
}

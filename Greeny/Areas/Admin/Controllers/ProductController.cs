using Greeny.Areas.Admin.ViewModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers;
using ServiceLayer.Services.Interfaces;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithIncludesAsync();

            List<ProductVM> model = new();

            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Brand = product.Brand.Name,
                    Category = product.Category.Name,
                    Discount = product.Discount.Name,
                    Image = product.Images.FirstOrDefault(m => m.IsMain).Image,
                    Name = product.Name,
                    Price = product.Price,
                    StockCount = product.StockCount,
                });
            }

            return View(model);
        }
    }
}

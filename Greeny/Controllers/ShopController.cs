using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;

namespace Greeny.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ShopController(IProductService productService,
                              ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string searchText = null)
        {
            List<ProductShopPageVM> list = new();

            var products = await _productService.GetAllWithSomeIncludesAsync();
            var categories = await _categoryService.GetAllWithIncludesAsync();

            if (searchText != null)
            {
                searchText = searchText.ToLower();
                var searchedProducts = await _productService.GetSearchedBlogs(searchText);
                foreach (var item in searchedProducts)
                {
                    list.Add(new ProductShopPageVM
                    {
                        Discount = item.Discount.Percent,
                        Image = item.Images.FirstOrDefault(m => m.IsMain).Image,
                        Name = item.Name,
                        Price = item.Price,
                        Rating = item.Rating.RatingCount,
                        ReviewCount = item.Reviews.Count
                    });
                }
            }
            else
            {
                foreach (var item in products)
                {
                    list.Add(new ProductShopPageVM
                    {
                        Discount = item.Discount.Percent,
                        Image = item.Images.FirstOrDefault(m => m.IsMain).Image,
                        Name = item.Name,
                        Price = item.Price,
                        Rating = item.Rating.RatingCount,
                        ReviewCount = item.Reviews.Count
                    });
                }
            }

            ShopVM model = new()
            {
                Products = list,
                Categories = categories.ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> FilterByCategory(int categoryId)
        {
            var products = await _productService.GetAllWithSomeIncludesAsync();
            var filteredProducts = products.Where(m => m.CategoryId == categoryId);

            return Json(filteredProducts);
        }
    }
}

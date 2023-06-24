using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;

namespace Greeny.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;

        public CartController(IHttpContextAccessor accessor,
                              IProductService productService)
        {
            _accessor = accessor;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync(id);

            if (product is null) return NotFound();

            List<BasketVM> basket = _basketService.GetAll();

            _basketService.AddProduct(basket, product);

            return Ok(basket.Sum(m => m.Count));
        }
    }
}

using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;
using System.Collections.Generic;
using System.Text.Json;

namespace Greeny.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public CartController(IHttpContextAccessor accessor,
                              IProductService productService,
                              IBasketService basketService)
        {
            _accessor = accessor;
            _productService = productService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            //var products = JsonSerializer.Deserialize<List<Product>>(_accessor.HttpContext.Session.GetString("basket"));

            //List<BasketDetailVM> model = new();

            //foreach (var item in products)
            //{
            //    model.Add(new BasketDetailVM
            //    {
            //        Id = item.Id,
            //        Count = 1,
            //        Price = item.Price,
            //        Discount = item.Discount.Percent,
            //        Name = item.Name,
            //        Image = item.Images.FirstOrDefault(m => !m.SoftDelete && m.IsMain).Image,
            //        TotalPrice =
            //    });
            //}

            return View();
        }
    }
}

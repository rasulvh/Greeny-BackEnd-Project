using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;

        public BasketService(IBasketRepository basketRepository,
                             IHttpContextAccessor accessor,
                             IProductService productService)
        {
            _basketRepository = basketRepository;
            _accessor = accessor;
            _productService = productService;
        }

        public async Task CreateAsync(AppUser user)
        {
            Basket basket = new();

            basket.AppUserId = user.Id;

            await _basketRepository.CreateAsync(basket);
        }

        public async Task<Basket> GetBasketByUserIdAsync(string userId)
        {
            var baskets = await _basketRepository.FindByConditionAsync(m => m.AppUserId == userId);
            var basket = baskets.FirstOrDefault();
            return basket;
        }

        public void AddProductToBasket(List<BasketVM> basket, Product product)
        {
            BasketVM existProduct = basket.FirstOrDefault(m => m.Id == product.Id);

            if (existProduct is null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }

            _accessor.HttpContext.Session.SetString("basket", JsonSerializer.Serialize(basket));
        }

        public List<BasketVM> GetBasketDatas()
        {
            List<BasketVM> basket;

            if (_accessor.HttpContext.Session.GetString("basket") != null)
            {
                basket = JsonSerializer.Deserialize<List<BasketVM>>(_accessor.HttpContext.Session.GetString("basket"));
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }
    }
}

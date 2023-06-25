using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProductBasketService : IProductBasketService
    {
        private readonly IProductBasketRepository _productBasketRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IBasketService _basketService;

        public ProductBasketService(IProductBasketRepository productBasketRepository,
                                    IHttpContextAccessor accessor,
                                    IBasketService basketService)
        {
            _productBasketRepository = productBasketRepository;
            _accessor = accessor;
            _basketService = basketService;

        }

        public async Task AddProductToBasketAsync(List<ProductBasket> productBaskets)
        {
            var userId = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var basket = await _basketService.GetBasketByUserIdAsync(userId);

            foreach (var productBasket in productBaskets)
            {
                if (productBasket.BasketId == basket.Id)
                {
                    var existingProductBasket = await _productBasketRepository
                    .GetByExpressionForPivotTable(m => m.BasketId == basket.Id && m.ProductId == productBasket.ProductId);

                    if (existingProductBasket != null)
                    {
                        existingProductBasket.ProductCount = productBasket.ProductCount;
                    }
                    else
                    {
                        var newProductBasket = new ProductBasket
                        {
                            BasketId = basket.Id,
                            ProductId = productBasket.ProductId,
                            ProductCount = productBasket.ProductCount
                        };

                        await _productBasketRepository.CreateAsync(newProductBasket);
                    }
                }
            }
        }

        public async Task<BasketVM> GetAllProductByBasket()
        {
            var userId = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var basket = await _basketService.GetBasketByUserIdAsync(userId);
            var basketProduct = await _productBasketRepository.GetByBasketIdAsync(basket.Id);

            BasketVM basketVM = new();
            basketVM.Id = basketProduct.ProductId;
            basketVM.Count = basketProduct.ProductCount;

            return basketVM;
        }
    }
}

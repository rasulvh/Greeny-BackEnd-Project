using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}

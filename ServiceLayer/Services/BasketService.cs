using DomainLayer.Models;
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

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task CreateAsync(AppUser user)
        {
            Basket basket = new();

            basket.AppUserId = user.Id;

            await _basketRepository.CreateAsync(basket);
        }
    }
}

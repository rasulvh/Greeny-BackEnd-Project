using DomainLayer.Models;
using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IBasketService
    {
        Task CreateAsync(AppUser user);
        List<BasketVM> GetAll();
        void AddProduct(List<BasketVM> basket, Product product);
    }
}

using DomainLayer.Models;
using RepositoryLayer.Repositories;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Admin.Discount;
using ServiceLayer.ViewModels.Admin.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            return await _discountRepository.GetAllAsync();
        }

        public async Task CreateAsync(DiscountCreateVM request)
        {
            Discount discount = new()
            {
                Name = request.Name,
                Percent = request.Percent
            };

            await _discountRepository.CreateAsync(discount);
        }

        public async Task<Discount> GetByIdAsync(int id)
        {
            return await _discountRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);
            await _discountRepository.DeleteAsync(discount);
        }

        public async Task EditAsync(int id, DiscountEditVM request)
        {
            var discount = await _discountRepository.GetByIdAsync(id);

            discount.Percent = request.Percent;
            discount.Name = request.Name;

            await _discountRepository.UpdateAsync(discount);
        }

    }
}

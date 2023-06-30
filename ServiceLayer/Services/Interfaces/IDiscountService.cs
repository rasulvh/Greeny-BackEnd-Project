using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetAllAsync();
        Task CreateAsync(DiscountCreateVM request);
        Task<Discount> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, DiscountEditVM request);
    }
}

using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllWithIncludesAsync();
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdWithIncludesAsync(int id);
    }
}

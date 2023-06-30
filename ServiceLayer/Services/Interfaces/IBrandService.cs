using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<IEnumerable<Brand>> GetAllWithIncludesAsync();
        Task CreateAsync(BrandCreateVM request);
        Task DeleteAsync(int id);
        Task<Brand> GetByIdAsync(int id);
        Task EditAsync(int categoryId, BrandEditVM request);
        Task<List<Brand>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();
    }
}

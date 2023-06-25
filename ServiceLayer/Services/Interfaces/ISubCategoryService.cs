using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IEnumerable<SubCategory>> GetAllAsync();
        Task<IEnumerable<SubCategory>> GetAllWithIncludesAsync();
        Task<SubCategory> GetByIdWithIncludesAsync(int id);
        Task CreateAsync(SubCategoryCreateVM request);
        Task<SubCategory> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, SubCategoryEditVM request);
    }
}

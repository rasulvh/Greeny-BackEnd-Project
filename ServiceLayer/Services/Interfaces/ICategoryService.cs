using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetAllWithIncludesAsync();
        Task CreateAsync(CategoryCreateVM request);
        Task DeleteAsync(int id);
        Task<Category> GetByIdAsync(int id);
        Task EditAsync(int categoryId, CategoryEditVM request);
    }
}

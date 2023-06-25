using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.ViewModels.Admin.SubCategory;

namespace ServiceLayer.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task CreateAsync(SubCategoryCreateVM request)
        {
            SubCategory subCategory = new()
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
            };

            await _subCategoryRepository.CreateAsync(subCategory);
        }

        public async Task DeleteAsync(int id)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(id);

            await _subCategoryRepository.DeleteAsync(subCategory);
        }

        public async Task EditAsync(int id, SubCategoryEditVM request)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(id);

            subCategory.CategoryId = request.CategoryId;
            subCategory.Name = request.Name;

            await _subCategoryRepository.UpdateAsync(subCategory);
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            return await _subCategoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<SubCategory>> GetAllWithIncludesAsync()
        {
            Func<IQueryable<SubCategory>, IIncludableQueryable<SubCategory, object>>[] includes =
            {
                entity => entity.Include(m=>m.Category),
                entity => entity.Include(m=>m.Products),
            };

            return await _subCategoryRepository.GetAllWithIncludesAsync(includes);
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            return await _subCategoryRepository.GetByIdAsync(id);
        }

        public async Task<SubCategory> GetByIdWithIncludesAsync(int id)
        {
            Func<IQueryable<SubCategory>, IIncludableQueryable<SubCategory, object>>[] includes =
{
                entity => entity.Include(m=>m.Category),
                entity => entity.Include(m=>m.Products),
            };

            return await _subCategoryRepository.GetByIdWithIncludesAsync(id, includes);
        }
    }
}

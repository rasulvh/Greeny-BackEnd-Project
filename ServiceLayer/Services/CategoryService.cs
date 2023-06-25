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
using ServiceLayer.ViewModels.Admin.Category;
using static System.Net.Mime.MediaTypeNames;
using ServiceLayer.Helpers;
using Microsoft.AspNetCore.Hosting;
using Org.BouncyCastle.Asn1.Ocsp;
using RepositoryLayer.Repositories;
using ServiceLayer.ViewModels.Admin.Product;

namespace ServiceLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _env;

        public CategoryService(ICategoryRepository categoryRepository,
                               IWebHostEnvironment env)
        {
            _categoryRepository = categoryRepository;
            _env = env;
        }

        public async Task CreateAsync(CategoryCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "images/suggest");

            image = fileName;

            Category category = new()
            {
                Image = image,
                Name = request.Name,
            };

            await _categoryRepository.CreateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            Func<IQueryable<Category>, IIncludableQueryable<Category, object>>[] includes =
{
                entity => entity.Include(m=>m.Products),
                entity => entity.Include(m=>m.SubCategories)
            };

            var category = await _categoryRepository.GetByIdWithIncludesAsync(id, includes);

            await _categoryRepository.DeleteAsync(category);

            string path = Path.Combine(_env.WebRootPath, "images/suggest", category.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetAllWithIncludesAsync()
        {
            Func<IQueryable<Category>, IIncludableQueryable<Category, object>>[] includes =
{
                entity => entity.Include(m=>m.SubCategories),
                entity => entity.Include(m=>m.Products),
            };

            return await _categoryRepository.GetAllWithIncludesAsync(includes);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task EditAsync(int categoryId, CategoryEditVM request)
        {
            string image = string.Empty;

            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "images/suggest");
            }

            category.Name = request.Name;
            category.Image = image;

            await _categoryRepository.UpdateAsync(category);
        }
    }
}

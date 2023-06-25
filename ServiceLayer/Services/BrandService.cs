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
using ServiceLayer.ViewModels.Admin.Brand;
using RepositoryLayer.Repositories;
using ServiceLayer.Helpers;
using Microsoft.AspNetCore.Hosting;
using ServiceLayer.ViewModels.Admin.Category;

namespace ServiceLayer.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IWebHostEnvironment _env;

        public BrandService(IBrandRepository brandRepository,
                            IWebHostEnvironment env)
        {
            _brandRepository = brandRepository;
            _env = env;
        }

        public async Task CreateAsync(BrandCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "images/brand");

            image = fileName;

            Brand brand = new()
            {
                Image = image,
                Name = request.Name,
            };

            await _brandRepository.CreateAsync(brand);
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllWithIncludesAsync()
        {
            Func<IQueryable<Brand>, IIncludableQueryable<Brand, object>>[] includes =
            {
                entity => entity.Include(m=>m.Products),
            };

            return await _brandRepository.GetAllWithIncludesAsync(includes);
        }

        public async Task DeleteAsync(int id)
        {
            Func<IQueryable<Brand>, IIncludableQueryable<Brand, object>>[] includes =
{
                entity => entity.Include(m=>m.Products),
            };

            var category = await _brandRepository.GetByIdWithIncludesAsync(id, includes);

            await _brandRepository.DeleteAsync(category);

            string path = Path.Combine(_env.WebRootPath, "images/brand", category.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task EditAsync(int categoryId, BrandEditVM request)
        {
            string image = string.Empty;

            var brand = await _brandRepository.GetByIdAsync(categoryId);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "images/brand");
            }

            brand.Name = request.Name;
            brand.Image = image;

            await _brandRepository.UpdateAsync(brand);
        }
    }
}

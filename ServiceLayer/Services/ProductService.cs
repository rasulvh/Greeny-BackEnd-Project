using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.ViewModels.Admin.Product;
using ServiceLayer.ViewModels.Admin;
using ServiceLayer.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace ServiceLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IProductTagService _productTagService;
        private readonly IReviewService _reviewService;
        private readonly IProductImageRepository _productImageRepository;

        public ProductService(IProductRepository productRepository,
                              IWebHostEnvironment env,
                              IProductTagService productTagService,
                              IReviewService reviewService,
                              IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _env = env;
            _productTagService = productTagService;
            _reviewService = reviewService;
            _productImageRepository = productImageRepository;
        }

        public int Generate7DigitNumber()
        {
            Random random = new Random();
            int min = 1000000;
            int max = 9999999;
            return random.Next(min, max);
        }

        public async Task CreateAsync(ProductCreateVM model)
        {
            List<ProductImage> images = new();

            foreach (var item in model.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/product");
                images.Add(new ProductImage { Image = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            Product product = new()
            {
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                Description = model.Description,
                DiscountId = model.DiscountId,
                StockCount = model.StockCount,
                SubCategoryId = model.SubCategoryId,
                Images = images,
                Price = model.Price,
                Name = model.Name,
                SKU = Generate7DigitNumber(),
                RatingId = 6,
            };

            await _productRepository.CreateAsync(product);

            foreach (var item in model.Tags)
            {
                if (item.IsChecked)
                {
                    await _productTagService.AddTagToProductAsync(product, item.Id);
                }
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithIncludesAsync()
        {
            Expression<Func<Product, object>>[] includes =
            {
                entity => entity.ProductTags,
                entity => entity.Images,
                entity => entity.SubCategory,
                entity => entity.Category,
                entity => entity.Brand,
                entity => entity.Discount,
                entity => entity.Rating,
                entity => entity.Reviews,
            };

            return await _productRepository.GetAllWithIncludesAsync(includes);
        }

        public async Task<Product> GetByIdWithIncludesAsync(int id)
        {
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>>[] includeFuncs =
            {
                entity => entity.Include(m=>m.ProductTags).ThenInclude(m=>m.Tag),
                entity => entity.Include(m=>m.SubCategory),
                entity => entity.Include(m=>m.Category),
                entity => entity.Include(m=>m.Discount),
                entity => entity.Include(m=>m.Brand),
                entity => entity.Include(m=>m.Rating),
                entity => entity.Include(m=>m.Images),
                entity => entity.Include(m=>m.Reviews),
                entity => entity.Include(m=>m.Reviews).ThenInclude(m=>m.AppUser),
            };

            return await _productRepository.GetByIdWithIncludesAsync(id, includeFuncs);
        }

        public async Task DeleteAsync(int? id)
        {
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>>[] includes =
            {
                entity => entity.Include(m=>m.Images),
            };

            var product = await _productRepository.GetByIdWithIncludesAsync((int)id, includes);

            var reviews = await _reviewService.GetAllAsync();

            foreach (var item in reviews)
            {
                if (item.ProductId == product.Id)
                {
                    await _reviewService.DeleteAsync(item.Id);
                }
            }

            await _productRepository.DeleteAsync(product);

            foreach (var item in product.Images)
            {
                string path = Path.Combine(_env.WebRootPath, "images/product", item.Image);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        public async Task EditAsync(int productId, ProductEditVM request)
        {
            List<ProductImage> images = new();

            var product = await GetByIdAsync(productId);

            if (request.NewImages != null)
            {
                foreach (var item in request.NewImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    await item.SaveFileAsync(fileName, _env.WebRootPath, "images/product");
                    images.Add(new ProductImage { Image = fileName, ProductId = productId });
                }

                await _productImageRepository.AddRangeAsync(images);
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.StockCount = request.StockCount;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;
            product.SubCategoryId = request.SubCategoryId;
            product.DiscountId = request.DiscountId;
            product.BrandId = request.BrandId;
            foreach (var item in request.Tags)
            {
                if (item.IsChecked)
                {
                    var productTags = await _productTagService.GetAllAsync();

                    var isExist = productTags.FirstOrDefault(m => m.TagId == item.Id && m.ProductId == product.Id);

                    if (isExist == null)
                    {
                        await _productTagService.AddTagToProductAsync(product, item.Id);
                    }
                }
                else
                {
                    var productTags = await _productTagService.GetAllAsync();

                    var productTag = productTags.FirstOrDefault(m => m.TagId == item.Id);

                    await _productTagService.RemoveTagFromProductAsync(product, item.Id);
                }
            }

            await _productRepository.UpdateAsync(product);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task DeleteImageByIdAsync(int id)
        {
            ProductImage image = await _productImageRepository.GetByIdAsync(id);
            await _productImageRepository.DeleteAsync(image);

            string path = Path.Combine(_env.WebRootPath, "images/product", image.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task ChangeImageIsMainAsync(int id)
        {
            ProductImage image = await _productImageRepository.GetByIdAsync(id);
            var images = await _productImageRepository.GetAllAsync();
            foreach (var item in images)
            {
                if (item.IsMain && item.ProductId == image.ProductId)
                {
                    item.IsMain = false;
                }
            }
            image.IsMain = true;
            foreach (var item in images)
            {
                await _productImageRepository.UpdateAsync(item);
            }
        }
    }
}

using DomainLayer.Models;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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

            var product = await _productRepository.GetByIdWithIncludesAsync(id, includes);

            return product;
        }
    }
}

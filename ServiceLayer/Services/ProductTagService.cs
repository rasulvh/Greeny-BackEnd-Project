using DomainLayer.Models;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProductTagService : IProductTagService
    {
        private readonly IProductTagRepository _productTagRepository;

        public ProductTagService(IProductTagRepository productTagRepository)
        {
            _productTagRepository = productTagRepository;
        }

        public async Task AddTagToProductAsync(Product product, int tagId)
        {
            ProductTag productTag = new()
            {
                ProductId = product.Id,
                TagId = tagId
            };

            await _productTagRepository.CreateAsync(productTag);
        }

        public async Task<IEnumerable<ProductTag>> GetAllAsync()
        {
            return await _productTagRepository.GetAllAsync();
        }

        public async Task RemoveTagFromProductAsync(Product product, int tagId)
        {
            var productTags = await _productTagRepository.GetAllAsync();

            var pivotDatas = productTags.Where(m => m.ProductId == product.Id);

            foreach (var item in pivotDatas)
            {
                if (item.TagId == tagId)
                {
                    await _productTagRepository.DeleteAsync(item);
                }
            }

        }
    }
}

using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProductTagService
    {
        Task AddTagToProductAsync(Product product, int tagId);
        Task<IEnumerable<ProductTag>> GetAllAsync();
        Task RemoveTagFromProductAsync(Product product, int tagId);
    }
}

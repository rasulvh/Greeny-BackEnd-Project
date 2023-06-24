using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Product;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllWithIncludesAsync();
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdWithIncludesAsync(int id);
        Task CreateAsync(ProductCreateVM request);
        Task DeleteAsync(int? id);
        Task EditAsync(int productId, ProductEditVM request);
        Task<Product> GetByIdAsync(int id);
        Task DeleteImageByIdAsync(int id);
        Task ChangeImageIsMainAsync(int id);
    }
}

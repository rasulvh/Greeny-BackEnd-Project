using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories
{
    public class ProductBasketRepository : Repository<ProductBasket>, IProductBasketRepository
    {
        public ProductBasketRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ProductBasket> GetByBasketIdAsync(int id)
        {
            return await _context.ProductBaskets.FirstOrDefaultAsync(m => m.BasketId == id);
        }
    }
}

using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Brand>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Brands
                .Include(m => m.Products)
                .Where(m => !m.SoftDelete)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
        }
    }
}

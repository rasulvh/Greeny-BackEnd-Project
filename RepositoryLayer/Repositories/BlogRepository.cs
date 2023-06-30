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
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Blog>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Blogs
                .Where(m => !m.SoftDelete)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
        }
    }
}

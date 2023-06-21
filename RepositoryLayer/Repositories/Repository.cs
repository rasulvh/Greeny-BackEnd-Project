using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> entities;

        public Repository(AppDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression != null)
            {
                return await entities.Where(expression).ToListAsync();
            }

            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            T entity = await entities.FindAsync(id);

            return entity ?? throw new NullReferenceException("Data not found");
        }

        public async Task<T> GetByIdWithIncludesAsync(int? id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(m => (int)m.GetType().GetProperty("Id").GetValue(m) == id);
        }
    }
}

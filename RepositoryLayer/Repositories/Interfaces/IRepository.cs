using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T> GetByIdAsync(int? id);
        Task<T> GetByIdWithIncludesAsync(int? id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
    }
}

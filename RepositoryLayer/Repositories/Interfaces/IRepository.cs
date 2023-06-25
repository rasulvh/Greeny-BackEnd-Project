using Microsoft.EntityFrameworkCore.Query;
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
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIdAsync(int? id);
        Task<T> GetByIdWithIncludesAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetAllWithIncludesAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T> GetByExpressionForPivotTable(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
    }
}

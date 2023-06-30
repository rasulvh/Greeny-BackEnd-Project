using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories.Interfaces
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<List<Blog>> GetPaginatedDatasAsync(int page, int take);
    }
}

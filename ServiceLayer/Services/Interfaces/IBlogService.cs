using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, BlogEditVM request);
        Task<int> GetCountAsync();
        Task<List<Blog>> GetPaginatedDatasAsync(int page, int take);
        Task<List<Blog>> GetSearchedBlogs(string searchText = null);
    }
}

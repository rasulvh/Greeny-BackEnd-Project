using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Tags;

namespace ServiceLayer.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<IEnumerable<Tag>> GetAllWithIncludesAsync();
        Task<Tag> GetByIdAsync(int id);
        Task EditAsync(int id, TagEditVM request);
        Task DeleteAsync(int id);
        Task CreateAsync(TagCreateVM request);
    }
}

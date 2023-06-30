using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Repositories;
using ServiceLayer.ViewModels.Admin.SubCategory;
using ServiceLayer.ViewModels.Admin.Tags;

namespace ServiceLayer.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _tagRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllWithIncludesAsync()
        {
            Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>[] includes =
{
                entity => entity.Include(m=>m.ProductTags),
            };
            return await _tagRepository.GetAllWithIncludesAsync(includes);
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _tagRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(TagCreateVM request)
        {
            Tag tag = new()
            {
                Name = request.Name,
            };

            await _tagRepository.CreateAsync(tag);
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            await _tagRepository.DeleteAsync(tag);
        }

        public async Task EditAsync(int id, TagEditVM request)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            tag.Name = request.Name;

            await _tagRepository.UpdateAsync(tag);
        }

    }
}

using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;

namespace ServiceLayer.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly ICategoryRepository _categoryRepository;

        public LayoutService(ISettingRepository settingRepository,
                             ICategoryRepository categoryRepository)
        {
            _settingRepository = settingRepository;
            _categoryRepository = categoryRepository;
        }

        public LayoutVM GetAllDatasAsync()
        {
            //Func<IQueryable<Category>, IIncludableQueryable<Category, object>>[] includes =
            //{
            //    entity => entity.Include(m=>m.SubCategories),
            //    entity => entity.Include(m=>m.Products)
            //};

            var settingDatas = _settingRepository.GetAllDatas();
            //var categoryDropdown = await _categoryRepository.GetAllWithIncludesAsync(includes);

            LayoutVM model = new()
            {
                SettingDatas = settingDatas,
                //Categories = categoryDropdown
            };

            return model;
        }
    }
}

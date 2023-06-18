using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;

namespace ServiceLayer.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ISettingRepository _settingRepository;

        public LayoutService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public LayoutVM GetAllDatasAsync()
        {
            var settingDatas = _settingRepository.GetAllDatas();

            LayoutVM model = new()
            {
                SettingDatas = settingDatas
            };

            return model;
        }
    }
}

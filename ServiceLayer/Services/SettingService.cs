using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Admin.Setting;
using ServiceLayer.ViewModels.Admin.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public Dictionary<string, string> GetAll()
        {
            return _settingRepository.GetAllDatas();
        }

        public List<Setting> GetAllWithIds()
        {
            return _settingRepository.GetAllWithIdsDatas();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _settingRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(SettingCreateVM request)
        {
            Setting setting = new()
            {
                Key = request.Key,
                Value = request.Value,
            };

            await _settingRepository.CreateAsync(setting);
        }

        public async Task DeleteAsync(int id)
        {
            var setting = await _settingRepository.GetByIdAsync(id);

            await _settingRepository.DeleteAsync(setting);
        }

        public async Task EditAsync(int id, SettingEditVM request)
        {
            var setting = await _settingRepository.GetByIdAsync(id);

            setting.Value = request.Value;
            setting.Key = request.Key;

            await _settingRepository.UpdateAsync(setting);
        }
    }
}

using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Setting;
using ServiceLayer.ViewModels.Admin.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetAll();
        List<Setting> GetAllWithIds();
        Task EditAsync(int id, SettingEditVM request);
        Task DeleteAsync(int id);
        Task CreateAsync(SettingCreateVM request);
        Task<Setting> GetByIdAsync(int id);
    }
}

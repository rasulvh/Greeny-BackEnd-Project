using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Blog;
using ServiceLayer.ViewModels.Admin.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City> GetByIdAsync(int id);
        Task CreateAsync(CityCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, CityEditVM request);
    }
}

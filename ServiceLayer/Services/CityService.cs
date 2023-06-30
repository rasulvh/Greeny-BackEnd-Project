using DomainLayer.Models;
using Microsoft.AspNetCore.Hosting;
using RepositoryLayer.Repositories;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Helpers;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Admin.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IWebHostEnvironment _env;

        public CityService(ICityRepository cityRepository,
                           IWebHostEnvironment env)
        {
            _cityRepository = cityRepository;
            _env = env;
        }

        public async Task CreateAsync(CityCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "images/branch");

            image = fileName;

            City city = new()
            {
                Image = image,
                Address = request.Address,
                Name = request.Name,
            };

            await _cityRepository.CreateAsync(city);
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);

            await _cityRepository.DeleteAsync(city);

            string path = Path.Combine(_env.WebRootPath, "images/branch", city.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, CityEditVM request)
        {
            var city = await _cityRepository.GetByIdAsync(id);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                city.Image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "images/branch");
            }

            city.Address = request.Address;
            city.Name = request.Name;

            await _cityRepository.UpdateAsync(city);
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }
    }
}

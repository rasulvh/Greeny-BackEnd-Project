using DomainLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Helpers;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;
using ServiceLayer.ViewModels.Admin.Category;
using ServiceLayer.ViewModels.Admin.Slider;

namespace ServiceLayer.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IWebHostEnvironment _env;

        public SliderService(ISliderRepository sliderRepository,
                             IWebHostEnvironment env)
        {
            _sliderRepository = sliderRepository;
            _env = env;
        }

        public async Task<List<SliderVM>> GetAllAsync()
        {
            var datas = await _sliderRepository.FindAllAsync(m => !m.SoftDelete);

            List<SliderVM> model = new List<SliderVM>();

            foreach (var data in datas)
            {
                model.Add(new SliderVM { Id = data.Id, Image = data.Image, Title = data.Title, Description = data.Description, Status = data.Status });
            }

            return model;
        }

        public async Task<bool> ChangeStatusAsync(Slider slider)
        {
            if (slider.Status && await GetCountAsync() != 1)
            {
                slider.Status = false;
                await _sliderRepository.UpdateAsync(slider);
                return true;
            }
            else
            {
                if (!slider.Status)
                {
                    slider.Status = true;
                    await _sliderRepository.UpdateAsync(slider);
                    return true;
                }
                slider.Status = true;
            }

            await _sliderRepository.UpdateAsync(slider);

            return false;
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _sliderRepository.GetByIdAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            var sliders = await _sliderRepository.GetAllAsync();
            return sliders.Where(m => m.Status).Count();
        }

        public async Task CreateAsync(SliderCreateVM request)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;

            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "images/home/index/");

            Slider slider = new()
            {
                Title = request.Title,
                Description = request.Description,
                Image = fileName,
                Status = true
            };

            await _sliderRepository.CreateAsync(slider);
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await GetByIdAsync(id);

            await _sliderRepository.DeleteAsync(slider);

            string path = Path.Combine(_env.WebRootPath, "images/home/index/", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int sliderId, SliderEditVM request)
        {
            var slider = await _sliderRepository.GetByIdAsync(sliderId);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                slider.Image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "images/home/index");
            }

            slider.Title = request.Title;
            slider.Description = request.Description;

            await _sliderRepository.UpdateAsync(slider);
        }
    }
}

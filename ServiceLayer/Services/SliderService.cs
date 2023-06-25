using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;

namespace ServiceLayer.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
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
    }
}

using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                model.Add(new SliderVM { Image = data.Image, Title = data.Title, Description = data.Description, Status = data.Status });
            }

            return model;
        }
    }
}

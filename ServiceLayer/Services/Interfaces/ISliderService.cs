using DomainLayer.Models;
using ServiceLayer.ViewModels;
using ServiceLayer.ViewModels.Admin.Slider;

namespace ServiceLayer.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task<bool> ChangeStatusAsync(Slider slider);
        Task CreateAsync(SliderCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(int sliderId, SliderEditVM request);
    }
}

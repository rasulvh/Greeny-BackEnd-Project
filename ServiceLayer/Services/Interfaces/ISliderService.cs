using DomainLayer.Models;
using ServiceLayer.ViewModels;

namespace ServiceLayer.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task<bool> ChangeStatusAsync(Slider slider);
    }
}

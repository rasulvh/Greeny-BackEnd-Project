using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;
using ServiceLayer.ViewModels.Admin.Slider;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();

            List<SliderVM> mappedSliders = new();

            foreach (var item in sliders)
            {
                mappedSliders.Add(new SliderVM
                {
                    Id = item.Id,
                    Description = item.Description,
                    Image = item.Image,
                    Status = item.Status,
                    Title = item.Title
                });
            }

            return View(mappedSliders);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            SliderDetailVM model = new()
            {
                CreateDate = slider.CreateDate,
                Description = slider.Description,
                Image = slider.Image,
                Status = slider.Status,
                Title = slider.Title
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null) return BadRequest();

            Slider slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return Ok(await _sliderService.ChangeStatusAsync(slider));
        }
    }
}

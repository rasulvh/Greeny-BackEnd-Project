using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;
using ServiceLayer.ViewModels.Admin.Category;
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "This file must be in image format");
                return View();
            }

            if (request.Image.CheckFileSize(500))
            {
                ModelState.AddModelError("Image", "Image size cannot be more than 200 KB");
                return View();
            }

            await _sliderService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            await _sliderService.DeleteAsync((int)id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _sliderService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            SliderEditVM model = new()
            {
                Description = category.Description,
                Image = category.Image,
                Title = category.Title,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderEditVM request)
        {
            var slider = await _sliderService.GetByIdAsync(id);

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage);
                foreach (var item in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                request.Image = slider.Image;
                return View(request);
            }

            if (request.NewImage != null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File format must be image");
                    request.Image = slider.Image;
                    return View(request);
                }


                if (request.NewImage.CheckFileSize(500))
                {
                    ModelState.AddModelError("NewImage", "Image size can be maximum 200 KB");
                    request.Image = slider.Image;
                    return View(request);
                }
            }

            await _sliderService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }
    }
}

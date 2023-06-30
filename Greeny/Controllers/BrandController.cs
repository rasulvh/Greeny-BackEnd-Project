using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;
using ServiceLayer.ViewModels.Admin.Product;

namespace Greeny.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly ISettingService _settingService;

        public BrandController(IBrandService brandService,
                               ISettingService settingService)
        {
            _brandService = brandService;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var settingDatas = _settingService.GetAll();

            int take = int.Parse(settingDatas["BrandPaginateTakeCount"]);

            var paginatedDatas = await _brandService.GetPaginatedDatasAsync(page, take);

            var pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            Paginate<Brand> result = new(paginatedDatas, page, pageCount);

            int brandCount = await _brandService.GetCountAsync();

            BrandVM model = new()
            {
                PaginateResult = result,
                BrandCount = brandCount
            };

            return View(model);
        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _brandService.GetCountAsync();

            var pageCount = Math.Ceiling((decimal)count / take);

            return (int)pageCount;
        }
    }
}

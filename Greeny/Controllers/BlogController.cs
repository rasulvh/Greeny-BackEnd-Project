using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels;

namespace Greeny.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ISettingService _settingService;

        public BlogController(IBlogService blogService,
                              ISettingService settingService)
        {
            _blogService = blogService;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index(int page = 1, string searchText = null)
        {
            var settingDatas = _settingService.GetAll();

            int take = int.Parse(settingDatas["BlogPaginateTakeCount"]);

            var paginatedDatas = await _blogService.GetPaginatedDatasAsync(page, take);

            var pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            Paginate<Blog> result = new(paginatedDatas, page, pageCount);

            int blogCount = await _blogService.GetCountAsync();

            var blogs = await _blogService.GetAllAsync();

            BlogVM model = new()
            {
                PaginateResult = result,
                BlogCount = blogCount,
                LastUploadedBlogs = blogs.OrderByDescending(m => m.Id).Take(5).ToList(),
            };

            if (searchText != null)
            {
                searchText = searchText.ToLower();
                var searchedBlogs = await _blogService.GetSearchedBlogs(searchText);
                model.SearchedBlogs = searchedBlogs;
            }

            return View(model);
        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _blogService.GetCountAsync();

            var pageCount = Math.Ceiling((decimal)count / take);

            return (int)pageCount;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace Greeny.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;

        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = _layoutService.GetAllDatasAsync();

            return View(datas);
        }
    }
}

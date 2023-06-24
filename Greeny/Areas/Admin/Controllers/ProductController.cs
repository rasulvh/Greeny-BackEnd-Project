using DomainLayer.Models;
using ServiceLayer.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.Helpers;
using ServiceLayer.Services.Interfaces;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IBrandService _brandService;
        private readonly ITagService _tagService;

        public ProductController(IProductService productService,
                                 IDiscountService discountService,
                                 ICategoryService categoryService,
                                 ISubCategoryService subCategoryService,
                                 IBrandService brandService,
                                 ITagService tagService)
        {
            _productService = productService;
            _discountService = discountService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _brandService = brandService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithIncludesAsync();

            List<ProductVM> model = new();

            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Brand = product.Brand.Name,
                    Category = product.Category.Name,
                    Discount = product.Discount.Name,
                    Image = product.Images.FirstOrDefault(m => m.IsMain).Image,
                    Name = product.Name,
                    Price = product.Price,
                    StockCount = product.StockCount,
                });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _productService.GetByIdWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            ProductDetailVM model = new()
            {
                Id = product.Id,
                Brand = product.Brand.Name,
                Category = product.Category.Name,
                Name = product.Name,
                Description = product.Description,
                DiscountName = product.Discount.Name,
                DiscountPercent = product.Discount.Percent,
                Images = product.Images,
                Price = product.Price.ToString("0.####"),
                ProductTags = product.ProductTags,
                Rating = product.Rating.RatingCount,
                StockCount = product.StockCount,
                Reviews = product.Reviews,
                SaleCount = product.SaleCount,
                SKU = product.SKU,
                SubCategory = product.SubCategory.Name,
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetAllSelectOptions();

            var tags = await _tagService.GetAllAsync();
            List<TagCheckbox> tagCheckboxes = new();

            foreach (var item in tags)
            {
                tagCheckboxes.Add(new TagCheckbox { Id = item.Id, Value = item.Name, IsChecked = false });
            }

            ProductCreateVM model = new()
            {
                Tags = tagCheckboxes
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            await GetAllSelectOptions();

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Please select only image file");
                    return View();
                }

                if (item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 KB");
                    return View();
                }
            }

            await _productService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task GetAllSelectOptions()
        {
            ViewBag.categories = await GetCategories();
            ViewBag.subcategories = await GetSubCategories();
            ViewBag.discounts = await GetDiscounts();
            ViewBag.brands = await GetBrands();
        }

        public async Task<SelectList> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }

        public async Task<SelectList> GetSubCategories()
        {
            IEnumerable<SubCategory> subCategories = await _subCategoryService.GetAllAsync();
            return new SelectList(subCategories, "Id", "Name");
        }

        public async Task<SelectList> GetDiscounts()
        {
            IEnumerable<Discount> discounts = await _discountService.GetAllAsync();
            return new SelectList(discounts, "Id", "Name");
        }

        public async Task<SelectList> GetBrands()
        {
            IEnumerable<Brand> brands = await _brandService.GetAllAsync();
            return new SelectList(brands, "Id", "Name");
        }

        public async Task<JsonResult> GetSubCategoryByCategoryId(int categoryId)
        {
            var subCatog = await _subCategoryService.GetAllAsync();

            return Json(subCatog.Where(m => m.CategoryId == categoryId).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id is null) BadRequest();

            await _productService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _productService.GetByIdWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            await GetAllSelectOptions();

            var tags = await _tagService.GetAllAsync();
            List<TagCheckbox> tagCheckboxes = new();

            foreach (var tag in tags)
            {
                if (tag.ProductTags == null)
                {
                    tagCheckboxes.Add(new TagCheckbox { Id = tag.Id, Value = tag.Name, IsChecked = false });
                }
                else
                {
                    foreach (var item in tag.ProductTags)
                    {
                        if (item.ProductId == product.Id)
                        {
                            tagCheckboxes.Add(new TagCheckbox { Id = tag.Id, Value = tag.Name, IsChecked = true });
                        }
                        else
                        {
                            tagCheckboxes.Add(new TagCheckbox { Id = tag.Id, Value = tag.Name, IsChecked = false });
                        }
                    }
                }
            }

            ProductEditVM model = new()
            {
                BrandId = product.BrandId,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Description = product.Description,
                DiscountId = product.DiscountId,
                Images = product.Images.ToList(),
                Price = product.Price,
                StockCount = product.StockCount,
                SubCategoryId = product.SubCategoryId,
                Tags = tagCheckboxes,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, ProductEditVM request)
        {
            await GetAllSelectOptions();

            var product = await _productService.GetByIdWithIncludesAsync(id);

            request.Images = product.Images.ToList();

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage);
                foreach (var item in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View(request);
            }

            if (request.NewImages != null)
            {
                foreach (var item in request.NewImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Image", "File format must be image");
                        request.Images = product.Images.ToList();
                        return View(request);
                    }


                    if (item.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Image", "Image size can be maximum 200 KB");
                        request.Images = product.Images.ToList();
                        return View(request);
                    }
                }
            }

            await _productService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            await _productService.DeleteImageByIdAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeImageIsMain(int id)
        {
            await _productService.ChangeImageIsMainAsync(id);
            return Ok();
        }
    }
}

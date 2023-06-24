using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int StockCount { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int DiscountId { get; set; }
        public int BrandId { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
        public List<TagCheckbox> Tags { get; set; }
    }
}

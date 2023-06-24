using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.Admin.Product
{
    public class ProductEditVM
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
        public List<IFormFile>? NewImages { get; set; }
        public List<ProductImage>? Images { get; set; }
        public List<TagCheckbox> Tags { get; set; }
    }
}

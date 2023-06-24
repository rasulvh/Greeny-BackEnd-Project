using DomainLayer.Models;

namespace ServiceLayer.ViewModels.Admin.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SKU { get; set; }
        public int SaleCount { get; set; }
        public int StockCount { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountPercent { get; set; }
        public string Brand { get; set; }
        public byte Rating { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<ProductTag> ProductTags { get; set; }
    }
}

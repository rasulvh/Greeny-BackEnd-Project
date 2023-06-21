using DomainLayer.Models;

namespace Greeny.Areas.Admin.ViewModel.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Discount { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
    }
}

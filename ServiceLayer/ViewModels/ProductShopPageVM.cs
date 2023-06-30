namespace ServiceLayer.ViewModels
{
    public class ProductShopPageVM
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte Discount { get; set; }
        public int Rating { get; set; }
        public int ReviewCount { get; set; }
    }
}

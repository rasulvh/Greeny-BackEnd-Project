using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels
{
    public class ShopVM
    {
        public List<ProductShopPageVM> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}

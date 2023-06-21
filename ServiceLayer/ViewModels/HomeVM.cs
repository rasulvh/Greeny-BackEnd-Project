using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

using DomainLayer.Models;
using ServiceLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels
{
    public class BrandVM
    {
        public Paginate<Brand> PaginateResult { get; set; }
        public int BrandCount { get; set; }
    }
}

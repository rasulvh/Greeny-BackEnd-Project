using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int SKU { get; set; }
        public int BrandId { get; set; }
        public int Rating { get; set; }
    }
}

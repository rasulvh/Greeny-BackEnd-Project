using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

﻿using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

﻿using DomainLayer.Common;

namespace DomainLayer.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
    }
}

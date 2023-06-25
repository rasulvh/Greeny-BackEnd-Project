﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.Admin.SubCategory
{
    public class SubCategoryEditVM
    {
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}

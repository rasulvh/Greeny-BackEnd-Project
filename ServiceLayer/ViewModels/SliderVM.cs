﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels
{
    public class SliderVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}

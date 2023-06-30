﻿using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string About { get; set; }
        public string Position { get; set; }
    }
}

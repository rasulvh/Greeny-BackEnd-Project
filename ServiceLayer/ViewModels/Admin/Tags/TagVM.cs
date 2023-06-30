using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.Admin.Tags
{
    public class TagVM
    {
        public IEnumerable<Tag> Tags { get; set; }
        public int ProductCount { get; set; }
    }
}

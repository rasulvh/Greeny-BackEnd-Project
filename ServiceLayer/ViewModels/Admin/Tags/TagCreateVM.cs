using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.Admin.Tags
{
    public class TagCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}

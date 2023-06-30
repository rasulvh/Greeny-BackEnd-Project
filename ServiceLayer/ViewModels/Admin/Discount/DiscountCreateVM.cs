using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.Admin.Discount
{
    public class DiscountCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Percent must be between 0 and 100")]
        public byte Percent { get; set; }
    }
}

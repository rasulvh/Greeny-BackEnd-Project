
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.Admin.SubCategory
{
    public class SubCategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}

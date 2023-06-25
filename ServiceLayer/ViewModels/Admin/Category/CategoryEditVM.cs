using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.Admin.Category
{
    public class CategoryEditVM
    {
        [Required]
        public string Name { get; set; }
        public IFormFile? NewImage { get; set; }
        public string? Image { get; set; }
    }
}

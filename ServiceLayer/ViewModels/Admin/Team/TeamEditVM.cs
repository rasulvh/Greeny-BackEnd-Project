using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.Admin.Team
{
    public class TeamEditVM
    {
        [Required]
        public string Name { get; set; }
        public IFormFile? NewImage { get; set; }
        public string? Image { get; set; }
        [Required]
        public string About { get; set; }
        [Required]
        public string Position { get; set; }
    }
}

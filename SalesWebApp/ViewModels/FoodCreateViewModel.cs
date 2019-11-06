using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public class FoodCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 45, ErrorMessage = "The {0} value must be between {2} and {1} characters")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        public IFormFile Photo { get; set; }
    }
}

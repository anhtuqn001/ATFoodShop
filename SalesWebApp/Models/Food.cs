using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public class Food
    {
        public int FoodId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
       
        public string ShortDescription { get; set; }
 
        public string LongDescription { get; set; }
        public string PhotoPath { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        
    }
}

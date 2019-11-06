using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public class ShoppingCartItem
    {
        public string Id { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Quantity { get; set; } = 1;

        public string ShoppingCartId { get; set; }
        public int FoodId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
        public Food Food { get; set; }
    }
}

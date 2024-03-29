﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; }
    }

}

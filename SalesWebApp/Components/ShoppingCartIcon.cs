using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebApp.Components
{
    public class ShoppingCartIcon : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartIcon(ShoppingCart shoppingCart, IHttpContextAccessor httpContext )
        {
            _httpContext = httpContext;
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            List<ShoppingCartItem> items = _shoppingCart.GetItemsInCart();
            int itemCount = items.Count;
            return View(itemCount);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebApp.Models
{
    public class ShoppingCart
    {
        private AppDbContext _context;

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }
        public string Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PurchaseTime { get; set; }

        public List<ShoppingCartItem> Items { get; set; }

        public bool IsCheckedOut { get; set; }
        public bool IsConfirmed { get; set; }
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string userId = services.GetRequiredService<IHttpContextAccessor>().HttpContext.User.Identity.IsAuthenticated ? services.GetRequiredService<IHttpContextAccessor>().HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value : null;
            var context = services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

            if(userId != null)
            {
                ShoppingCart unsubmitedCart = context.ShoppingCarts
                                    .SingleOrDefault(c => c.ApplicationUserId == userId && c.IsCheckedOut == false);
                if (unsubmitedCart != null)
                {
                    return unsubmitedCart;
                }
                return new ShoppingCart(context) { Id = Guid.NewGuid().ToString(), ApplicationUserId = userId };
            }
            else
            {
                string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
                session.SetString("CartId", cartId);
                return new ShoppingCart(context) { Id = cartId};
            }          
        }
        

        public ShoppingCartItem AddItemToCart(Food food, int quantity)
        {
            ShoppingCartItem item = _context.ShoppingCartItems
                                        .SingleOrDefault(i => i.Food.FoodId == food.FoodId && i.ShoppingCartId == Id);
            if(item == null)
            {
                item = new ShoppingCartItem()
                {
                    FoodId = food.FoodId,
                    ShoppingCartId = Id,
                    Quantity = quantity
                };
                _context.ShoppingCartItems.Add(item);
            }
            else
            {
                item.Quantity++;
            }
            _context.SaveChanges();
            return item;
        }

        public ShoppingCartItem DecreaseItemInCart(string itemId)
        {
            ShoppingCartItem item = _context.ShoppingCartItems.SingleOrDefault(i => i.Id == itemId);
            if(item.Quantity > 1)
            { 
                item.Quantity--;
            }
            else
            {
                _context.ShoppingCartItems.Remove(item);
            }
            _context.SaveChanges();
            return item;
        }

        public void RemoveItemFromCart(string itemId)
        {
            ShoppingCartItem item = _context.ShoppingCartItems.SingleOrDefault(i => i.Id == itemId);
            if(item != null)
            {
                _context.ShoppingCartItems.Remove(item);
                _context.SaveChanges();
            }
        }

        public decimal GetTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (ShoppingCartItem item in Items)
            {

                totalAmount += (item.Quantity * item.Food.Price);
            }
            return totalAmount;
        }

        public List<ShoppingCartItem> GetItemsInCart()
        {
            return Items ?? _context.ShoppingCartItems.AsNoTracking().Where(i => i.ShoppingCartId == Id).Include(i => i.Food).ToList();
        }
    }
}


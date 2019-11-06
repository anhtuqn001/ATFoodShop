using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebApp.Models
{
    public class SQLShoppingCartRepository : IShoppingCartRepository
    {
        private AppDbContext _context;
        public SQLShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }
        public ShoppingCart Add(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            _context.SaveChanges();
            return shoppingCart;
        }

        public List<ShoppingCart> GetAllCart()
        {
            return _context.ShoppingCarts
                            .Include(c => c.Items)
                                .ThenInclude(i => i.Food)
                            .Include(c => c.User)
                            .AsNoTracking()
                            .ToList();
        }

        public ShoppingCart GetCart(string shoppingCartId)
        {
            ShoppingCart sc = _context.ShoppingCarts.SingleOrDefault(c => c.Id == shoppingCartId);
            return sc;
        }

        public ShoppingCart Update(ShoppingCart shoppingCartChanges)
        {
            var shoppingCart = _context.ShoppingCarts.Attach(shoppingCartChanges);
            shoppingCart.State = EntityState.Modified;
            _context.SaveChanges();
            return shoppingCartChanges;
        }

        public ShoppingCart Delete(string shoppingCartId)
        {
            var shoppingCart = _context.ShoppingCarts.Find(shoppingCartId);
            if(shoppingCart != null)
            {
                _context.ShoppingCarts.Remove(shoppingCart);
                _context.SaveChanges();
            }
            return shoppingCart;
        }

        public IQueryable<ShoppingCart> GetCartsByUserId(string userId)
        {
            IQueryable<ShoppingCart> shoppingCart = _context.ShoppingCarts
                                                            .Include(c => c.Items)
                                                                .ThenInclude(i => i.Food)
                                                            .Where(c => c.ApplicationUserId == userId);
            return shoppingCart;
        }

        public void Clear(string shoppingCartId)
        {
            IQueryable<ShoppingCartItem> items = _context.ShoppingCartItems.Where(i => i.ShoppingCartId == shoppingCartId);
            if(items != null)
            {
                _context.ShoppingCartItems.RemoveRange(items);
                _context.SaveChanges();
            }
            return;
        }
    }
}

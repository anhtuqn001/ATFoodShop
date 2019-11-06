using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public interface IShoppingCartRepository
    {
        ShoppingCart Add(ShoppingCart shoppingCart);
        ShoppingCart GetCart(string shoppingCartId);
        ShoppingCart Update(ShoppingCart shoppingCart);
        List<ShoppingCart> GetAllCart();
        ShoppingCart Delete(string shoppingCartId);
        void Clear(string shoppingCartId);
        IQueryable<ShoppingCart> GetCartsByUserId(string userId);
    }
}

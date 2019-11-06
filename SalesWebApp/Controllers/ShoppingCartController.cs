using Microsoft.AspNetCore.Authorization;
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

namespace SalesWebApp.Controllers
{
    
    public class ShoppingCartController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ShoppingCart _shoppingCart;
        private IFoodRepository _foodRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                      ShoppingCart shoppingCart, IFoodRepository foodRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _shoppingCart = shoppingCart;
            _foodRepository = foodRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }
        
        public IActionResult Index()
        {
            var items = _shoppingCart.GetItemsInCart();
            _shoppingCart.Items = items;
            return View(_shoppingCart);
        }
        
        public IActionResult AddItemToCart(int foodId)
        {
            Food food = _foodRepository.GetFood(foodId);
            if(food == null)
            {
                ViewBag.ErrorMessage = $"Food with Id = {foodId} cannot be found";
                return View("NotFound");
            }
            ShoppingCart availableCart = _shoppingCartRepository.GetCart(_shoppingCart.Id);
            if(availableCart == null)
            {
                _shoppingCartRepository.Add(_shoppingCart);
            }
            ShoppingCartItem cartItem = _shoppingCart.AddItemToCart(food, 1);
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseItemInCart(string itemId)
        {
            _shoppingCart.DecreaseItemInCart(itemId);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult RemoveItemFromCart (string itemId)
        {   
            _shoppingCart.RemoveItemFromCart(itemId);
            return RedirectToAction("Index");
        }

        public IActionResult CheckoutCart()
        {
            _shoppingCart.PurchaseTime = DateTime.Now;
            _shoppingCart.IsCheckedOut = true;
            _shoppingCartRepository.Update(_shoppingCart);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult DeleteCart(string shoppingCartId)
        {
            ShoppingCart cart = _shoppingCartRepository.GetCart(shoppingCartId);
            if (cart == null)
            {
                ViewBag.ErrorMessage = $"The cart with Id={shoppingCartId} cannot be found";
                return View("NotFound");
            }
            _shoppingCartRepository.Delete(shoppingCartId);
            return RedirectToAction("ListOrders", "Administration");
        }

        public IActionResult ClearCart()
        {
            _shoppingCartRepository.Clear(_shoppingCart.Id);
            return RedirectToAction("Index");
        }

        public IActionResult PurchaseHistory()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IQueryable<ShoppingCart> items = _shoppingCartRepository.GetCartsByUserId(userId).Where(c => c.IsCheckedOut == true).OrderByDescending(i => i.PurchaseTime);
            return View(items.AsNoTracking().ToList());
        }

    }
}

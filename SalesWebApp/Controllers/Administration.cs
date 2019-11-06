using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesWebApp.Models;
using SalesWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
    public class Administration : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly AppDbContext _context;

        public Administration(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ILogger<Administration> logger, IShoppingCartRepository shoppingCartRepository, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _shoppingCartRepository = shoppingCartRepository;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Cannot find role with id = ${id}";
                return View("NotFound");
            }
            EditRoleViewModel model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid) {
                IdentityRole role = await _roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Cannot find role with id = ${model.Id}";
                    return View("NotFound");
                }
                else
                {
                    role.Name = model.RoleName;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles", "Administration");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;
            IdentityRole role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"The role with id = {roleId} cannot be found";
            }
            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                    return View("NotFound");
                }

                IdentityResult result = null;

                for (int i = 0; i < model.Count; i++)
                {
                    var user = await _userManager.FindByIdAsync(model[i].UserId);
                    if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await _userManager.AddToRoleAsync(user, role.Name);
                    } else if (!model[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    } else
                    {
                        continue;
                    }
                    if (result.Succeeded)
                    {
                        if (i < model.Count - 1)
                            continue;
                        else
                            return RedirectToAction("EditRole", new { id = role.Id });
                    }
                }
                return RedirectToAction("EditRole", new { id = role.Id });
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
           
            var userRoles = await _userManager.GetRolesAsync(user);

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
                Roles = userRoles
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                    return View("NotFound");
                }
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Address = model.Address;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Administration");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListUsers");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string id)
        {

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                try {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles", "Administration");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("ListRoles");
                }
                catch (DbUpdateException ex)
                {
                    ViewBag.ErrorTitle = $"{role.Name} is in use.";
                    ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users " +
                        $"in this role. If you want to delete this role, please remove the users from " +
                        $"the role and then try to delete.";
                    return View("Error");
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.UserId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                ManageUserRolesViewModel userRolesViewModel = new ManageUserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUserRoles(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("EditUser", new { id = userId });
        }

        [HttpGet]
        public IActionResult ListOrders()
        {
            IEnumerable<ShoppingCart> carts = _shoppingCartRepository.GetAllCart().Where(c => c.IsCheckedOut == true && c.IsConfirmed == false).OrderByDescending(c => c.PurchaseTime);
            return View(carts);
        }

        public IActionResult ConfirmCart(string cartId)
        {
            ShoppingCart shoppingCart = _context.ShoppingCarts.Find(cartId);
            _context.Entry(shoppingCart).State = EntityState.Detached;
            shoppingCart.IsConfirmed = true;
            _shoppingCartRepository.Update(shoppingCart);
            return RedirectToAction("ListOrders");
        }

        [HttpGet]
        public IActionResult TotalRevenue()
        {
            IEnumerable<ShoppingCart> carts = _shoppingCartRepository.GetAllCart().Where(c => c.IsCheckedOut == true && c.IsConfirmed == true).OrderByDescending(c => c.PurchaseTime);
            return View(carts);
        }
    }
}

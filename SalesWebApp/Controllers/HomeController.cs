using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SalesWebApp.Models;
using SalesWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IFoodRepository _foodRepository;
        private IHostingEnvironment _hostingEnvironment;

        public HomeController(IFoodRepository foodRepository,
                              IHostingEnvironment hostingEnvironment)
        {
            _foodRepository = foodRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            var foods = _foodRepository.GetAllFood();
            foods = foods.OrderByDescending(f => f);
            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString;
                foods = foods.Where(f => f.Name.ToLower().Contains(searchString.ToLower()));
            }
            int pageSize = 9;
            return View(await PaginatedList<Food>.CreateAsync(foods.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            Food food = _foodRepository.GetFood(id.Value);
            if (food == null)
            {
                Response.StatusCode = 404;
                return View("FoodNotFound", id.Value);
            };
            HomeDetailsViewModel model = new HomeDetailsViewModel
            {
                Food = food,
                PageTitle = "Food Details"
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FoodCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Food food = new Food
                {
                    Name = model.Name,
                    Price = model.Price,
                    ShortDescription = model.ShortDescription,
                    LongDescription = model.LongDescription,
                    PhotoPath = uniqueFileName
                };
                _foodRepository.Add(food);
                return RedirectToAction("details", new { id = food.FoodId });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Food food = _foodRepository.GetFood(id);
            FoodEditViewModel foodToEdit = new FoodEditViewModel
            {
                FoodId = food.FoodId,
                Name = food.Name,
                Price = food.Price,
                ShortDescription = food.ShortDescription,
                LongDescription = food.LongDescription,
                ExistingPhotoPath = food.PhotoPath
            };
            return View(foodToEdit);
        }

        [HttpPost]
        public IActionResult Edit(FoodEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Food food = _foodRepository.GetFood(model.FoodId);
                food.Name = model.Name;
                food.Price = model.Price;
                food.ShortDescription = model.ShortDescription;
                food.LongDescription = model.LongDescription;
                if (model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    food.PhotoPath = ProcessUploadedFile(model);
                }
                _foodRepository.Update(food);

                return RedirectToAction("details", new { id = food.FoodId });
            }
            return View();
        }

        private string ProcessUploadedFile(FoodCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) { 
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Delete(int? foodId)
        {
            var food = _foodRepository.GetFood(foodId.Value);
            if (food == null)
            {
                Response.StatusCode = 404;
                return View("FoodNotFound", foodId.Value);
            };
            _foodRepository.Delete(foodId.Value);
            return RedirectToAction("Index");
        }
    }
}

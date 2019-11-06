using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public class MockFoodRepository : IFoodRepository
    {
        private List<Food> _foodList;

        public MockFoodRepository()
        {
            _foodList = new List<Food>()
            {
                new Food() { FoodId = 1, Name = "FoodName1", Price = 1 },
                new Food() { FoodId = 2, Name = "FoodName2", Price = 2 },
                new Food() { FoodId = 3, Name = "FoodName3", Price = 3 }
            };
        }

        public Food Add(Food food)
        {
            food.FoodId = _foodList.Max(f => f.FoodId) + 1;
            _foodList.Add(food);
            return food;
        }

        public Food Delete(int id)
        {
           Food food = _foodList.FirstOrDefault(f => f.FoodId == id);
           if(food != null)
            { 
            _foodList.Remove(food);
            }
            return food;
        }

        public IQueryable<Food> GetAllFood()
        {
            return _foodList.AsQueryable();
        }

        public Food GetFood(int id)
        {
            return _foodList.FirstOrDefault(f => f.FoodId == id);
        }

        public Food Update(Food foodChanges)
        {
            Food food = _foodList.FirstOrDefault(f => f.FoodId == foodChanges.FoodId);
            if(food != null)
            {
                food.Name = foodChanges.Name;
                food.Price = foodChanges.Price;
            }
            return food;
        }
    }
}

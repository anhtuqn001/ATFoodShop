using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
     public interface IFoodRepository
    {
        Food GetFood(int id);
        IQueryable<Food> GetAllFood();
        Food Add(Food food);
        Food Update(Food FoodChanges);
        Food Delete(int id);
    }
}

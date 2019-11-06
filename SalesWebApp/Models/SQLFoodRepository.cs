using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public class SQLFoodRepository : IFoodRepository
    {
        private readonly AppDbContext context;
        public SQLFoodRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Food Add(Food food)
        {
            context.Foods.Add(food);
            context.SaveChanges();
            return food;
        }

        public Food Delete(int id)
        {
            Food food = context.Foods.Find(id);
            if(food != null)
            {
                context.Foods.Remove(food);
                context.SaveChanges();
            }
            return food;
        }

        public IQueryable<Food> GetAllFood()
        {
            return context.Foods;
        }

        public Food GetFood(int id)
        {
            return context.Foods.Find(id);
        }


        public Food Update(Food foodChanges)
        {
            var food = context.Foods.Attach(foodChanges);
            food.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return foodChanges;
        }
    }
}

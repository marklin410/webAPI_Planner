using System;
using MealPlanner.Domain.Entities;

namespace MealPlanner.Infrastructure.DTO
{
    public class MealPlanDTO
    {
        public int id { get; set; }
        public int Calories { get; set; }
        public int Fats { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public String Description { get; set; }

        public MealPlan ToEntity()
        {
            return new MealPlan()
            {
                Description = Description,
                Calories = Calories,
                Fats = Fats,
                Proteins = Proteins,
                Carbohydrates= Carbohydrates
            };
        }

    }
}

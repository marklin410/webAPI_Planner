using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner.Domain.Entities;

namespace MealPlanner.Presentation.Models
{
    


    public class MealPlanModel
    {
        public string Description { get; set; }
        public int Calories { get; set; }
        public int Fats { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }

        public MealPlanModel()
        {


        }
        public MealPlanModel(MealPlan mealPlan)
        {
            Description = mealPlan?.Description?? throw new ArgumentNullException(nameof(mealPlan));
            Calories = mealPlan?.Calories ?? throw new ArgumentNullException(nameof(mealPlan));
            Fats = mealPlan?.Fats ?? throw new ArgumentNullException(nameof(mealPlan)); ;
            Proteins = mealPlan?.Proteins ?? throw new ArgumentNullException(nameof(mealPlan)); ;
            Carbohydrates = mealPlan?.Carbohydrates ?? throw new ArgumentNullException(nameof(mealPlan)); ; 

        }

        public MealPlan ToEntity()
        {
            return new MealPlan()
            {
                Description = this.Description,
                Calories=this.Calories,
                Fats = this.Fats,
                Proteins = this.Proteins,
                Carbohydrates = this.Carbohydrates
            };

        }
       

        
    }
}

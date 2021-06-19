using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner.Domain.Entities
{
    public class MealPlan
    {
        public int Calories { get; set; }
        public int Fats { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
       public string Description { get; set; }
    }
}

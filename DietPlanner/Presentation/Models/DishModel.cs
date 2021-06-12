using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Domain.Entities;

namespace DietPlanner.Presentation.Models
{
    


    public class DishModel
    {
        public string Name { get; set; }
        public int Calories { get; set; }

        public DishModel()
        {


        }
        public DishModel(Domain.Entities.Dish dish)
        {
            Name = dish?.Name?? throw new ArgumentNullException(nameof(dish));
            Calories = dish?.Calories ?? throw new ArgumentNullException(nameof(dish)); ;

        }

        public Dish ToEntity()
        {
            return new Dish()
            {
                Name = this.Name,
                Calories=this.Calories
            };

        }
       

        
    }
}

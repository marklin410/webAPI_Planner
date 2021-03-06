using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodJournal.Domain.Entities;

namespace FoodJournal.Presentation.Models
{
    


    public class DishModel
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Fats { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }

        public DishModel()
        {


        }
        public DishModel(Domain.Entities.Dish dish)
        {
            Name = dish?.Name?? throw new ArgumentNullException(nameof(dish));
            Calories = dish?.Calories ?? throw new ArgumentNullException(nameof(dish));
            Fats = dish?.Fats ?? throw new ArgumentNullException(nameof(dish)); ;
            Proteins = dish?.Proteins ?? throw new ArgumentNullException(nameof(dish)); ;
            Carbohydrates = dish?.Carbohydrates ?? throw new ArgumentNullException(nameof(dish)); ; 

        }

        public Dish ToEntity()
        {
            return new Dish()
            {
                Name = this.Name,
                Calories=this.Calories,
                Fats = this.Fats,
                Proteins = this.Proteins,
                Carbohydrates = this.Carbohydrates

            };

        }
       

        
    }
}

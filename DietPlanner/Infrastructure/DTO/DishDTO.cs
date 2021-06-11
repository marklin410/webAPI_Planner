using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Domain.Entities;

namespace DietPlanner.Infrastructure.DTO
{
    public class DishDTO
    {
        public int id { get; set; }
        public int Calories { get; set; }
        public String Name { get; set; }
        public Dish ToEntity()
        {
            return new Dish()
            {
                Name = Name,
                Calories = Calories
            };
        }

    }
}

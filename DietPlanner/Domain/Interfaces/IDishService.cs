using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Domain.Entities;

namespace DietPlanner.Domain.Interfaces
{
    public interface IDishService
    {
        Task<Dish[]> GetDishes();
        Task AddDish(Dish dish);
    }
}

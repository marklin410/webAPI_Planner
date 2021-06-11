using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Domain.Entities;
using DietPlanner.Domain.Interfaces;

namespace DietPlanner.Domain.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;
        public DishService(IDishRepository repository)
        {
            _dishRepository = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        public async Task AddDish(Dish dish)
        {
            if(dish==null)
                throw new ArgumentNullException(nameof(dish));
            await _dishRepository.AddDish(dish);
        }

        public async Task<Dish[]> GetDishes()
        {
            return await _dishRepository.GetDishes();
        }
    }
}

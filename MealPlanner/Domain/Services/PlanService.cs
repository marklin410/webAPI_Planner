using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner.Domain.Entities;
using MealPlanner.Domain.Interfaces;

namespace MealPlanner.Domain.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        public PlanService(IPlanRepository repository)
        {
            _planRepository = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        public async Task<MealPlan[]> GetPlans(UserData userData)
        {
            int k;
            double proteins, fats, carbohydrates, calories;

            if (userData.Gender.Equals('ж'))
            {
                k = -161;
            }
            else
            {
                k = 5;
            }
            fats = 0.2;
            if (userData.Goal < 1)
            {
                proteins = 0.4;
                carbohydrates = 0.4;
            }
            else if (userData.Goal == 1.0)
            {
                proteins = 0.5;
                carbohydrates = 0.3;
            }
            else
            {
                proteins = 0.3;
                carbohydrates = 0.5;
            }
            calories = Math.Round(((10 * userData.Weight) + 6.25 * userData.Height + 5 * userData.Age + k) * userData.ActivityLevel*userData.Goal);
            MealPlan userMealPlan = new MealPlan()
            {

                Calories = (int)calories,
                Proteins = (int)(calories * proteins / 4),
                Fats = (int)(calories*fats/9),
                Carbohydrates = (int)(calories*carbohydrates/4)
                
            };
            return await _planRepository.GetPlans(userMealPlan);
        }
    }
}

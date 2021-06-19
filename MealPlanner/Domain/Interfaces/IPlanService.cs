using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner.Domain.Entities;

namespace MealPlanner.Domain.Interfaces
{
    public interface IPlanService
    {
        Task<MealPlan[]> GetPlans(UserData userData);

    }
}

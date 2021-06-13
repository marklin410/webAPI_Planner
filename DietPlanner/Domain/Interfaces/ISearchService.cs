using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Domain.Entities;

namespace DietPlanner.Domain.Interfaces
{
    public interface ISearchService
    {
        Task<Dish[]> GetDishesByName(Search search);
    }
}

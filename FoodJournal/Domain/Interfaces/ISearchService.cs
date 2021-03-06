using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodJournal.Domain.Entities;

namespace FoodJournal.Domain.Interfaces
{
    public interface ISearchService
    {
        Task<Dish[]> GetDishesByName(Search search);
    }
}

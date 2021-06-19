using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodJournal.Domain.Entities;

namespace FoodJournal.Domain.Interfaces
{
    public interface IDishRepository
    {
        Task<Dish[]> GetDishes();
        Task<Dish[]> GetDishesByName(Search search);
       // Task AddDish(Dish dish);
        Task AddEntry(JournalEntry journalEntry);
    }
}

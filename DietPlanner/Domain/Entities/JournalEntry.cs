using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietPlanner.Domain.Entities
{
    public class JournalEntry
    {
        public int DishID { get; set; }
        public DateTime dateTime { get; set; }
    }
}

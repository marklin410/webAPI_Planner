using DietPlanner.Domain.Entities;
using System;

namespace DietPlanner.Presentation.Models
{
    public class JournalModel
{

         public int DishID { get; set; }
         public DateTime dateTime { get; set; }

            public JournalModel()
            {
            
            }
            public JournalModel(JournalEntry journalEntry)
            {
                DishID = journalEntry?.DishID ?? throw new ArgumentNullException(nameof(journalEntry));
                dateTime = DateTime.Now;
            }

            public JournalEntry ToEntity()
            {
                return new JournalEntry()
                {
                    DishID = this.DishID,
                    dateTime = this.dateTime
                };
            }
        }
    
}

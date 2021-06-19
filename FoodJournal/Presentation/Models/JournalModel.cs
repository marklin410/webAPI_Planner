using FoodJournal.Domain.Entities;
using System;

namespace FoodJournal.Presentation.Models
{
    public class JournalModel
{

         public int DishID { get; set; }

            public JournalModel()
            {
            
            }
            public JournalModel(JournalEntry journalEntry)
            {
                DishID = journalEntry?.DishID ?? throw new ArgumentNullException(nameof(journalEntry));
            }

            public JournalEntry ToEntity()
            {
                return new JournalEntry()
                {
                    DishID = this.DishID,
                };
            }
        }
    
}

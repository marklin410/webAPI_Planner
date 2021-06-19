using FoodJournal.Domain.Entities;
using System;

namespace FoodJournal.Presentation.Models
{
    public class SearchModel
{

         public string SearchName { get; set; }

            public SearchModel()
            {
            
            }
            public SearchModel(Search search)
            {
                SearchName = search?.SearchName ?? throw new ArgumentNullException(nameof(search));
            }

            public Search ToEntity()
            {
                return new Search()
                {
                    SearchName = this.SearchName,
                };

            }
        }
    
}

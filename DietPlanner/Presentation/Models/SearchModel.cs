using DietPlanner.Domain.Entities;
using System;

namespace DietPlanner.Presentation.Models
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

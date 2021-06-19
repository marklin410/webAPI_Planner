using MealPlanner.Domain.Entities;
using System;

namespace MealPlanner.Presentation.Models
{
    public class UserDataModel
{

        public int Weight { get; set; }
        public int Height { get; set; }
        public double ActivityLevel { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }

        public double Goal { get; set; }

        public UserDataModel()
            {
            
            }
            public UserDataModel(UserData userData)
            {
                Weight = userData?.Weight ?? throw new ArgumentNullException(nameof(userData));
                Height = userData?.Height ?? throw new ArgumentNullException(nameof(userData));
                ActivityLevel = userData?.ActivityLevel ?? throw new ArgumentNullException(nameof(userData));
                Gender = userData?.Gender ?? throw new ArgumentNullException(nameof(userData));
                Goal = userData?.Goal ?? throw new ArgumentNullException(nameof(userData));
            }

            public UserData ToEntity()
            {
                return new UserData()
                {
                    Weight = this.Weight,
                    Height = this.Height,
                    ActivityLevel = this.ActivityLevel,
                    Gender = this.Gender,
                    Goal = this.Goal,
                };

            }
        }
    
}

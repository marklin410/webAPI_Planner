using System;


namespace MealPlanner.Domain.Entities
{
	public class UserData
	{
			public int Weight { get; set; }
			public int Height { get; set; }
			public double ActivityLevel { get; set; }
			public int Age { get; set; }
			public char Gender { get; set; }

			public double Goal { get; set; }
	}
}

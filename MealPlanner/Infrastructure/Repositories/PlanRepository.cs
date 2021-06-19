using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner.Domain.Entities;
using MealPlanner.Domain.Interfaces;
using MealPlanner.Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MealPlanner.Infrastructure.Repositories
{

    public class PlanRepository : IPlanRepository
    {
        private const string CONNECTION_STRING_NAME = "DietPlanner";
        private readonly IConfiguration _configuration;
        private readonly ILogger<PlanRepository> _logger;

        private readonly string connection_string_docker = "Server=host.docker.internal;Database=DietPlanner;uid=sa;pwd=Test1234;";
        private const string connection_string_local = "Server=DESKTOP-VEKOKSB;Database=DietPlanner;uid=sa;pwd=Test1234";
        public PlanRepository(IConfiguration configuration, ILogger<PlanRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

      

        public async Task<MealPlan[]> GetPlans(MealPlan userMealPlan)
        {
            List<MealPlanDTO> dishes = new List<MealPlanDTO>();

            using (var connection = new SqlConnection(connection_string_local))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand($"SELECT * FROM dbo.Meal_Plans WHERE " +
                    $"(Calories > {userMealPlan.Calories-50}) AND (Calories > {userMealPlan.Calories + 50})" +
                    $"AND (Fats>{userMealPlan.Fats-10}) AND (Fats<{userMealPlan.Fats + 10}) " +
                    $"AND (Proteins>{userMealPlan.Proteins - 10}) AND (Proteins<{userMealPlan.Proteins + 10})" +
                    $"AND (Carbohydrates>{userMealPlan.Carbohydrates - 10}) AND (Carbohydrates<{userMealPlan.Carbohydrates + 10})", connection))
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        dishes.Add(new MealPlanDTO()
                        {
                            id = int.Parse(reader["id"].ToString()),
                            Description = reader["Description"].ToString(),
                            Calories = int.Parse(reader["Calories"].ToString()),
                            Fats = int.Parse(reader["Fats"].ToString()),
                            Proteins = int.Parse(reader["Proteins"].ToString()),
                            Carbohydrates = int.Parse(reader["Carbohydrates"].ToString())
                        });

                    }
                }
            }
            try
            {
                return dishes.Select(e => e.ToEntity()).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при запросе из бд");
                return dishes.Select(e => e.ToEntity()).ToArray();
            }
        }
    }
}

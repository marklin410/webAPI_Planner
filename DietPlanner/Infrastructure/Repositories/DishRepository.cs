using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Domain.Entities;
using DietPlanner.Domain.Interfaces;
using DietPlanner.Infrastructure.DTO;
using Microsoft.Extensions.Configuration;

namespace DietPlanner.Infrastructure.Repositories
{

    public class DishRepository : IDishRepository
    {
        private const string CONNECTION_STRING_NAME = "DietPlanner";
        private readonly IConfiguration _configuration;

        private readonly string connection_string_docker = "Server=host.docker.internal;Database=DietPlanner;uid=sa;pwd=Test1234;";
        private const string connection_string_local = "Server=DESKTOP-VEKOKSB;Database=DietPlanner;uid=sa;pwd=Test1234";
        public DishRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task AddDish(Dish dish)
        {
            if (dish == null)
                throw new ArgumentNullException(nameof(dish));
            using (var connection = new SqlConnection(connection_string_docker))
            {
                 await connection.OpenAsync();
                using (var cmd = new SqlCommand($"INSERT INTO dbo.Dishes (Name, Calories) VALUES ('{dish.Name}' , {dish.Calories})", connection))
                {
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task<Dish[]> GetDishes()
        {
            List<DishDTO> dishes = new List<DishDTO>();

        using (var connection = new SqlConnection(connection_string_docker))
        {
            await connection.OpenAsync();
            using (var cmd = new SqlCommand("SELECT * FROM dbo.Dishes", connection))
            {
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    dishes.Add(new DishDTO()
                    {
                        id = int.Parse(reader["id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Calories = int.Parse(reader["Calories"].ToString())
                    });

                }
            }
        }
            return dishes.Select(e => e.ToEntity()).ToArray();
        }
    }
}

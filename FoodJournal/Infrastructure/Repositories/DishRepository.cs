using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FoodJournal.Domain.Entities;
using FoodJournal.Domain.Interfaces;
using FoodJournal.Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FoodJournal.Infrastructure.Repositories
{

    public class DishRepository : IDishRepository
    {
        private const string CONNECTION_STRING_NAME = "DietPlanner";
        private readonly IConfiguration _configuration;
        private readonly ILogger<DishRepository> _logger;

       
        public DishRepository(IConfiguration configuration, ILogger<DishRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task AddEntry(JournalEntry journalEntry)
        {
            if (journalEntry == null)
                throw new ArgumentNullException(nameof(journalEntry));
            using (var connection = new SqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
              
                using (var cmd = new SqlCommand($"INSERT INTO dbo.User_Journal (DishId, Date) VALUES ({journalEntry.DishID},  CURDATE() )", connection))
                {
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        _logger.LogError(e, "Произошла ошибка при запросе из бд");

                    }

                }
            }
        }

        public async Task<Dish[]> GetDishes()
        {
            List<DishDTO> dishes = new List<DishDTO>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
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
                            Calories = int.Parse(reader["Calories"].ToString()),
                            Fats = int.Parse(reader["Fats"].ToString()),
                            Proteins = int.Parse(reader["Proteins"].ToString()),
                            Carbohydrates = int.Parse(reader["Carbohydrates"].ToString())

                        });

                    }
                }
            }
            return dishes.Select(e => e.ToEntity()).ToArray();
        }



        public async Task<Dish[]> GetDishesByName(Search search)
        {
            List<DishDTO> dishes = new List<DishDTO>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
        {
            await connection.OpenAsync();
            string select_query = "SELECT * FROM dbo.Dishes WHERE Name LIKE '%"+search.SearchName+"%'";
            using (var cmd = new SqlCommand(select_query, connection))
            {
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    dishes.Add(new DishDTO()
                    {
                        id = int.Parse(reader["id"].ToString()),
                        Name = reader["Name"].ToString(),
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
            }catch(Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при запросе из бд");
                return dishes.Select(e => e.ToEntity()).ToArray();
            }
        }
    }
}

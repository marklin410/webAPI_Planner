using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Domain.Entities;
using DietPlanner.Domain.Interfaces;
using DietPlanner.Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DietPlanner.Infrastructure.Repositories
{

    public class DishRepository : IDishRepository
    {
        private const string CONNECTION_STRING_NAME = "DietPlanner";
        private readonly IConfiguration _configuration;
        private readonly ILogger<DishRepository> _logger;

        private readonly string connection_string_docker = "Server=host.docker.internal;Database=DietPlanner;uid=sa;pwd=Test1234;";
        private const string connection_string_local = "Server=DESKTOP-VEKOKSB;Database=DietPlanner;uid=sa;pwd=Test1234";
        public DishRepository(IConfiguration configuration, ILogger<DishRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task AddEntry(JournalEntry journalEntry)
        {
            if (journalEntry == null)
                throw new ArgumentNullException(nameof(journalEntry));
            using (var connection = new SqlConnection(connection_string_local))
            {
                await connection.OpenAsync();
                /*{journalEntry.DishID}  {journalEntry.dateTime.ToString("yyyyMMdd HH:mm:ss")}*/
                using (var cmd = new SqlCommand($"INSERT INTO dbo.User_Journal (DishId, Date) VALUES ({journalEntry.DishID},  '2019-08-17' )", connection))
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

        using (var connection = new SqlConnection(connection_string_local))
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

        using (var connection = new SqlConnection(connection_string_local))
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

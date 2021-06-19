using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodJournal.Domain.Interfaces;
using FoodJournal.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FoodJournal.Presentation.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {

        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;
        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]SearchModel model)
        {
            _logger.LogInformation("Запрос блюд по названию");
            if (model == null)
            {
                _logger.LogError("Отсутствуют данные для добавления");
                return StatusCode(StatusCodes.Status500InternalServerError, "Пустой поисковый запрос");
            }
            try
            {
                _logger.LogInformation("Начало запроса блюд");
                return Ok((await _searchService.GetDishesByName(model.ToEntity())).Select(dish => new DishModel(dish)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при поисковом запросе");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

    }
}

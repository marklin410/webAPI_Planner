﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Domain.Interfaces;
using DietPlanner.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DietPlanner.Presentation.Controllers
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
            _logger.LogInformation("Exec to get dish by name");
            if (model == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пустой поисковый запрос");
            }
            try
            {
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
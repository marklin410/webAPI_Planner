using Microsoft.AspNetCore.Mvc;
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
    [Route("dish")]
    public class DishController : ControllerBase
    {

        private readonly IDishService _dishService;
        private readonly ILogger<DishController> _logger;
        public DishController(IDishService dishService, ILogger<DishController> logger)
        {
            _dishService = dishService ?? throw new ArgumentNullException(nameof(dishService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Exec to get dish");
            try
            {
                return Ok((await _dishService.GetDishes()).Select(dish => new DishModel(dish)));
            } catch(Exception e)
            {
                _logger.LogError(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]DishModel model)
        {
            if(model == null)
            {
                return BadRequest("Input data");
            }
            await _dishService.AddDish(model.ToEntity());
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}

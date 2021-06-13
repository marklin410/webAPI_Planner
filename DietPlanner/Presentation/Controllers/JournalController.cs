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
    [Route("journal")]
    public class JournalController : ControllerBase
    {

        private readonly IJournalService _journalService;
        private readonly ILogger<JournalController> _logger;
        public JournalController(IJournalService journalService, ILogger<JournalController> logger)
        {
            _journalService = journalService ?? throw new ArgumentNullException(nameof(journalService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /*[HttpGet]
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
        }*/

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]JournalModel model)
        {
            if(model == null)
            {
                return BadRequest("Input data");
            }
            try
            {
                await _journalService.AddEntry(model.ToEntity());
            }catch(Exception e)
            {
                return BadRequest(model.ToEntity().dateTime.ToString("yyyyMMdd HH:mm:ss"));
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


    }
}

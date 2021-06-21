using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner.Domain.Interfaces;
using MealPlanner.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace MealPlanner.Presentation.Controllers
{
    [ApiController]
    [Route("plan")]
    public class MealPlanController : ControllerBase
    {

        private readonly IPlanService _planService;
        Logger _logger;
        public MealPlanController(IPlanService planService, ILogger<MealPlanController> logger)
        {
            _planService = planService ?? throw new ArgumentNullException(nameof(planService));
           
            _logger = new LoggerConfiguration()
                     .WriteTo.Sentry("https://17979ac0d0f111eb84b09a4090bb6627@o828966.ingest.sentry.io/5813633")
                     .WriteTo.Console()
                     .Enrich.FromLogContext()
                     .CreateLogger();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDataModel model)
        {
            _logger.Information("Запрос планов питания");
            if (model == null)
            {
                _logger.Error("Отсутствуют данные для запроса");
                return StatusCode(StatusCodes.Status500InternalServerError, "Пустой запрос");
            }
            try
            {
                return Ok((await _planService.GetPlans(model.ToEntity())).Select(mealPlan => new MealPlanModel(mealPlan)));
            }
            catch (Exception e)
            {
                _logger.Error(e, "Произошла ошибка при запросе");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

    }
}

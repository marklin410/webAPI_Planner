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
using Serilog.Core;

namespace FoodJournal.Presentation.Controllers
{
    [ApiController]
    [Route("journal")]
    public class JournalController : ControllerBase
    {

        private readonly IJournalService _journalService;
        
        public JournalController(IJournalService journalService, ILogger<JournalController> logger)
        {
            _journalService = journalService ?? throw new ArgumentNullException(nameof(journalService));
            
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]JournalModel model)
        {
            var _logger = new LoggerConfiguration()
                      .WriteTo.Sentry("https://17979ac0d0f111eb84b09a4090bb6627@o828966.ingest.sentry.io/5813633")
                      .WriteTo.Console()
                      .Enrich.FromLogContext()
                      .CreateLogger();
            _logger.Error("Плановая ошибка");
            if (model == null)
            {
                _logger.Error( "Пустой запрос");
                return BadRequest("Input data");
            }
            try
            {
                _logger.Information("Добавление записи");
                await _journalService.AddEntry(model.ToEntity());
                _logger.Information("Запись добавлена");
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception e)
            {
                _logger.Error("Ошибка при добавлении");
                return BadRequest(e.Message);
            }
            
        }


    }
}

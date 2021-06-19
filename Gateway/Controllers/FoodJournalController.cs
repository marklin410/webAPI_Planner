using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [Route("api/v1/food-journal")]
    [ApiController]
    public class FoodJournalController : ControllerBase
    {
        IConfiguration _configuration;
        public FoodJournalController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
       
        [HttpPut]
        public async Task<IActionResult> SaveDish(object value)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = _configuration.GetSection("FoodJournalURI").Value;
                    var resMsg = await client.PutAsJsonAsync($"{url}journal",value); 
                    var res = await resMsg.Content.ReadAsStringAsync();
                    return Ok(res);
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

        }
    }
}

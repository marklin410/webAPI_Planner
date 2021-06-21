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
    [Route("api/v2/meal-planner")]
    [ApiController]
    public class MealPlannerController : ControllerBase
    {
        IConfiguration _configuration;
        public MealPlannerController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
       
        [HttpPost]
        public async Task<IActionResult> GetPlan(object value)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = _configuration.GetSection("MealPlannerURI").Value;
                    var resMsg = await client.PutAsJsonAsync($"{url}plan",value);
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

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
    [Route("api/v1/diet-planner")]
    [ApiController]
    public class DietPlannerController : ControllerBase
    {
        IConfiguration _configuration;
        public DietPlannerController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        [HttpGet]
       public async Task<IActionResult> GetDietPlanner()
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = "http://172.17.0.2/";
                        //_configuration.GetSection("DietPlannerURI").Value;
                    var resMsg = await client.GetAsync($"{url}dish");
                    var res = await resMsg.Content.ReadAsStringAsync();
                    return Ok(res);
                }

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> SaveDish(object value)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = _configuration.GetSection("DietPlannerURI").Value;
                    var resMsg = await client.PutAsJsonAsync($"{url}dish",value);
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

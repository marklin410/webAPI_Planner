using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [Route("api/v1/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        IConfiguration _configuration;
        public SearchController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
       
        [HttpPut]
        public async Task<IActionResult> StartSearch(object value)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = _configuration.GetSection("FoodJournalURI").Value;
                    var resMsg = await client.PutAsJsonAsync($"{url}search",value);
                    var res = await resMsg.Content.ReadAsStringAsync();
                    return Ok(res);
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}

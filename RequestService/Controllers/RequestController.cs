using Microsoft.AspNetCore.Mvc;

namespace RequestService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        // GET /api/request
        [HttpGet]
        public async Task<ActionResult> MakeARequest()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7099/api/response/25");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> ResponseService returned SUCCESS");
                return Ok();
            }

            Console.WriteLine("--> ResponseService returned FAILURE");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
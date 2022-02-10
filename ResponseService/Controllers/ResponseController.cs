using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        // GET /api/response/100
        [Route("{id:int}")]
        [HttpGet]
        public ActionResult GetAResponse(int id)
        {
            Random random = new();
            var randomInteger = random.Next(1, 101);
            if (randomInteger >= id)
            {
                Console.WriteLine("--> FAILURE - Generate a HTTP 500");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Console.WriteLine("--> SUCCESS - Generate a HTTP 200");
            return Ok();
        }
    }
}
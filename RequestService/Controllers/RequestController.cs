using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;

namespace RequestService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ClientPolicy _clientPolicy;
        private readonly IHttpClientFactory _httpClientFactory;

        public RequestController(ClientPolicy clientPolicy, IHttpClientFactory httpClientFactory)
        {
            _clientPolicy = clientPolicy;
            _httpClientFactory = httpClientFactory;
        }

        // GET /api/request/25
        [Route("{id:int}")]
        [HttpGet]
        public async Task<ActionResult> MakeARequest(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(
                () => client.GetAsync($"https://localhost:7099/api/response/{id}"));

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
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("api/server")]
    public class Server : Controller
    {
        [HttpGet("run")]
        public IActionResult Run()
        {
            return Ok("Ok");
        }
    }
}
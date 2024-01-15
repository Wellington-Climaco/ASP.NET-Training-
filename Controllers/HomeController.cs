using Microsoft.AspNetCore.Mvc;

namespace TreinandoApi.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices] IConfiguration config)
        {
            var env = config.GetValue<string>("Env");
            return Ok(new {Environment = env});
        }

    }
}

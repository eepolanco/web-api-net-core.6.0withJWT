using Microsoft.AspNetCore.Mvc;

namespace I_banking___Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    // [Route("GetExample")]
    public IActionResult GetExample()
        {
            return StatusCode(200, "true");

        }

        [HttpPost]
    // [Route("GetExample")]
    public IActionResult PostExample()
        {
            return StatusCode(200, "true");

        }
}

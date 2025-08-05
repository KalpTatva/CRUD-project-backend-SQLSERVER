using Microsoft.AspNetCore.Mvc;

namespace webapiproject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Hello, how are you!" }); // Returns JSON
    }
}

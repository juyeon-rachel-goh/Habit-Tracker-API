using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Newtonsoft.Json;

namespace Api.Controllers;

[ApiController]
[Route("api/contacts")]
public class AuthController : ControllerBase
{

    public AuthController(ILogger<AuthController> logger)
    {
        // _logger = logger;
    }

    [HttpGet]   
    [Route("")]
    public OkObjectResult Get() 
    {
        return Ok("hellooo");
    }
}

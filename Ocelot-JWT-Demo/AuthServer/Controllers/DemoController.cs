using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthServer.Controllers;

[Route("api/[controller]/")]
public class DemoController : Controller
{
    private IOptions<Audience> _settings;

    public DemoController(IOptions<Audience> settings)
    {
        _settings = settings;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Get()
    {
        return Ok("Hello from the Auth Server! / using Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)/ ");
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult Get(int id)
    {
        return Ok($"No Authorization Here - Hello from the Auth Server (with a parameter) - {id}");
    }
}
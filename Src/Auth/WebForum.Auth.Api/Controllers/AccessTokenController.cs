using Microsoft.AspNetCore.Mvc;

namespace WebForum.Auth.Api.Controllers;

[ApiController]
[Route("access-token")]
public class AccessTokenController : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        throw new NotImplementedException();
    }
}
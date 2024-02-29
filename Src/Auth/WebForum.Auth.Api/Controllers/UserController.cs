using Microsoft.AspNetCore.Mvc;

namespace WebForum.Auth.Api.Controllers;

[ApiController]
[Route("user")]
public sealed class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult Create()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update()
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
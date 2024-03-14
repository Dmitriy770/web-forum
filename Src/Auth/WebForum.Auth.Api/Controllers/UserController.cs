using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebForum.Auth.Api.Requests;
using WebForum.Auth.Api.Responses;
using WebForum.Auth.Application.Commands;

namespace WebForum.Auth.Api.Controllers;

[ApiController]
[Route("user")]
public sealed class UserController(
    ISender sender
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userId = await sender.Send(new CreateUserCommand(request.Login, request.Password), cancellationToken);

        return Ok(new CreateUserResponse(userId));
    }

    [HttpPut]
    public IActionResult Update()
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
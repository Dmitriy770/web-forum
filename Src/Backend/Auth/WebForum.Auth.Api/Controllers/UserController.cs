using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebForum.Auth.Api.Authorization;
using WebForum.Auth.Api.ExceptionFilters;
using WebForum.Auth.Api.Requests;
using WebForum.Auth.Api.Responses;
using WebForum.Auth.Application.Commands;

namespace WebForum.Auth.Api.Controllers;

[ApiController]
[Route("user")]
[UserExceptionFilter]
public sealed class UserController(
    ISender sender
) : ControllerBase
{
    [HttpPost]
    [NoAuthorization]
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteUserCommand(id), cancellationToken);
        return Ok();
    }
}
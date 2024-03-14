using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebForum.Auth.Api.Requests;
using WebForum.Auth.Api.Responses;
using WebForum.Auth.Application.Queries;

namespace WebForum.Auth.Api.Controllers;

[ApiController]
[Route("access-token")]
public class AccessTokenController(
    ISender sender
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(GetAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var token = await sender.Send(
            new GetAccessTokenQuery(request.Login, request.Password), cancellationToken
        );

        return Ok(new GetAccessTokenResponse());
    }
}
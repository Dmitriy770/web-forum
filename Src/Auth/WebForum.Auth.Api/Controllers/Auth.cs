using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebForum.Auth.Api.Requests;
using WebForum.Auth.Api.Responses;
using WebForum.Auth.Application.Queries;

namespace WebForum.Auth.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class Auth(
    ISender sender
) : ControllerBase
{
    [HttpPost("access-token")]
    public async Task<IActionResult> Get(
        GetAccessTokenRequest request,
        CancellationToken cancellationToken)
    {
        var (token, expiresIn) = await sender.Send(
            new GetAccessTokenQuery(request.Login, request.Password), cancellationToken
        );
        
        HttpContext.Response.Cookies.Append("some-cookies", token);

        return Ok(new GetAccessTokenResponse(expiresIn));
    }
}
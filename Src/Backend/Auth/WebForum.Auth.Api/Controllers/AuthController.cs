using System.Security.Authentication;
using MediatR;
using WebForum.Auth.Api.Authorization;
using WebForum.Auth.Api.Requests;
using WebForum.Auth.Api.Responses;
using WebForum.Auth.Application.Queries;

namespace WebForum.Auth.Api.Controllers;

public sealed class AuthController
{
    public void Register(WebApplication app)
    {
        var group = app.MapGroup("/api/auth/access-token")
            .AddEndpointFilter<AuthorizationFilter>();

        group.MapPost("/", GetAccessToken)
            .Produces<GetAccessTokenResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        group.MapDelete("/", DeleteAccessToken)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    [NoAuthorization]
    private async Task<IResult> GetAccessToken(
        HttpContext context,
        GetAccessTokenRequest request,
        CancellationToken cancellationToken,
        ISender sender
    )
    {
        try
        {
            var (token, authInfo) = await sender.Send(
                new GetAccessTokenQuery(request.Login, request.Password), cancellationToken
            );

            var cookieOptions = new CookieOptions
            {
                Secure = false,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                IsEssential = true
            };
            context.Response.Cookies.Append("some-cookies", token, cookieOptions);

            return Results.Ok(new GetAccessTokenResponse(
                authInfo.Id,
                authInfo.Login,
                authInfo.Permissions.ToString(),
                authInfo.ExpiresIn
            ));
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    private IResult DeleteAccessToken(HttpContext context)
    {
        try
        {
            context.Response.Cookies.Delete("some-coolies");
            return Results.Ok();
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    private IResult HandleException(Exception exception)
    {
        return exception switch
        {
            InvalidCredentialException => Results.BadRequest(),
            _ => Results.Problem(statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}
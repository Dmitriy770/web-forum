using System.Net;
using MediatR;
using WebForum.Auth.Application.Queries;

namespace WebForum.Auth.Api.Authorization;

public class AuthorizationFilter(
    ISender sender
) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var endpoint = context.HttpContext.GetEndpoint();
        if (endpoint is null)
        {
            return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
        }


        if (endpoint.Metadata.GetMetadata<NoAuthorizationAttribute>() is not null)
        {
            return await next(context);
        }

        context.HttpContext.Request.Cookies.TryGetValue("some-cookies", out var accessToken);
        if (accessToken is null)
        {
            return Results.Problem(statusCode: StatusCodes.Status401Unauthorized);
        }
        
        var user = await sender.Send(new GetUserByAccessTokenQuery(accessToken));
        if (user is null)
        {
            return Results.Problem(statusCode: StatusCodes.Status401Unauthorized);
        }

        context.HttpContext.Items.Add("UserId", user.Id);
        var authorizationAttribute = endpoint.Metadata.GetMetadata<AuthorizationAttribute>();
        if (authorizationAttribute is null)
        {
            return await next(context);
        }

        var requiredPermissions = authorizationAttribute.Permissions;
        if (((int)requiredPermissions & (int)user.Permissions) != 0)
        {
            return await next(context);
        }
        return Results.Problem(statusCode: StatusCodes.Status403Forbidden);
    }
}
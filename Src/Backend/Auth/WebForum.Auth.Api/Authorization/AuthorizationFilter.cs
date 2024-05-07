using MediatR;
using Microsoft.AspNetCore.Authentication;
using WebForum.Auth.Application.Queries;

namespace WebForum.Auth.Api.Authorization;

public class AuthorizationFilter(
    ISender sender
    ) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if(context.HttpContext.GetEndpoint() is {} endpoint )
        {
            if (endpoint.Metadata.GetMetadata<NoAuthorizationAttribute>() is not null)
            {
                return await next(context);
            }
            
            context.HttpContext.Request.Cookies.TryGetValue("some-cookies", out var accessToken);
            if (accessToken is null)
            {
                return Results.Unauthorized();
            }

            var user = await sender.Send(new GetUserByAccessTokenQuery(accessToken));
            if (user is null)
            {
                return Results.Unauthorized();
            }
            context.HttpContext.Items.Add("UserId", user.Id);

            if (endpoint.Metadata.GetMetadata<AuthorizationAttribute>() is {} authorizationAttribute )
            {
                var requiredPermissions = authorizationAttribute.Permissions;
                
                if (((int)requiredPermissions & (int)user.Permissions) != 0)
                {
                    return await next(context);
                }
                else
                {
                    return Results.Problem(statusCode: 403);
                }
            }
            else
            {
                return await next(context);
            }
        }

        return Results.Unauthorized();
    }
}
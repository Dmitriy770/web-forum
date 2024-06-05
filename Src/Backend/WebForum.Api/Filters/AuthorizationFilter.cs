using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using WebForum.Application.Commands;
using WebForum.Domain.Models.UserModels;

namespace WebForum.Api.Filters;

public class AuthorizationFilter(
    ISender sender
) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var endpoint = context.HttpContext.GetEndpoint();
        var authorizationAttribute = endpoint?.Metadata.GetMetadata<AuthorizationAttribute>();
        if (authorizationAttribute is null)
        {
            return;
        }

        if (context.HttpContext.Request.Cookies.TryGetValue("key", out var accessToken))
        {
            var user = await sender.Send(new ValidateLogInCommand(accessToken));
            context.HttpContext.Items[nameof(User)] = user;
        }
    }
}

// TODO доабвить конфиг для jwt вмесе с именем cookie
// TODO создать слой infrastructure
// TODO зарегать все сервисы
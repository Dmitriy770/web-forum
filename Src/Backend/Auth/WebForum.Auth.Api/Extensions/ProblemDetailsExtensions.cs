using Microsoft.AspNetCore.Mvc;

namespace WebForum.Auth.Api.Extensions;

public static class ProblemDetailsExtensions
{
    public static ActionResult ToActionResult(this ProblemDetails details)
    {
        return new ObjectResult(details)
        {
            StatusCode = details.Status
        };
    }
}
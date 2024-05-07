using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebForum.Auth.Api.Extensions;
using WebForum.Auth.Domain.Exceptions;

namespace WebForum.Auth.Api.ExceptionFilters;

[AttributeUsage(AttributeTargets.Class)]
public sealed class UserExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var problemFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
        
        switch (context.Exception)
        {
            case ArgumentException:
            case UserAlreadyExistsException:
                context.Result = problemFactory.CreateProblemDetails(
                    context.HttpContext,
                    (int)HttpStatusCode.BadRequest,
                    context.Exception.Message
                ).ToActionResult();
                break;
            case UserNotFoundException:
                context.Result = problemFactory.CreateProblemDetails(
                    context.HttpContext,
                    (int)HttpStatusCode.NotFound,
                    context.Exception.Message
                ).ToActionResult();
                break;
            default:
                context.Result = problemFactory.CreateProblemDetails(
                    context.HttpContext,
                    (int)HttpStatusCode.InternalServerError,
                    context.Exception.Message
                ).ToActionResult();
                break;
        }
    }
}
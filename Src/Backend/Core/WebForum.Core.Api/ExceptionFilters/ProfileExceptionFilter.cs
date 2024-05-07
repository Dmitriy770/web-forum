using Microsoft.AspNetCore.Mvc.Filters;
using WebForum.Core.Domain.Exceptions;

namespace WebForum.Core.Api.ExceptionFilters;

public class ProfileExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ProfileNotFoundException:
                Results.NotFound().ExecuteAsync(context.HttpContext);
                break;
            case ProfileAlreadyExistsException:
                Results.Conflict().ExecuteAsync(context.HttpContext);
                break;
            default:
                Results.Problem().ExecuteAsync(context.HttpContext);
                break;
        }
    }
}
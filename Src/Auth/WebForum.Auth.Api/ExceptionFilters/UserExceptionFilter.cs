using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebForum.Auth.Api.ExceptionFilters;

[AttributeUsage(AttributeTargets.Class)]
public class UserExceptionFilter(
    ProblemDetailsFactory problemFactory
    ) : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // factory.CreateProblemDetails(context.HttpContext, )
        context.Result = new ProblemDetails().;
        
        context.Result = new ContentResult
        {
            StatusCode = 2,
            Content = new ProblemDetails
            {
                
            }.ToString(),
            ContentType = HttpContent
        }
    }
}
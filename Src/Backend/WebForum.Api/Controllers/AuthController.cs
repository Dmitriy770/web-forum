using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WebForum.Application.Commands;
using WebForum.Domain.Models.AuthModels;

namespace WebForum.Api.Controllers;

public class AuthController(
    ISender sender
) : ODataController
{
    public async Task<ActionResult> Post(
        [FromBody] AuthDraft authDraft,
        CancellationToken cancellationToken)
    {
        var (auth, token) = await sender.Send(new LogInCommand(authDraft), cancellationToken);

        HttpContext.Response.Cookies.Append("key", token);

        return Created(auth);
    }
    
    public ActionResult Delete(
        [FromRoute] Guid key)
    {
        HttpContext.Response.Cookies.Delete("key");

        return NoContent();
    }
}
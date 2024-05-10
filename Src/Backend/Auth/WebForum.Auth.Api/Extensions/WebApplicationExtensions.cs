using WebForum.Auth.Api.Controllers;

namespace WebForum.Auth.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapWebForumAuthApi(this WebApplication app)
    {
        new AuthController().Register(app);
        new UserController().Register(app);
        
        return app;
    }
}
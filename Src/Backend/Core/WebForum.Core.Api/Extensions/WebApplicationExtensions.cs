using WebForum.Core.Api.Controllers;

namespace WebForum.Core.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapWebForumCoreApi(this WebApplication app)
    {
        new ProfileController().Register(app);
        new PostController().Register(app);

        return app;
    }
}
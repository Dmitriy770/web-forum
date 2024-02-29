namespace WebForum.Auth.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddWebForumAuth(this IApplicationBuilder app)
    {
        return app;
    }
}
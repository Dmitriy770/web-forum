using Microsoft.AspNetCore.CookiePolicy;

namespace WebForum.Auth.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseWebForumAuth(this WebApplication app)
    {
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.SameAsRequest
        });

        app.UseAuthentication();
        app.UseAuthorization();

        
        return app;
    }
}
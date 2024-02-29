namespace WebForum.Auth.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebForumAuth(this IServiceCollection services)
    {
        services.AddControllers();
        
        return services;
    }
}
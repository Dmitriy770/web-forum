using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Application.Queries;
using WebForum.Auth.Infrastructure.Options;
using WebForum.Auth.Infrastructure.Utilities;

namespace WebForum.Auth.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebForumAuth(this IServiceCollection services)
    {
        // Api
        services.AddControllers();

        // Application
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAccessTokenQuery).Assembly));
        
        // Infrastructure
        services.AddOptions<JwtOptions>(nameof(JwtOptions));
        services.AddScoped<IHasher, Hasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        
        return services;
    }
}
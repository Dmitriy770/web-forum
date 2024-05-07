using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Application.Queries;
using WebForum.Auth.Infrastructure.Options;
using WebForum.Auth.Infrastructure.Repositories;
using WebForum.Auth.Infrastructure.Utilities;

namespace WebForum.Auth.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebForumAuth(this IServiceCollection services, IConfigurationRoot config)
    {
        //Configuration
        services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));
        
        // Api
        services.AddControllers();

        // Application
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAccessTokenQuery).Assembly));

        // Infrastructure
        services.AddScoped<IHasher, Hasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddSingleton<IUserRepository, FakeUserRepository>();

        return services;
    }
}
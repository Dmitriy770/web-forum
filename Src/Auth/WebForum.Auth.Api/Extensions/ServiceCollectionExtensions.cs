using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebForum.Auth.Api.Authorization;
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
        // Api
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddApiAuthentication();
        services.AddControllers();

        // Application
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAccessTokenQuery).Assembly));

        // Infrastructure
        services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));
        services.AddScoped<IHasher, Hasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddSingleton<IUserRepository, FakeUserRepository>();

        return services;
    }

    private static IServiceCollection AddApiAuthentication(this IServiceCollection services)
    {
        var jwtOptions = services.BuildServiceProvider().GetService<IOptions<JwtOptions>>()!.Value;
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["some-cookies"];
                        
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthentication();
        
        return services;
    }
}
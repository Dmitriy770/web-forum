using WebForum.Core.Application.Interfaces;
using WebForum.Core.Application.Queries;
using WebForum.Core.Infrastructure.Repositories;
using WebForum.Core.Infrastructure.Utilities;

namespace WebForum.Core.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebForumCore(this IServiceCollection services)
    {
        // Api
        services.AddControllers();

        // Application
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProfileByUserIdQuery).Assembly));

        // Infrastructure
        services.AddScoped<IDataTimeProvider, DataTimeProvider>();
        services.AddScoped<IProfileRepository, FakeProfileRepository>();
        services.AddScoped<IPostRepository, FakePostRepository>();

        return services;
    } 
}
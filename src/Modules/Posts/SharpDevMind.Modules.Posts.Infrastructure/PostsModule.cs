using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpDevMind.Common.Presentation.Endpoints;

namespace SharpDevMind.Modules.Posts.Infrastructure;

public static class PostsModule
{
    public static IServiceCollection AddPostsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // will implement it later
    }
}

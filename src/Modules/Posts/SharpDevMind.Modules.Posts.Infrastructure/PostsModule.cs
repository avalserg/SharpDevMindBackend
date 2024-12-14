using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpDevMind.Common.Infrastructure.Outbox;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Modules.Posts.Application.Abstractions.Data;
using SharpDevMind.Modules.Posts.Domain.Authors;
using SharpDevMind.Modules.Posts.Domain.Categories;
using SharpDevMind.Modules.Posts.Domain.Posts;
using SharpDevMind.Modules.Posts.Infrastructure.Authors;
using SharpDevMind.Modules.Posts.Infrastructure.Categories;
using SharpDevMind.Modules.Posts.Infrastructure.Database;
using SharpDevMind.Modules.Posts.Infrastructure.Posts;
using SharpDevMind.Modules.Posts.Presentation.Authors;

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
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<UserRegisteredIntegrationEventConsumer>();


    }
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PostsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Posts))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PostsDbContext>());

        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();

    }
}

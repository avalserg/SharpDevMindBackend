using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Infrastructure.Outbox;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Modules.Comments.Application.Abstractions.Data;
using SharpDevMind.Modules.Comments.Domain.Authors;
using SharpDevMind.Modules.Comments.Domain.Comments;
using SharpDevMind.Modules.Comments.Domain.Posts;
using SharpDevMind.Modules.Comments.Infrastructure.Authors;
using SharpDevMind.Modules.Comments.Infrastructure.Comments;
using SharpDevMind.Modules.Comments.Infrastructure.Database;
using SharpDevMind.Modules.Comments.Infrastructure.Outbox;
using SharpDevMind.Modules.Comments.Infrastructure.Posts;
using SharpDevMind.Modules.Comments.Presentation.Authors;
using SharpDevMind.Modules.Comments.Presentation.Posts;

namespace SharpDevMind.Modules.Comments.Infrastructure;

public static class CommentsModule
{
    public static IServiceCollection AddCommentsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDomainEventHandlers();

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        services.AddInfrastructure(configuration);

        return services;
    }
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {

        registrationConfigurator.AddConsumer<UserRegisteredIntegrationEventConsumer>();
        registrationConfigurator.AddConsumer<UserProfileUpdatedIntegrationEventConsumer>();
        registrationConfigurator.AddConsumer<PostCreatedIntegrationEventConsumer>();
    }
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CommentsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Comments))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CommentsDbContext>());

        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IPostRepository, PostRepository>();

        services.Configure<OutboxOptions>(configuration.GetSection("Comments:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();


    }
    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }

}

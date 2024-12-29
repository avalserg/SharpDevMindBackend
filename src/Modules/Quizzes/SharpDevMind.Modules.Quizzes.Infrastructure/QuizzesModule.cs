using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Infrastructure.Outbox;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Modules.Quizzes.Application.Abstractions.Data;
using SharpDevMind.Modules.Quizzes.Domain.Options;
using SharpDevMind.Modules.Quizzes.Domain.Questions;
using SharpDevMind.Modules.Quizzes.Domain.QuizResults;
using SharpDevMind.Modules.Quizzes.Domain.Users;
using SharpDevMind.Modules.Quizzes.Infrastructure.Database;
using SharpDevMind.Modules.Quizzes.Infrastructure.Options;
using SharpDevMind.Modules.Quizzes.Infrastructure.Outbox;
using SharpDevMind.Modules.Quizzes.Infrastructure.Questions;
using SharpDevMind.Modules.Quizzes.Infrastructure.QuizResults;
using SharpDevMind.Modules.Quizzes.Infrastructure.Users;
using SharpDevMind.Modules.Quizzes.Presentation.Users;

namespace SharpDevMind.Modules.Quizzes.Infrastructure;

public static class QuizzesModule
{
    public static IServiceCollection AddQuizzesModule(
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
    }
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<QuizzesDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Quizzes))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<QuizzesDbContext>());

        services.AddScoped<IOptionRepository, OptionRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IQuizResultRepository, QuizResultRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.Configure<OutboxOptions>(configuration.GetSection("Quizzes:Outbox"));

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

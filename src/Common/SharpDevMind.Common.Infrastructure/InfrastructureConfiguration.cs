using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using SharpDevMind.Common.Application.Clock;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Infrastructure.Clock;
using SharpDevMind.Common.Infrastructure.Data;

namespace SharpDevMind.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();


        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        return services;
    }
}

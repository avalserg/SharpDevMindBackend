﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using SharpDevMind.Common.Application.Caching;
using SharpDevMind.Common.Application.Clock;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Infrastructure.Caching;
using SharpDevMind.Common.Infrastructure.Clock;
using SharpDevMind.Common.Infrastructure.Data;
using StackExchange.Redis;

namespace SharpDevMind.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnectionString,
        string redisConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();

        services.TryAddSingleton(npgsqlDataSource);

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();


        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();

        return services;
    }
}

using System.Data.Common;
using Npgsql;
using SharpDevMind.Modules.Users.Application.Abstractions.Data;

namespace SharpDevMind.Modules.Users.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}

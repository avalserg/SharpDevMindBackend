using System.Data.Common;

namespace SharpDevMind.Modules.Users.Application.Abstractions.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}

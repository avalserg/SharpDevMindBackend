using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Domain.Users;

namespace SharpDevMind.Modules.Quizzes.Application.Users.GetUser;

internal sealed class GetUserByIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(UserResponse.Id)},
                 email AS {nameof(UserResponse.Email)},
                 first_name AS {nameof(UserResponse.FirstName)},
                 last_name AS {nameof(UserResponse.LastName)}
             FROM quizzes.users
             WHERE id = @UserId
             """;

        UserResponse? users = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, request);

        if (users is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }

        return users;
    }
}

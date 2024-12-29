using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Quizzes.Application.QuizResults.GetQuizzesResultsByUserId;

internal sealed class GetQuizzesResultsByUserIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetQuizzesResultsByUserIdQuery, IReadOnlyCollection<QuizzesResultsByUserIdResponse>>
{
    public async Task<Result<IReadOnlyCollection<QuizzesResultsByUserIdResponse>>> Handle(
        GetQuizzesResultsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 e.id AS {nameof(QuizzesResultsByUserIdResponse.Id)},
                 e.user_id AS {nameof(QuizzesResultsByUserIdResponse.UserId)},
                 e.score AS {nameof(QuizzesResultsByUserIdResponse.Score)},
                 e.completed_at AS {nameof(QuizzesResultsByUserIdResponse.CompletedAt)}
             FROM quizzes.quiz_results e
             WHERE
                 e.user_id = @UserId   
             """;

        List<QuizzesResultsByUserIdResponse> quizzesResults = (await connection.QueryAsync<QuizzesResultsByUserIdResponse>(sql, request)).AsList();

        return quizzesResults;
    }
}

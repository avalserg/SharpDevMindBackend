using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Domain.QuizResults;

namespace SharpDevMind.Modules.Quizzes.Application.QuizResults.GetQuizResult;

internal sealed class GetQuizResultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetQuizResultQuery, QuizResultResponse>
{
    public async Task<Result<QuizResultResponse>> Handle(GetQuizResultQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
              SELECT
                  e.id AS {nameof(QuizResultResponse.Id)},
                  e.user_id AS {nameof(QuizResultResponse.UserId)},
                  e.score AS {nameof(QuizResultResponse.Score)},
                  e.completed_at AS {nameof(QuizResultResponse.CompletedAt)}
                  FROM quizzes.quiz_results e
              WHERE e.id = @Id
              """;

        QuizResultResponse? quizResultResponse = await connection.QuerySingleOrDefaultAsync<QuizResultResponse>(sql, request);

        if (quizResultResponse is null)
        {
            return Result.Failure<QuizResultResponse>(QuizResultErrors.NotFound(request.Id));
        }

        return quizResultResponse;

    }
}

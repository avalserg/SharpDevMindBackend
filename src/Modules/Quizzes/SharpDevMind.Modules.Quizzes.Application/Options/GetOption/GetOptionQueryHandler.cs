using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Domain.Options;

namespace SharpDevMind.Modules.Quizzes.Application.Options.GetOption;

internal sealed class GetOptionQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOptionQuery, OptionResponse>
{
    public async Task<Result<OptionResponse>> Handle(GetOptionQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        const string sql =
            $"""
             SELECT
                 e.id AS {nameof(OptionResponse.Id)},
                 e.question_id AS {nameof(OptionResponse.QuestionId)},
                 e.text AS {nameof(OptionResponse.Text)}
             FROM quizzes.options e
             WHERE e.id = @OptionId
             """;


        OptionResponse? comment = await connection.QuerySingleOrDefaultAsync<OptionResponse>(sql, request);

        if (comment is null)
        {
            return Result.Failure<OptionResponse>(OptionErrors.NotFound(request.OptionId));
        }

        return comment;
    }
}

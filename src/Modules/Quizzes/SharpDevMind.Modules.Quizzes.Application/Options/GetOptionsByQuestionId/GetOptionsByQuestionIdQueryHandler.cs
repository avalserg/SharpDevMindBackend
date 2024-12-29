using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Quizzes.Application.Options.GetOptionsByQuestionId;

internal sealed class GetOptionsByQuestionIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOptionsByQuestionIdQuery, IReadOnlyCollection<OptionByQuestionIdResponse>>
{
    public async Task<Result<IReadOnlyCollection<OptionByQuestionIdResponse>>> Handle(
        GetOptionsByQuestionIdQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 e.id AS {nameof(OptionByQuestionIdResponse.Id)},
                 e.question_id AS {nameof(OptionByQuestionIdResponse.QuestionId)},
                 e.text AS {nameof(OptionByQuestionIdResponse.Text)}
             FROM quizzes.options e
             WHERE
                 e.question_id = @QuestionId   
             """;

        List<OptionByQuestionIdResponse> options = (await connection.QueryAsync<OptionByQuestionIdResponse>(sql, request)).AsList();

        return options;
    }
}

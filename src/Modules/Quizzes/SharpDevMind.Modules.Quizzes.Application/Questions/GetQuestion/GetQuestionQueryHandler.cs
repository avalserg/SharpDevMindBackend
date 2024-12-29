using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Application.Options.GetOption;
using SharpDevMind.Modules.Quizzes.Domain.Questions;

namespace SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestion;

internal sealed class GetQuestionQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetQuestionQuery, QuestionResponse>
{
    public async Task<Result<QuestionResponse>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        const string sql =
            $"""
              SELECT
                  e.id AS {nameof(QuestionResponse.Id)},
                  e.text_condition AS {nameof(QuestionResponse.TextCondition)},
                  o.id AS {nameof(OptionResponse.Id)},
                  o.text AS {nameof(OptionResponse.Text)},
                  o.question_id AS {nameof(OptionResponse.QuestionId)}
                  FROM quizzes.questions e
              LEFT JOIN quizzes.options o ON e.id = o.question_id
              WHERE e.id = @Id
              """;


        var questionDictionary = new Dictionary<Guid, QuestionResponse>();

        await connection.QueryAsync<QuestionResponse, OptionResponse?, QuestionResponse>(
              sql,
              (question, option) =>
              {
                  if (!questionDictionary.TryGetValue(question.Id, out QuestionResponse? questionEntry))
                  {
                      questionEntry = question;
                      questionDictionary.Add(question.Id, questionEntry);
                  }

                  if (option != null)
                  {
                      questionEntry.Options.Add(option);
                  }

                  return questionEntry;
              },
              new { request.Id },
              splitOn: "Id");

        QuestionResponse? questionResponse = questionDictionary.Values.FirstOrDefault();

        if (questionResponse is null)
        {
            return Result.Failure<QuestionResponse>(QuestionErrors.NotFound(request.Id));
        }

        return questionResponse;


    }
}

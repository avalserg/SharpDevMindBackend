using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Application.Options.GetOption;
using SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestion;

namespace SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestions;

internal sealed class GetQuestionsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetQuestionsQuery, IReadOnlyCollection<QuestionResponse>>
{
    public async Task<Result<IReadOnlyCollection<QuestionResponse>>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
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
              splitOn: "Id");

        return questionDictionary.Values.ToList();

    }
}

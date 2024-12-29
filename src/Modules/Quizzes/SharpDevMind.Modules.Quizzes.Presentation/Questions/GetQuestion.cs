using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestion;

namespace SharpDevMind.Modules.Quizzes.Presentation.Questions;

internal sealed class GetQuestion : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("questions/{questionId}", async (Guid questionId, ISender sender) =>
        {
            Result<QuestionResponse> result = await sender.Send(new GetQuestionQuery(questionId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Quizzes);
    }
}

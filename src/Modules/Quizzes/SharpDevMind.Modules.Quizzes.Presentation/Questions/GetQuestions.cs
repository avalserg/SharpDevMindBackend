using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestion;
using SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestions;

namespace SharpDevMind.Modules.Quizzes.Presentation.Questions;

internal sealed class GetQuestions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("questions", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<QuestionResponse>> result = await sender.Send(new GetQuestionsQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Quizzes);
    }
}

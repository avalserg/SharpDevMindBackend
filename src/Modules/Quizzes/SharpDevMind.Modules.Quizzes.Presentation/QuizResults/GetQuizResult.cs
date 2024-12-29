using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Quizzes.Application.QuizResults.GetQuizResult;

namespace SharpDevMind.Modules.Quizzes.Presentation.QuizResults;

internal sealed class GetQuizResult : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("quizResult/{quizResultId}", async (Guid quizResultId, ISender sender) =>
        {
            Result<QuizResultResponse> result = await sender.Send(new GetQuizResultQuery(quizResultId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })

        .WithTags(Tags.Quizzes);
    }
}

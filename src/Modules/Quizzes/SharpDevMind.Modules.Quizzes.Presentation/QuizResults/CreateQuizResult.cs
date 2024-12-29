using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Quizzes.Application.QuizResults.CreateQuizResult;

namespace SharpDevMind.Modules.Quizzes.Presentation.QuizResults;

internal sealed class CreateQuizResult : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("quizResult", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateQuizResultCommand(
                request.UserId,
                request.Score
               ));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Quizzes);
    }

    internal sealed class Request
    {
        public Guid UserId { get; init; }
        public int Score { get; init; }

    }
}

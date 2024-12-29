using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Quizzes.Application.QuizResults.GetQuizzesResultsByUserId;

namespace SharpDevMind.Modules.Quizzes.Presentation.QuizResults;

internal sealed class GetQuizzesResultsByUserId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("quizResult/userId/{userId}", async (Guid userId, ISender sender) =>
        {
            Result<IReadOnlyCollection<QuizzesResultsByUserIdResponse>> result = await sender.Send(new GetQuizzesResultsByUserIdQuery(userId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Quizzes);
    }
}

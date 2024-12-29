using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Quizzes.Application.Options.CreateOption;

namespace SharpDevMind.Modules.Quizzes.Presentation.Options;

internal sealed class CreateOption : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("options", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateOptionCommand(
                request.QuestionId,
                request.Text
               ));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Quizzes);
    }

    internal sealed class Request
    {
        public Guid QuestionId { get; init; }
        public string Text { get; init; }

    }
}

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Quizzes.Application.Questions.CreateQuestion;

namespace SharpDevMind.Modules.Quizzes.Presentation.Questions;

internal sealed class CreateQuestion : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("questions", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateQuestionCommand(
                request.Text,
                request.Options,
                request.IndexOfTheCorrectAnswer
               ));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Quizzes);
    }

    internal sealed class Request
    {

        public string Text { get; init; }
        public List<string> Options { get; init; }
        public int IndexOfTheCorrectAnswer { get; init; }

    }
}

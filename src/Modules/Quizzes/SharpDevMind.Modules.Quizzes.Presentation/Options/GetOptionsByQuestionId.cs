using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Quizzes.Application.Options.GetOptionsByQuestionId;

namespace SharpDevMind.Modules.Quizzes.Presentation.Options;

internal sealed class GetOptionsByQuestionId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("options/questionId/{questionId}", async (Guid questionId, ISender sender) =>
        {
            Result<IReadOnlyCollection<OptionByQuestionIdResponse>> result = await sender.Send(new GetOptionsByQuestionIdQuery(questionId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Quizzes);
    }
}

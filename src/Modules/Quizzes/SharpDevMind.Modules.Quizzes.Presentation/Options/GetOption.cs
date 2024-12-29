using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Quizzes.Application.Options.GetOption;

namespace SharpDevMind.Modules.Quizzes.Presentation.Options;

internal sealed class GetOption : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("options/{optionId}", async (Guid optionId, ISender sender) =>
        {
            Result<OptionResponse> result = await sender.Send(new GetOptionQuery(optionId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })

        .WithTags(Tags.Quizzes);
    }
}

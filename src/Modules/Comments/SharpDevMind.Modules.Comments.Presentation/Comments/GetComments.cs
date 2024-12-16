using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Comments.Application.Comments.GetComments;

namespace SharpDevMind.Modules.Comments.Presentation.Comments;

internal sealed class GetComments : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("comments", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<CommentResponse>> result = await sender.Send(new GetCommentsQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Comments);
    }
}

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Comments.Application.Comments.ArchiveComment;

namespace SharpDevMind.Modules.Comments.Presentation.Comments;

internal sealed class CancelComment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("comments/{id}/cancel", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new ArchiveCommentCommand(id));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Comments);
    }
}

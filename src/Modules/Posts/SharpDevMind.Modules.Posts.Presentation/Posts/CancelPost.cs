using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Posts.Application.Posts.ArchivePost;

namespace SharpDevMind.Modules.Posts.Presentation.Posts;

internal sealed class CancelPost : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("posts/{id}/cancel", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new ArchivePostCommand(id));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Posts);
    }
}

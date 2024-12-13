using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Posts.Application.Posts.GetPost;

namespace SharpDevMind.Modules.Posts.Presentation.Posts;

internal sealed class GetPost : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("posts/{id}", async (Guid id, ISender sender) =>
        {
            Result<PostResponse> result = await sender.Send(new GetPostQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })

        .WithTags(Tags.Posts);
    }
}

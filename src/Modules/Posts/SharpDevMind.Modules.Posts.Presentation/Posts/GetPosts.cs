using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Posts.Application.Posts.GetPosts;

namespace SharpDevMind.Modules.Posts.Presentation.Posts;

internal sealed class GetPosts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("posts", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<PostResponse>> result = await sender.Send(new GetPostsQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Posts);
    }
}

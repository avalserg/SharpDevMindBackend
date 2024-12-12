using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Posts.Application.Posts.CreatePost;

namespace SharpDevMind.Modules.Posts.Presentation.Events;

internal sealed class CreatePost : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("posts", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreatePostCommand(
                request.CategoryId,
                request.UserId,
                request.Title,
                request.Content
               ));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Posts);
    }

    internal sealed class Request
    {
        public Guid CategoryId { get; init; }
        public Guid UserId { get; init; }

        public string Title { get; init; }

        public string Content { get; init; }

    }
}

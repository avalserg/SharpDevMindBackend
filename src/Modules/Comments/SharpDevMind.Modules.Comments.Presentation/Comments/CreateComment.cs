using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Comments.Application.Comments.CreateComment;

namespace SharpDevMind.Modules.Comments.Presentation.Comments;

internal sealed class CreateComment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("comments", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateCommentCommand(
                request.UserId,
                request.PostId,
                request.Content
               ));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Comments);
    }

    internal sealed class Request
    {
        public Guid PostId { get; init; }
        public Guid UserId { get; init; }
        public string Content { get; init; }

    }
}

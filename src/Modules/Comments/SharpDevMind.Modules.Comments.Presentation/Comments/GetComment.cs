using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Comments.Application.Comments.GetComment;

namespace SharpDevMind.Modules.Comments.Presentation.Comments;

internal sealed class GetComment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("comments/{id}", async (Guid id, ISender sender) =>
        {
            Result<CommentResponse> result = await sender.Send(new GetCommentQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })

        .WithTags(Tags.Comments);
    }
}

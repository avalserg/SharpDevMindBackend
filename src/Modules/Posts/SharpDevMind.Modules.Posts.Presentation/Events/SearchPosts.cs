using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Posts.Application.Posts.SearchPosts;

namespace SharpDevMind.Modules.Posts.Presentation.Events;

internal sealed class SearchPosts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("posts/search", async (
            ISender sender,
            Guid? categoryId,
            DateTime? startDate,
            DateTime? endDate,
            int page = 0,
            int pageSize = 15) =>
        {
            Result<SearchPostsResponse> result = await sender.Send(
                new SearchPostsQuery(categoryId, startDate, endDate, page, pageSize));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Posts);
    }
}

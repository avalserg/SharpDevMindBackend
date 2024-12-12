using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Posts.Application.Categories.GetCategory;

namespace SharpDevMind.Modules.Posts.Presentation.Categories;

internal sealed class GetCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories/{id}", async (Guid id, ISender sender) =>
        {
            Result<CategoryResponse> result = await sender.Send(new GetCategoryQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Categories);
    }
}

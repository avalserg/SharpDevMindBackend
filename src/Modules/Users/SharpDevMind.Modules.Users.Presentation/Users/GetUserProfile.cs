using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Application.Caching;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Users.Application.Users.GetUser;

namespace SharpDevMind.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{id}", async (Guid id, ISender sender, ICacheService cacheService) =>
            {
                UserResponse? userResponse = await cacheService.GetAsync<UserResponse>("users");

                if (userResponse is not null)
                {
                    return Results.Ok(userResponse);
                }
                Result<UserResponse> result = await sender.Send(new GetUserQuery(id));

                if (result.IsSuccess)
                {
                    await cacheService.SetAsync("users", result.Value);
                }

                return result.Match(Results.Ok, ApiResults.Problem);
            })
        .WithTags(Tags.Users);
    }
}

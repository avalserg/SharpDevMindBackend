using Evently.Common.Application.Caching;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Users.Application.Users.GetUser;
using SharpDevMind.Modules.Users.Presentation.Results;

namespace SharpDevMind.Modules.Users.Presentation.Users;

internal static class GetUserProfile
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{id}", async (Guid id, ISender sender, ICacheService cacheService) =>
            {
                UserResponse? userResponse = await cacheService.GetAsync<UserResponse>("users");

                if (userResponse is not null)
                {
                    return Microsoft.AspNetCore.Http.Results.Ok(userResponse);
                }
                Result<UserResponse> result = await sender.Send(new GetUserQuery(id));

                if (result.IsSuccess)
                {
                    await cacheService.SetAsync("users", result.Value);
                }

                return result.Match(Microsoft.AspNetCore.Http.Results.Ok, ApiResults.Problem);
            })
        .WithTags(Tags.Users);
    }
}

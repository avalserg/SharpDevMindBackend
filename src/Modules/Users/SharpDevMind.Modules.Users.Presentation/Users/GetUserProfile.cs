using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Modules.Users.Application.Users.GetUser;
using SharpDevMind.Modules.Users.Domain.Abstractions;

namespace SharpDevMind.Modules.Users.Presentation.Users;

internal static class GetUserProfile
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{id}", async (Guid id, ISender sender) =>
        {
            Result<UserResponse> result = await sender.Send(new GetUserQuery(id));

            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithTags(Tags.Users);
    }
}

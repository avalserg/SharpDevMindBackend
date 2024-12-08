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
        app.MapGet("users/{id}", async (Guid id, ISender sender) =>
        {
            Result<UserResponse> result = await sender.Send(new GetUserQuery(id));

            return result.Match(Microsoft.AspNetCore.Http.Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}

using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Common.Domain;
using SharpDevMind.Common.Infrastructure.Authentication;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Common.Presentation.Results;
using SharpDevMind.Modules.Users.Application.Users.GetUser;

namespace SharpDevMind.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/profile", async (ClaimsPrincipal claims, ISender sender) =>
            {

                Result<UserResponse> result = await sender.Send(new GetUserQuery(claims.GetUserId()));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization("users:read")
        .WithTags(Tags.Users);
    }
}

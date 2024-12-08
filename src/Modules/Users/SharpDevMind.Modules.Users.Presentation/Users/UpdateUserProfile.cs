﻿using Evently.Common.Presentation.Results;
using Evently.Modules.Users.Application.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpDevMind.Modules.Users.Domain.Abstractions;
using SharpDevMind.Modules.Users.Presentation;

namespace Evently.Modules.Users.Presentation.Users;

internal static class UpdateUserProfile
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/profile/{id}", async (Request request, Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateUserCommand(
                id,
                request.FirstName,
                request.LastName));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}

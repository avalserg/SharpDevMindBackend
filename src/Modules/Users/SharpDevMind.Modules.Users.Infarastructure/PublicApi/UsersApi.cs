using MediatR;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Users.Application.Users.GetUser;
using SharpDevMind.Modules.Users.PublicApi;
using UserResponse = SharpDevMind.Modules.Users.PublicApi.UserResponse;

namespace SharpDevMind.Modules.Users.Infrastructure.PublicApi;
internal sealed class UsersApi(ISender sender) : IUsersApi
{
    public async Task<UserResponse?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        Result<Application.Users.GetUser.UserResponse> result =
            await sender.Send(new GetUserQuery(userId), cancellationToken);
        if (result.IsFailure)
        {
            return null;
        }

        return new UserResponse(
            result.Value.Id,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName
        );
    }
}

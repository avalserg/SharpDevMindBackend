using System.Net.Http.Json;

namespace SharpDevMind.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakClient(HttpClient httpClient)
{
    internal async Task<string> RegisterUserAsync(UserRepresentation user, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(
            "users",
            user,
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return ExtractIdentityIdFromLocationHeader(httpResponseMessage);
    }
    internal async Task<string> UpdateUserAsync(UserRepresentation user, string identityId, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(
            $"users/{identityId}",
            user,
            cancellationToken);
        httpResponseMessage.EnsureSuccessStatusCode();

        return identityId;
    }
    internal async Task UpdateUserPasswordAsync(PasswordRepresentation user, string identityId, CancellationToken cancellationToken = default)
    {

        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(
            $"users/{identityId}/reset-password",
            user,
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

    }

    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        string? locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header is null");
        }

        int userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);

        string identityId = locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);

        return identityId;
    }
}

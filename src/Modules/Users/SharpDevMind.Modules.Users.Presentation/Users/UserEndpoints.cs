using Microsoft.AspNetCore.Routing;

namespace SharpDevMind.Modules.Users.Presentation.Users;

public static class EventEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        RegisterUser.MapEndpoint(app);
        GetUserProfile.MapEndpoint(app);
        UpdateUserProfile.MapEndpoint(app);
    }
}

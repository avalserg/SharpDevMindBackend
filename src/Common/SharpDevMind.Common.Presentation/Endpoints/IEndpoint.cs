using Microsoft.AspNetCore.Routing;

namespace SharpDevMind.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

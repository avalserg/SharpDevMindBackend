using MediatR;
using SharpDevMind.Modules.Posts.Application.Authors.CreateAuthor;
using SharpDevMind.Modules.Posts.PublicApi;

namespace SharpDevMind.Modules.Posts.Infrastructure.PublicApi;
internal sealed class PostsApi(ISender sender) : IPostsApi
{
    public async Task CreateAuthorAsync(
        Guid authorId,
        string email,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default)
    {

        await sender.Send(new CreateAuthorCommand(authorId, email, firstName, lastName), cancellationToken);

    }
}

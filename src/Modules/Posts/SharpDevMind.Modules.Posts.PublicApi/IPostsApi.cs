namespace SharpDevMind.Modules.Posts.PublicApi;

public interface IPostsApi
{
    Task CreateAuthorAsync(
        Guid authorId,
        string email,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default);
}

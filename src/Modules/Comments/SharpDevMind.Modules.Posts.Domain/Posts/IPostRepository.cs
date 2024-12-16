namespace SharpDevMind.Modules.Comments.Domain.Posts;

public interface IPostRepository
{
    Task<Post?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Post post);
}

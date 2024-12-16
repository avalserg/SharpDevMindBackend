namespace SharpDevMind.Modules.Comments.Domain.Comments;

public interface ICommentRepository
{
    Task<Comment?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Comment post);
}

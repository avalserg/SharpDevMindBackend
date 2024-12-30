namespace SharpDevMind.Modules.Posts.Domain.Authors;

public interface IAuthorRepository
{
    Task<Author?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Author author);
}

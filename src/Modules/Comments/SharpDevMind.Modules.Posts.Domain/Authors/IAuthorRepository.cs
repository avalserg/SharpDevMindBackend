namespace SharpDevMind.Modules.Comments.Domain.Authors;

public interface IAuthorRepository
{
    Task<Author?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Author author);
}

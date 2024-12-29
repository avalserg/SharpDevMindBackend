namespace SharpDevMind.Modules.Quizzes.Domain.Options;
public interface IOptionRepository
{
    Task<Option?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Option option);
    void InsertMany(List<Option> options);
}

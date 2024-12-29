using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Quizzes.Domain.Options;
using SharpDevMind.Modules.Quizzes.Infrastructure.Database;

namespace SharpDevMind.Modules.Quizzes.Infrastructure.Options;
internal sealed class OptionRepository(QuizzesDbContext context) : IOptionRepository
{
    public async Task<Option?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Options.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(Option option)
    {
        context.Options.Add(option);
    }
    public void InsertMany(List<Option> options)
    {
        context.Options.AddRange(options);
    }
}

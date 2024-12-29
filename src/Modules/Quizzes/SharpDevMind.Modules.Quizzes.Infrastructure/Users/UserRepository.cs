using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Quizzes.Domain.Users;
using SharpDevMind.Modules.Quizzes.Infrastructure.Database;

namespace SharpDevMind.Modules.Quizzes.Infrastructure.Users;

internal sealed class UserRepository(QuizzesDbContext context) : IUserRepository
{
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(User customer)
    {
        context.Users.Add(customer);
    }
}

using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Comments.Infrastructure.Database;
using SharpDevMind.Modules.Posts.Infrastructure.Database;
using SharpDevMind.Modules.Quizzes.Infrastructure.Database;
using SharpDevMind.Modules.Users.Infrastructure.Database;

namespace SharpDevMind.Api.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<UsersDbContext>(scope);
        ApplyMigration<PostsDbContext>(scope);
        ApplyMigration<CommentsDbContext>(scope);
        ApplyMigration<QuizzesDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}

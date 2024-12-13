using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Users.Application.Abstractions.Data;
using SharpDevMind.Modules.Users.Domain.Users;
using SharpDevMind.Modules.Users.Infrastructure.Users;

namespace SharpDevMind.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());

    }
}

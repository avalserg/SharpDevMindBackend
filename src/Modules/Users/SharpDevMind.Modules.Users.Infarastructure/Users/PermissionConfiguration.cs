using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpDevMind.Modules.Users.Domain.Users;

namespace SharpDevMind.Modules.Users.Infrastructure.Users;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code).HasMaxLength(100);

        builder.HasData(
            Permission.GetUser,
            Permission.ModifyUser,
            Permission.GetCategories,
            Permission.ModifyCategories,
            Permission.ArchiveCategories,
            Permission.AddCategories,
            Permission.GetPosts,
            Permission.AddPosts,
            Permission.ArchivePosts,
            Permission.SearchPosts
            );



        builder
                .HasMany<Role>()
                .WithMany()
                .UsingEntity(joinBuilder =>
                {
                    joinBuilder.ToTable("role_permissions");

                    joinBuilder.HasData(
                         // Guest Permission
                         CreateRolePermission(Role.Guest, Permission.GetCategories),
                         CreateRolePermission(Role.Guest, Permission.GetPosts),
                         CreateRolePermission(Role.Guest, Permission.SearchPosts),
                        // Member permissions
                        CreateRolePermission(Role.User, Permission.GetUser),
                        CreateRolePermission(Role.User, Permission.ModifyUser),
                        CreateRolePermission(Role.User, Permission.GetCategories),
                        CreateRolePermission(Role.User, Permission.GetPosts),
                        CreateRolePermission(Role.User, Permission.AddPosts),
                        CreateRolePermission(Role.User, Permission.ArchivePosts),
                        CreateRolePermission(Role.User, Permission.SearchPosts),
                        // Admin permissions
                        CreateRolePermission(Role.Administrator, Permission.GetUser),
                        CreateRolePermission(Role.Administrator, Permission.ModifyUser),
                        CreateRolePermission(Role.Administrator, Permission.GetCategories),
                        CreateRolePermission(Role.Administrator, Permission.ModifyCategories),
                        CreateRolePermission(Role.Administrator, Permission.ArchiveCategories),
                        CreateRolePermission(Role.Administrator, Permission.AddCategories),
                        CreateRolePermission(Role.Administrator, Permission.GetPosts),
                        CreateRolePermission(Role.Administrator, Permission.AddPosts),
                        CreateRolePermission(Role.Administrator, Permission.ArchivePosts),
                        CreateRolePermission(Role.Administrator, Permission.SearchPosts)

                      );
                });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}

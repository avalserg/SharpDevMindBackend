namespace SharpDevMind.Modules.Users.Domain.Users;

public sealed class Permission
{
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission ModifyUser = new("users:update");
    public static readonly Permission GetCategories = new("categories:read");
    public static readonly Permission ArchiveCategories = new("categories:archive");
    public static readonly Permission AddCategories = new("categories:add");
    public static readonly Permission ModifyCategories = new("categories:update");
    public static readonly Permission GetPosts = new("posts:read");
    public static readonly Permission AddPosts = new("posts:add");
    public static readonly Permission ArchivePosts = new("posts:archive");
    public static readonly Permission SearchPosts = new("posts:search");



    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }
}

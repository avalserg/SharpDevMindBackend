namespace SharpDevMind.Modules.Users.Domain.Users;

public sealed class Permission
{
    // users
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission ModifyUser = new("users:update");

    // categories
    public static readonly Permission GetCategories = new("categories:read");
    public static readonly Permission ArchiveCategories = new("categories:archive");
    public static readonly Permission AddCategories = new("categories:add");
    public static readonly Permission ModifyCategories = new("categories:update");

    // posts
    public static readonly Permission GetPosts = new("posts:read");
    public static readonly Permission AddPosts = new("posts:add");
    public static readonly Permission ArchivePosts = new("posts:archive");
    public static readonly Permission SearchPosts = new("posts:search");

    // comments
    public static readonly Permission AddComments = new("comments:add");
    public static readonly Permission ModifyComments = new("comments:update");



    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }
}

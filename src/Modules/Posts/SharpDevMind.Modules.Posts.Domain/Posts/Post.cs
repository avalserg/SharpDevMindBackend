using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Domain.Categories;

namespace SharpDevMind.Modules.Posts.Domain.Posts;

public sealed class Post : Entity
{
    private Post()
    {
    }

    public Guid Id { get; private set; }

    public Guid CategoryId { get; private set; }
    public Guid UserId { get; private set; }

    public string Title { get; private set; }

    public string Content { get; private set; }
    public int Rating { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }
    public PostStatus Status { get; private set; }

    public static Post Create(
        Guid userId,
        Category category,
        string title,
        string content
        )
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CategoryId = category.Id,
            Title = title,
            Content = content,
            CreatedAtUtc = DateTime.Now.ToUniversalTime(),
            Rating = 0,
            Status = PostStatus.Published
        };

        post.Raise(new PostCreatedDomainEvent(post.Id));

        return post;
    }

    public Result Update(
        Guid userId,
        Category category,
        string title,
        string content
        )
    {
        if (UserId != userId)
        {
            return Result.Failure(PostErrors.UserNotOwnerPost(userId));
        }

        CategoryId = category.Id;
        Title = title;
        Content = content;
        UpdatedAtUtc = DateTime.Now.ToUniversalTime();

        Raise(new PostUpdatedDomainEvent(Id, UpdatedAtUtc));

        return Result.Success();
    }

    public Result Archive()
    {
        if (Status == PostStatus.Archived)
        {
            return Result.Failure(PostErrors.AlreadyArchived);
        }

        Status = PostStatus.Archived;

        Raise(new PostArchivedDomainEvent(Id));

        return Result.Success();
    }
}

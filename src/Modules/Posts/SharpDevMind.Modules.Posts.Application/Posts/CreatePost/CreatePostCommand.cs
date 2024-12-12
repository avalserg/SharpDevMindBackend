using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Posts.Application.Posts.CreatePost;

public sealed record CreatePostCommand(
    Guid CategoryId,
    Guid UserId,
    string Title,
    string Content) : ICommand<Guid>;

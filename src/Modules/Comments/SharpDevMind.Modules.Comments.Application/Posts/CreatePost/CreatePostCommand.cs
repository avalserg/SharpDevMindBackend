using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Posts.CreatePost;

public sealed record CreatePostCommand(Guid PostId, Guid AuthorId)
    : ICommand;

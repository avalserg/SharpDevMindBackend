using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Comments.CreateComment;

public sealed record CreateCommentCommand(
    Guid AuthorId,
    Guid PostId,
    string Content) : ICommand<Guid>;

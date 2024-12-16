using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Comments.CreateComment;

public sealed record CreateCommentCommand(
    Guid UserId,
    Guid PostId,
    string Content) : ICommand<Guid>;

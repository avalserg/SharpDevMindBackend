using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Comments.ArchiveComment;

public sealed record ArchiveCommentCommand(Guid CommentId) : ICommand;

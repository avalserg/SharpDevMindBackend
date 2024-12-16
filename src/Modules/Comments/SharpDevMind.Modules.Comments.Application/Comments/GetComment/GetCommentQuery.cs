using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Comments.GetComment;

public sealed record GetCommentQuery(Guid CommentId) : IQuery<CommentResponse>;

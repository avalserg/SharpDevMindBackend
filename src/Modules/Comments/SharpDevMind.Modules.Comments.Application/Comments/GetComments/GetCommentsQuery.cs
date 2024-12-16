using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Comments.GetComments;

public sealed record GetCommentsQuery : IQuery<IReadOnlyCollection<CommentResponse>>;

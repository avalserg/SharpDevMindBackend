namespace SharpDevMind.Modules.Comments.Application.Comments.GetComment;

public sealed record CommentResponse(
    Guid Id,
    Guid PostId,
    string Content,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc
);


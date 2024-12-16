using SharpDevMind.Modules.Comments.Application.Comments.GetComments;

namespace SharpDevMind.Modules.Comments.Application.Comments.SearchComments;

public sealed record SearchCommentsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<CommentResponse> Comments);

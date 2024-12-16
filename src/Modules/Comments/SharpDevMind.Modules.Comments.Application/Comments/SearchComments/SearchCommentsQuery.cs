using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Comments.SearchComments;

public sealed record SearchCommentsQuery(
    Guid? PostId,
    DateTime? StartDate,
    DateTime? EndDate,
    int Page,
    int PageSize) : IQuery<SearchCommentsResponse>;

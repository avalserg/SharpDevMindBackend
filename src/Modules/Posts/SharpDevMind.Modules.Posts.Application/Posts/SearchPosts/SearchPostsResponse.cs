using SharpDevMind.Modules.Posts.Application.Posts.GetPosts;

namespace SharpDevMind.Modules.Posts.Application.Posts.SearchPosts;

public sealed record SearchPostsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<PostResponse> Posts);

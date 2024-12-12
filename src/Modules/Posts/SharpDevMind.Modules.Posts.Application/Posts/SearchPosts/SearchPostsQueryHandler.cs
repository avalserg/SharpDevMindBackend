using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Application.Posts.GetPosts;
using SharpDevMind.Modules.Posts.Domain.Posts;

namespace SharpDevMind.Modules.Posts.Application.Posts.SearchPosts;

internal sealed class SearchPostsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<SearchPostsQuery, SearchPostsResponse>
{

    public async Task<Result<SearchPostsResponse>> Handle(
        SearchPostsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var parameters = new SearchEventsParameters(
            (int)PostStatus.Published,
            request.CategoryId,
            request.StartDate?.Date,
            request.EndDate?.Date,
            request.PageSize,
            (request.Page - 1) * request.PageSize);

        IReadOnlyCollection<PostResponse> events = await GetEventsAsync(connection, parameters);

        int totalCount = await CountEventsAsync(connection, parameters);

        return new SearchPostsResponse(request.Page, request.PageSize, totalCount, events);
    }

    private static async Task<IReadOnlyCollection<PostResponse>> GetEventsAsync(
        DbConnection connection,
        SearchEventsParameters parameters)
    {
        const string sql =
            $"""
             SELECT
                 id AS {nameof(PostResponse.Id)},
                 category_id AS {nameof(PostResponse.CategoryId)},
                 title AS {nameof(PostResponse.Title)},
                 content AS {nameof(PostResponse.Content)},
                 rating AS {nameof(PostResponse.Rating)},
                 created_at_utc AS {nameof(PostResponse.CreatedAtUtc)},
                 updated_at_utc AS {nameof(PostResponse.UpdatedAtUtc)}
             FROM posts.posts
             WHERE
                status = 1 AND
                (@CategoryId IS NULL OR category_id = @CategoryId) AND
                (@StartDate::timestamp IS NULL OR created_at_utc >= @StartDate::timestamp) AND
                (@EndDate::timestamp IS NULL OR created_at_utc <= @EndDate::timestamp)
             ORDER BY created_at_utc
             OFFSET @Skip
             LIMIT @Take
             """;

        List<PostResponse> posts = (await connection.QueryAsync<PostResponse>(sql, parameters)).AsList();

        return posts;
    }

    private static async Task<int> CountEventsAsync(DbConnection connection, SearchEventsParameters parameters)
    {
        const string sql =
            """
            SELECT COUNT(*)
            FROM posts.posts
            WHERE
               status = 1 AND
               (@CategoryId IS NULL OR category_id = @CategoryId) AND
               (@StartDate::timestamp IS NULL OR created_at_utc >= @StartDate::timestamp) AND
               (@EndDate::timestamp IS NULL OR created_at_utc >= @EndDate::timestamp)
            """;

        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return totalCount;
    }

    private sealed record SearchEventsParameters(
        int Status,
        Guid? CategoryId,
        DateTime? StartDate,
        DateTime? EndDate,
        int Take,
        int Skip);
}

using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Application.Comments.GetComments;
using SharpDevMind.Modules.Comments.Domain.Comments;

namespace SharpDevMind.Modules.Comments.Application.Comments.SearchComments;

internal sealed class SearchCommentsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<SearchCommentsQuery, SearchCommentsResponse>
{

    public async Task<Result<SearchCommentsResponse>> Handle(
        SearchCommentsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var parameters = new SearchEventsParameters(
            (int)CommentStatus.Created,
            request.PostId,
            request.StartDate?.Date,
            request.EndDate?.Date,
            request.PageSize,
            (request.Page - 1) * request.PageSize);

        IReadOnlyCollection<CommentResponse> comments = await GetEventsAsync(connection, parameters);

        int totalCount = await CountCommentsAsync(connection, parameters);

        return new SearchCommentsResponse(request.Page, request.PageSize, totalCount, comments);
    }

    private static async Task<IReadOnlyCollection<CommentResponse>> GetEventsAsync(
        DbConnection connection,
        SearchEventsParameters parameters)
    {
        const string sql =
            $"""
             SELECT
                 id AS {nameof(CommentResponse.Id)},
                 post_id AS {nameof(CommentResponse.PostId)},
                 content AS {nameof(CommentResponse.Content)},
                 created_at_utc AS {nameof(CommentResponse.CreatedAtUtc)},
                 updated_at_utc AS {nameof(CommentResponse.UpdatedAtUtc)}
             FROM comments.comments
             WHERE
                status = 1 AND
                (@PostId IS NULL OR post_id = @PostId) AND
                (@StartDate::timestamp IS NULL OR created_at_utc >= @StartDate::timestamp) AND
                (@EndDate::timestamp IS NULL OR created_at_utc <= @EndDate::timestamp)
             ORDER BY created_at_utc
             OFFSET @Skip
             LIMIT @Take
             """;

        List<CommentResponse> comments = (await connection.QueryAsync<CommentResponse>(sql, parameters)).AsList();

        return comments;
    }

    private static async Task<int> CountCommentsAsync(DbConnection connection, SearchEventsParameters parameters)
    {
        const string sql =
            """
            SELECT COUNT(*)
            FROM comments.comments
            WHERE
               status = 1 AND
               (@PostId IS NULL OR post_id = @PostId) AND
               (@StartDate::timestamp IS NULL OR created_at_utc >= @StartDate::timestamp) AND
               (@EndDate::timestamp IS NULL OR created_at_utc >= @EndDate::timestamp)
            """;

        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return totalCount;
    }

    private sealed record SearchEventsParameters(
        int Status,
        Guid? PostId,
        DateTime? StartDate,
        DateTime? EndDate,
        int Take,
        int Skip);
}

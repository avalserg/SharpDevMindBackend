using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Posts.Application.Posts.GetPosts;

internal sealed class GetPostsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPostsQuery, IReadOnlyCollection<PostResponse>>
{
    public async Task<Result<IReadOnlyCollection<PostResponse>>> Handle(
        GetPostsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

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
                 status = 1   
             """;

        List<PostResponse> posts = (await connection.QueryAsync<PostResponse>(sql, request)).AsList();

        return posts;
    }
}

using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Domain.Posts;

namespace SharpDevMind.Modules.Posts.Application.Posts.GetPost;

internal sealed class GetPostQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPostQuery, PostResponse>
{
    public async Task<Result<PostResponse>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        const string sql =
            $"""
             SELECT
                 e.id AS {nameof(PostResponse.Id)},
                 e.category_id AS {nameof(PostResponse.CategoryId)},
                 e.user_id AS {nameof(PostResponse.AuthorId)},
                 e.title AS {nameof(PostResponse.Title)},
                 e.content AS {nameof(PostResponse.Content)},
                 e.rating AS {nameof(PostResponse.Rating)},
                 e.created_at_utc AS {nameof(PostResponse.CreatedAtUtc)},
                 e.updated_at_utc AS {nameof(PostResponse.UpdatedAtUtc)}
             FROM posts.posts e
             WHERE e.id = @PostId AND e.status = 1
             """;


        PostResponse? post = await connection.QuerySingleOrDefaultAsync<PostResponse>(sql, request);
        if (post is null)
        {
            return Result.Failure<PostResponse>(PostErrors.NotFound(request.PostId));
        }

        return post;
    }
}

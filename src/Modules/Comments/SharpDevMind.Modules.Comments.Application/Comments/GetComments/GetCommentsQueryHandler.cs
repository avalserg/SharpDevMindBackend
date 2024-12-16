using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Comments.Application.Comments.GetComments;

internal sealed class GetCommentsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCommentsQuery, IReadOnlyCollection<CommentResponse>>
{
    public async Task<Result<IReadOnlyCollection<CommentResponse>>> Handle(
        GetCommentsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

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
                 status = 1   
             """;

        List<CommentResponse> comments = (await connection.QueryAsync<CommentResponse>(sql, request)).AsList();

        return comments;
    }
}

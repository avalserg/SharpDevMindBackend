using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Domain.Comments;

namespace SharpDevMind.Modules.Comments.Application.Comments.GetComment;

internal sealed class GetCommentQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCommentQuery, CommentResponse>
{
    public async Task<Result<CommentResponse>> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        const string sql =
            $"""
             SELECT
                 e.id AS {nameof(CommentResponse.Id)},
                 e.comment_id AS {nameof(CommentResponse.PostId)},
                 e.content AS {nameof(CommentResponse.Content)},
                 e.created_at_utc AS {nameof(CommentResponse.CreatedAtUtc)},
                 e.updated_at_utc AS {nameof(CommentResponse.UpdatedAtUtc)}
             FROM comments.comments e
             WHERE e.id = @CommentId AND e.status = 1
             """;


        CommentResponse? comment = await connection.QuerySingleOrDefaultAsync<CommentResponse>(sql, request);

        if (comment is null)
        {
            return Result.Failure<CommentResponse>(CommentErrors.NotFound(request.CommentId));
        }

        return comment;
    }
}

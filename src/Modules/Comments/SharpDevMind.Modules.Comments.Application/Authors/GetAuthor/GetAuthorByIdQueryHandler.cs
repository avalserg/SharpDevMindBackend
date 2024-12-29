using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Domain.Posts;

namespace SharpDevMind.Modules.Comments.Application.Authors.GetAuthor;

internal sealed class GetAuthorByIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetAuthorQuery, AuthorResponse>
{
    public async Task<Result<AuthorResponse>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(AuthorResponse.Id)},
                 email AS {nameof(AuthorResponse.Email)},
                 first_name AS {nameof(AuthorResponse.FirstName)},
                 last_name AS {nameof(AuthorResponse.LastName)}
             FROM comments.authors
             WHERE id = @AuthorId
             """;

        AuthorResponse? authors = await connection.QuerySingleOrDefaultAsync<AuthorResponse>(sql, request);

        if (authors is null)
        {
            return Result.Failure<AuthorResponse>(PostErrors.NotFound(request.AuthorId));
        }

        return authors;
    }
}

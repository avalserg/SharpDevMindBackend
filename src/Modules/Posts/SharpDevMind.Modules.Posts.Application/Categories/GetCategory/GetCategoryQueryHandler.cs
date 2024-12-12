using System.Data.Common;
using Dapper;
using SharpDevMind.Common.Application.Data;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Domain.Categories;

namespace SharpDevMind.Modules.Posts.Application.Categories.GetCategory;

internal sealed class GetCategoryQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCategoryQuery, CategoryResponse>
{
    public async Task<Result<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CategoryResponse.Id)},
                 name AS {nameof(CategoryResponse.Name)},
                 is_archived AS {nameof(CategoryResponse.IsArchived)}
             FROM posts.categories
             WHERE id = @CategoryId
             """;

        CategoryResponse? category = await connection.QuerySingleOrDefaultAsync<CategoryResponse>(sql, request);

        if (category is null)
        {
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound(request.CategoryId));
        }

        return category;
    }
}

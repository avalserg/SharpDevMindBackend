using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Posts.Application.Categories.GetCategory;

public sealed record GetCategoryQuery(Guid CategoryId) : IQuery<CategoryResponse>;

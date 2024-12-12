using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Modules.Posts.Application.Categories.GetCategory;

namespace SharpDevMind.Modules.Posts.Application.Categories.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IReadOnlyCollection<CategoryResponse>>;

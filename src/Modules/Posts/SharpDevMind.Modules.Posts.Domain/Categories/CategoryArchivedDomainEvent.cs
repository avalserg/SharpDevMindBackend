using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Posts.Domain.Categories;

public sealed class CategoryArchivedDomainEvent(Guid categoryId) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
}

using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Posts.Domain.Authors;

public static class AuthorErrors
{
    public static Error NotFound(Guid customerId) =>
        Error.NotFound("Author.NotFound", $"The author with the identifier {customerId} was not found");
}

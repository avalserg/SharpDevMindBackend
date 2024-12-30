using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Posts.Application.Authors.CreateAuthor;

public sealed record CreateAuthorCommand(Guid AuthorId, string FirstName, string LastName)
    : ICommand;

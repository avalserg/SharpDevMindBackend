using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Authors.CreateAuthor;

public sealed record CreateAuthorCommand(Guid AuthorId, string Email, string FirstName, string LastName)
    : ICommand;

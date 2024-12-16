using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Authors.UpdateAuthor;

public sealed record UpdateAuthorCommand(Guid AuthorId, string FirstName, string LastName) : ICommand;

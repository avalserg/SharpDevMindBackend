using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Posts.Application.Authors.UpdateAuthor;

public sealed record UpdateAuthorCommand(Guid AuthorId, string FirstName, string LastName) : ICommand;

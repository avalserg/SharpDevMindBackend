using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Posts.Application.Authors.GetAuthor;

public sealed record GetAuthorQuery(Guid AuthorId) : IQuery<AuthorResponse>;

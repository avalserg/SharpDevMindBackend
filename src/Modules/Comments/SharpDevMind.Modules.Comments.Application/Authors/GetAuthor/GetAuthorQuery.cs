using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Comments.Application.Authors.GetAuthor;

public sealed record GetAuthorQuery(Guid AuthorId) : IQuery<AuthorResponse>;

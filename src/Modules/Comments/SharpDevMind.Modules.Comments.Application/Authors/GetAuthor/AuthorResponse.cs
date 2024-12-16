namespace SharpDevMind.Modules.Comments.Application.Authors.GetAuthor;

public sealed record AuthorResponse(Guid Id, string Email, string FirstName, string LastName);

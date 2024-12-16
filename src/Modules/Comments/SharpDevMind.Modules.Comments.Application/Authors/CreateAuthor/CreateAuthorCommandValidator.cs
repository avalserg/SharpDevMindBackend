using FluentValidation;

namespace SharpDevMind.Modules.Comments.Application.Authors.CreateAuthor;

internal sealed class CreatePostCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.AuthorId).NotEmpty();
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}

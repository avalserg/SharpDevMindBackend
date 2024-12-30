using FluentValidation;

namespace SharpDevMind.Modules.Posts.Application.Authors.CreateAuthor;

internal sealed class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(c => c.AuthorId).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}

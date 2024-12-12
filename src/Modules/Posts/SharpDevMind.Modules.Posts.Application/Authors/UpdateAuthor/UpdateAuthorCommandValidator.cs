using FluentValidation;

namespace SharpDevMind.Modules.Posts.Application.Authors.UpdateAuthor;

internal sealed class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(c => c.AuthorId).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}

using FluentValidation;

namespace SharpDevMind.Modules.Comments.Application.Comments.CreateComment;

internal sealed class CreatePostCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.Content).NotEmpty();
    }
}

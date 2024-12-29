using FluentValidation;

namespace SharpDevMind.Modules.Quizzes.Application.Options.CreateOption;

internal sealed class CreatePostCommandValidator : AbstractValidator<CreateOptionCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.Text).NotEmpty();
        RuleFor(c => c.QuestionId).NotEmpty();
    }
}

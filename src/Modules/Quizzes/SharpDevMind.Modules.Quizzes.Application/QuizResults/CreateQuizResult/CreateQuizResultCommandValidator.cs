using FluentValidation;

namespace SharpDevMind.Modules.Quizzes.Application.QuizResults.CreateQuizResult;

internal sealed class CreateQuizResultCommandValidator : AbstractValidator<CreateQuizResultCommand>
{
    public CreateQuizResultCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Score).NotEmpty();
    }
}

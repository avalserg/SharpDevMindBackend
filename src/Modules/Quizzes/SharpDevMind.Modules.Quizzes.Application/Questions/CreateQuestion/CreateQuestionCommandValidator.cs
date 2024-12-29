using FluentValidation;

namespace SharpDevMind.Modules.Quizzes.Application.Questions.CreateQuestion;

internal sealed class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(c => c.TextCondition).NotEmpty().WithMessage("TextCondition is required.");

        RuleFor(command => command.Options)
            .NotEmpty()
            .WithMessage("Options must not be empty.")
            .Must(options => options.TrueForAll(option => !string.IsNullOrWhiteSpace(option)))
            .WithMessage("Each option must not be empty or whitespace.");

        RuleFor(c => c.IndexOfTheRightAnswer)
            .GreaterThanOrEqualTo(0)
            .WithMessage("IndexOfTheRightAnswer must be non-negative.")
            .LessThan(command => command.Options.Count)
            .WithMessage("IndexOfTheRightAnswer must be within the range of the options list.");


    }
}

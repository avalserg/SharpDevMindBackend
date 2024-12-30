using FluentValidation;

namespace SharpDevMind.Modules.Quizzes.Application.Users.CreateUser;

internal sealed class CreatePostCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}

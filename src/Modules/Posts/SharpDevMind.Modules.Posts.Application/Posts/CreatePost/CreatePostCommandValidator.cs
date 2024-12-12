using FluentValidation;

namespace SharpDevMind.Modules.Posts.Application.Posts.CreatePost;

internal sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Content).NotEmpty();

    }
}

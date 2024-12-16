using FluentValidation;

namespace SharpDevMind.Modules.Comments.Application.Posts.CreatePost;

internal sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.PostId).NotEmpty();
    }
}

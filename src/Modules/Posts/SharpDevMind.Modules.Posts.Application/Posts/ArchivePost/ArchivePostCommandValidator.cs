using FluentValidation;

namespace SharpDevMind.Modules.Posts.Application.Posts.ArchivePost;

internal sealed class ArchivePostCommandValidator : AbstractValidator<ArchivePostCommand>
{
    public ArchivePostCommandValidator()
    {
        RuleFor(c => c.PostId).NotEmpty();
    }
}

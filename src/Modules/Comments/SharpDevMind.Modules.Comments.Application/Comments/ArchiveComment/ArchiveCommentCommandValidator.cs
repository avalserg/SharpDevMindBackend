using FluentValidation;

namespace SharpDevMind.Modules.Comments.Application.Comments.ArchiveComment;

internal sealed class ArchiveCommentCommandValidator : AbstractValidator<ArchiveCommentCommand>
{
    public ArchiveCommentCommandValidator()
    {
        RuleFor(c => c.CommentId).NotEmpty();
    }
}

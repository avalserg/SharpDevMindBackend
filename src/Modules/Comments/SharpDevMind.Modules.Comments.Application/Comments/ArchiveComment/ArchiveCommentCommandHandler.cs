using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Application.Abstractions.Data;
using SharpDevMind.Modules.Comments.Domain.Comments;

namespace SharpDevMind.Modules.Comments.Application.Comments.ArchiveComment;

internal sealed class ArchiveCommentCommandHandler(
    ICommentRepository eventRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ArchiveCommentCommand>
{
    public async Task<Result> Handle(ArchiveCommentCommand request, CancellationToken cancellationToken)
    {
        Comment? post = await eventRepository.GetAsync(request.CommentId, cancellationToken);

        if (post is null)
        {
            return Result.Failure(CommentErrors.NotFound(request.CommentId));
        }

        Result result = post.Archive();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

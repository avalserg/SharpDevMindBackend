using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Application.Abstractions.Data;
using SharpDevMind.Modules.Posts.Domain.Posts;

namespace SharpDevMind.Modules.Posts.Application.Posts.ArchivePost;

internal sealed class ArchivePostCommandHandler(
    IPostRepository eventRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ArchivePostCommand>
{
    public async Task<Result> Handle(ArchivePostCommand request, CancellationToken cancellationToken)
    {
        Post? post = await eventRepository.GetAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            return Result.Failure(PostErrors.NotFound(request.PostId));
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

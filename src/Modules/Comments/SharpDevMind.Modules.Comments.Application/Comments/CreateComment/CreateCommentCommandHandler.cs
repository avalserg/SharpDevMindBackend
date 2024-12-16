using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Application.Abstractions.Data;
using SharpDevMind.Modules.Comments.Domain.Authors;
using SharpDevMind.Modules.Comments.Domain.Comments;
using SharpDevMind.Modules.Comments.Domain.Posts;

namespace SharpDevMind.Modules.Comments.Application.Comments.CreateComment;

internal sealed class CreateCommentCommandHandler(
    ICommentRepository commentRepository,
    IAuthorRepository authorRepository,
    IPostRepository postRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCommentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        Author author = await authorRepository.GetAsync(request.UserId, cancellationToken);

        if (author == null)
        {
            return Result.Failure<Guid>(CommentErrors.OwnerNotFound(request.UserId));
        }
        Domain.Posts.Post? post = await postRepository.GetAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            return Result.Failure<Guid>(PostErrors.NotFound(request.PostId));
        }

        Result<Comment> result = Comment.Create(
            author.Id,
            post.PostId,
            request.Content);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        commentRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}

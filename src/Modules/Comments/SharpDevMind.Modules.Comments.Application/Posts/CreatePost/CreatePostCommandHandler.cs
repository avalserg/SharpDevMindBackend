using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Application.Abstractions.Data;
using SharpDevMind.Modules.Comments.Domain.Posts;

namespace SharpDevMind.Modules.Comments.Application.Posts.CreatePost;

internal sealed class CreatePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePostCommand>
{
    public async Task<Result> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = Post.Create(request.PostId, request.AuthorId);

        postRepository.Insert(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

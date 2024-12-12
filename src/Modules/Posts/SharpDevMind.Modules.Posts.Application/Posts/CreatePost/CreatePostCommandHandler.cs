using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Application.Abstractions.Data;
using SharpDevMind.Modules.Posts.Domain.Authors;
using SharpDevMind.Modules.Posts.Domain.Categories;
using SharpDevMind.Modules.Posts.Domain.Posts;

namespace SharpDevMind.Modules.Posts.Application.Posts.CreatePost;

internal sealed class CreatePostCommandHandler(
    ICategoryRepository categoryRepository,
    IPostRepository eventRepository,
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePostCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Author author = await authorRepository.GetAsync(request.UserId, cancellationToken);

        if (author == null)
        {
            return Result.Failure<Guid>(PostErrors.OwnerNotFound(request.UserId));
        }

        Category? category = await categoryRepository.GetAsync(request.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound(request.CategoryId));
        }

        Result<Post> result = Post.Create(
            author.Id,
            category,
            request.Title,
            request.Content);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        eventRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}

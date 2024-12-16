using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Application.Abstractions.Data;
using SharpDevMind.Modules.Comments.Domain.Authors;
using SharpDevMind.Modules.Comments.Domain.Posts;

namespace SharpDevMind.Modules.Comments.Application.Authors.UpdateAuthor;

internal sealed class UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateAuthorCommand>
{
    public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        Author? author = await authorRepository.GetAsync(request.AuthorId, cancellationToken);

        if (author is null)
        {
            return Result.Failure(PostErrors.NotFound(request.AuthorId));
        }

        author.Update(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

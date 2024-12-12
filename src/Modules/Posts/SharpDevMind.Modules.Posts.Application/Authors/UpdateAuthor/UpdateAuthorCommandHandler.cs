using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Application.Abstractions.Data;
using SharpDevMind.Modules.Posts.Domain.Authors;

namespace SharpDevMind.Modules.Posts.Application.Authors.UpdateAuthor;

internal sealed class UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateAuthorCommand>
{
    public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        Author? author = await authorRepository.GetAsync(request.AuthorId, cancellationToken);

        if (author is null)
        {
            return Result.Failure(AuthorErrors.NotFound(request.AuthorId));
        }

        author.Update(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Application.Abstractions.Data;
using SharpDevMind.Modules.Posts.Domain.Authors;

namespace SharpDevMind.Modules.Posts.Application.Authors.CreateAuthor;

internal sealed class CreateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAuthorCommand>
{
    public async Task<Result> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = Author.Create(request.AuthorId, request.Email, request.FirstName, request.LastName);

        authorRepository.Insert(author);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

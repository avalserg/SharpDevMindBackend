using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Application.Abstractions.Data;
using SharpDevMind.Modules.Comments.Domain.Authors;

namespace SharpDevMind.Modules.Comments.Application.Authors.CreateAuthor;

internal sealed class CreatePostCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAuthorCommand>
{
    public async Task<Result> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = Author.Create(request.AuthorId, request.FirstName, request.LastName);

        authorRepository.Insert(author);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

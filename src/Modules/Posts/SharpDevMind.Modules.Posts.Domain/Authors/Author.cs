using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Posts.Domain.Authors;
public sealed class Author : Entity
{
    private Author()
    {
    }

    public Guid Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public static Author Create(Guid id, string firstName, string lastName)
    {
        return new Author
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName
        };
    }

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

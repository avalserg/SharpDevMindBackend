using SharpDevMind.Modules.Users.Domain.Abstractions;

namespace SharpDevMind.Modules.Users.Application.Abstractions.Exceptions;

public sealed class SharpDevMindException : Exception
{
    public SharpDevMindException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}

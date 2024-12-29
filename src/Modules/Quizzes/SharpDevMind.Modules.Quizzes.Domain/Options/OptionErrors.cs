using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Quizzes.Domain.Options;
public static class OptionErrors
{
    public static Error NotFound(Guid optionId) =>
        Error.NotFound("Option.NotFound", $"The option with the identifier {optionId} was not found");
}

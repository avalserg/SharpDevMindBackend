using System.Reflection;

namespace SharpDevMind.Modules.Comments.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

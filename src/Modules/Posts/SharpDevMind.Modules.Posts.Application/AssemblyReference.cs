using System.Reflection;

namespace SharpDevMind.Modules.Posts.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

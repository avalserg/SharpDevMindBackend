using System.Reflection;
using NetArchTest.Rules;
using SharpDevMind.ArchitectureTests.Abstractions;
using SharpDevMind.Modules.Posts.Domain.Posts;
using SharpDevMind.Modules.Posts.Infrastructure;
using SharpDevMind.Modules.Users.Domain.Users;
using SharpDevMind.Modules.Users.Infrastructure;

namespace SharpDevMind.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    [Fact]
    public void UsersModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [PostsNamespace];
        string[] integrationEventsModules = [
            PostsIntegrationEventsNamespace
            ];

        List<Assembly> usersAssemblies =
        [
            typeof(User).Assembly,
            Modules.Users.Application.AssemblyReference.Assembly,
            Modules.Users.Presentation.AssemblyReference.Assembly,
            typeof(UsersModule).Assembly
        ];

        Types.InAssemblies(usersAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void PostsModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace];
        string[] integrationEventsModules = [
            UsersIntegrationEventsNamespace
            ];

        List<Assembly> eventsAssemblies =
        [
            typeof(Post).Assembly,
            Modules.Posts.Application.AssemblyReference.Assembly,
            Modules.Posts.Presentation.AssemblyReference.Assembly,
            typeof(PostsModule).Assembly
        ];

        Types.InAssemblies(eventsAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

}

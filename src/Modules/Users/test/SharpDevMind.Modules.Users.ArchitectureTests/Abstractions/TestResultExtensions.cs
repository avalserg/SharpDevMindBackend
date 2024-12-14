using FluentAssertions;
using NetArchTest.Rules;

namespace SharpDevMind.Modules.Users.ArchitectureTests.Abstractions;

internal static class TestResultExtensions
{
    internal static void ShouldBeSuccessful(this TestResult testResult)
    {
        testResult.FailingTypes?.Should().BeEmpty();
    }
}

using FluentAssertions.Equivalency;
using Godot.DependencyInjection.Injection;

namespace Godot.DependencyInjection.Core.UnitTests;

public class InjectionServiceUnitTest
{
    [Fact]
    public void InjectionService_InjectDependencies_UnknownNode_ShouldNotThrow()
    {
        var root = Substitute.For<INodeWrapper>();
        root.ReturnsNoChildren();

        var (sut, _) = InjectionServiceFactory.Create(root);
        
        var action = () => sut.InjectDependencies(root);

        action.Should().NotThrow();
    }
}
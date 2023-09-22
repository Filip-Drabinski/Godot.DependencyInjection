using System.Collections.Generic;
using System.Linq;
using FluentAssertions.Equivalency;
using Godot.DependencyInjection.Core.UnitTests.FirstAssemblyToScan;
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

    [Fact]
    public void InjectionService_InjectDependencies_ServicesShouldBeInjected()
    {
        var elem = new FirstAssembly2();
        var root = new NodeWrapper(new object(),
            new NodeWrapper(new ServiceRegistration())
            ,new NodeWrapper(elem)
        );

        var (sut, nodes) = InjectionServiceFactory.Create(root);
        var unpackedNodes = nodes.Select(x => x.RawNode).ToArray();
        foreach (var node in unpackedNodes)
        {
            sut.AddProviders(node);
        }
        foreach (var node in unpackedNodes)
        {
            sut.InjectDependencies(node);
        }

        elem.Guid.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void InjectionService_InjectDependencies_ProvidedShouldBeInjected()
    {
        var elem = new Provided();
        var provider = new Provider();
        var root = new NodeWrapper(new object(),
            new NodeWrapper(new ServiceRegistration()), 
            new NodeWrapper(provider),
            new NodeWrapper(elem)
        );

        var (sut, nodes) = InjectionServiceFactory.Create(root);
        var unpackedNodes = nodes.Select(x => x.RawNode).ToArray();
        foreach (var node in unpackedNodes)
        {
            sut.AddProviders(node);
        }
        foreach (var node in unpackedNodes)
        {
            sut.InjectDependencies(node);
        }

        elem.Value.Should().Be(provider);
    }

    private class NodeWrapper:INodeWrapper
    {
        public object RawNode { get; }
        public IEnumerable<NodeWrapper> Children { get; }

        public NodeWrapper(object rawNode,params NodeWrapper[] children)
        {
            RawNode = rawNode;
            Children = children;
        }
        public IEnumerable<INodeWrapper> GetChildren()
        {
            return Children;
        }
    }
}
using Godot.DependencyInjection.Injection;

namespace Godot.DependencyInjection.Core.UnitTests
{
    public class InjectionServiceFactoryUnitTest
    {
        [Fact]
        public void InjectionServiceFactory_injectionService_ShouldNotNull()
        {
            var rootSubstitute = Substitute.For<INodeWrapper>();
            rootSubstitute.GetChildren().Returns(Array.Empty<INodeWrapper>());

            var (injectionService, _) = InjectionServiceFactory.Create(rootSubstitute);
            
            injectionService.Should().NotBeNull();
        }
        [Fact]
        public void InjectionServiceFactory_nodesToInject_ShouldNotNull()
        {
            var rootSubstitute = Substitute.For<INodeWrapper>();
            rootSubstitute.GetChildren().Returns(Array.Empty<INodeWrapper>());

            var (_, nodesToInject) = InjectionServiceFactory.Create(rootSubstitute);
            
            nodesToInject.Should().NotBeNull();
        }
        [Fact]
        public void InjectionServiceFactory_nodesToInject_ShouldReturnAllNodesInTree()
        {
            
            var nodeA1B1 = Substitute.For<INodeWrapper>();
            var nodeA1B2 = Substitute.For<INodeWrapper>();
            var nodeA1 = Substitute.For<INodeWrapper>();
            nodeA1B1.ReturnsNoChildren();
            nodeA1B2.ReturnsNoChildren();
            nodeA1.ReturnsChildren(nodeA1B1,nodeA1B2);

            var nodeA2B1 = Substitute.For<INodeWrapper>();
            var nodeA2 = Substitute.For<INodeWrapper>();
            nodeA2B1.ReturnsNoChildren();
            nodeA2.ReturnsChildren(nodeA2B1);

            var root = Substitute.For<INodeWrapper>();
            root.ReturnsChildren(nodeA1,nodeA2);

            var (_, nodesToInject) = InjectionServiceFactory.Create(root);
            nodesToInject.Should().HaveCount(6);
        }
    }
}
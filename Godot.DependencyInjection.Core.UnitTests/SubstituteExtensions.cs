using Godot.DependencyInjection.Injection;

namespace Godot.DependencyInjection.Core.UnitTests;

internal static class SubstituteExtensions
{
    public static void ReturnsNoChildren(this INodeWrapper node)
    {
        node.GetChildren().Returns(Array.Empty<INodeWrapper>());
    }
    public static void ReturnsChildren(this INodeWrapper node,params INodeWrapper[] children)
    {
        node.GetChildren().Returns(children);
    }
}
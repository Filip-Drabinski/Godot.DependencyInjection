using Godot.DependencyInjection.Injection;

namespace Godot.DependencyInjection;

internal readonly struct NodeWrapper : INodeWrapper
{
    public readonly Node node;

    public NodeWrapper(Node node)
    {
        this.node = node;
    }

    public IEnumerable<INodeWrapper> GetChildren()
    {
        return node.GetChildren(true).Select<Node, INodeWrapper>(x => new NodeWrapper(x));
    }
}
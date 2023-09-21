using Godot.DependencyInjection.Injection;

namespace Godot.DependencyInjection;

internal readonly struct NodeWrapper : INodeWrapper
{
    public object RawNode => Node;

    public readonly Node Node;

    public NodeWrapper(Node node)
    {
        this.Node = node;
    }

    public IEnumerable<INodeWrapper> GetChildren()
    {
        return Node.GetChildren(true).Select<Node, INodeWrapper>(x => new NodeWrapper(x));
    }
}
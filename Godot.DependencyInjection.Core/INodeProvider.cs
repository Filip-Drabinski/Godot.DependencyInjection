namespace Godot.DependencyInjection;


public interface INodeProvider<TNode>
{
    public void Provide(out TNode node);
}
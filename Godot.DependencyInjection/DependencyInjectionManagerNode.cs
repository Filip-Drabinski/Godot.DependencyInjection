using Godot.DependencyInjection.Injection;

namespace Godot.DependencyInjection;

public abstract partial class DependencyInjectionManagerNode : Node
{
    private InjectionService _injectionService = null!;

    /// <summary>
    /// Called when the node is ready.
    /// </summary>
    public override void _EnterTree()
    {
        var tree = GetTree();
        (_injectionService, var nodesToInject) = InjectionServiceFactory.Create(new NodeWrapper(tree.Root));

        var unpackedNodes = nodesToInject.Select(x => ((NodeWrapper)x).Node).ToArray();
        foreach (var node in unpackedNodes)
        {
            _injectionService.AddProviders(node);
        }
        foreach (var node in unpackedNodes)
        {
            _injectionService.InjectDependencies(node);
        }

        tree.NodeAdded += _injectionService.AddProvidersAndInjectDependencies;
        
        tree.NodeRemoved += _injectionService.RemoveProviders;
    }
}
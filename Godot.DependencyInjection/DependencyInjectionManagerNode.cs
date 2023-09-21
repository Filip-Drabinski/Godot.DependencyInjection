using Godot.DependencyInjection.Injection;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Godot.DependencyInjection;

public abstract partial class DependencyInjectionManagerNode : Node
{
    private InjectionService _injectionService = null!;

    /// <summary>
    /// Called when the node is ready.
    /// </summary>
    public override void _Ready()
    {
        var tree = GetTree();

        (_injectionService, var nodesToInject) = InjectionServiceFactory.Create(new NodeWrapper(tree.Root));

        var unpackedNodes = nodesToInject.Select(x => ((NodeWrapper)x).node);
        foreach (var node in unpackedNodes)
        {
            _injectionService.InjectDependencies(node);
        }

        tree.NodeAdded += _injectionService.InjectDependencies;
    }
}
using Godot.DependencyInjection.Injection;
using Microsoft.Extensions.DependencyInjection;

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
        var nodeLists = ProcessInitialNodes(tree);
        ServiceProvider serviceProvider = BuildServiceProvider(nodeLists.configurators);
        _injectionService = new InjectionService(serviceProvider);

        foreach (var node in nodeLists.nodesToInject)
        {
            _injectionService.InjectDependencies(node);
        }

        tree.NodeAdded += _injectionService.InjectDependencies;
    }

    /// <summary>
    /// Registers services from nodes implementing <see cref="IServicesConfigurator"/>
    /// </summary>
    /// <param name="configurators"></param>
    /// <returns></returns>
    private static ServiceProvider BuildServiceProvider(List<IServicesConfigurator> configurators)
    {
        ServiceCollection services = new();
        foreach (var item in configurators)
        {
            item.ConfigureServices(services);
        }
        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }

    /// <summary>
    /// Processes the initial nodes in the scene tree.
    /// </summary>
    /// <param name="tree">The scene tree to process.</param>
    private (List<Node> nodesToInject, List<IServicesConfigurator> configurators) ProcessInitialNodes(SceneTree tree)
    {
        var nodesToInject = new List<Node>();
        var configurators = new List<IServicesConfigurator>();
        var queue = new Queue<Node>();
        queue.Enqueue(tree.Root);

        while (queue.TryDequeue(out var element))
        {
            nodesToInject.Add(element);
            if (element is IServicesConfigurator servicesConfigurator)
            {
                configurators.Add(servicesConfigurator);
            }

            var children = element.GetChildren(true);
            foreach (var child in children)
            {
                queue.Enqueue(child);
            }
        }
        return (nodesToInject: nodesToInject, configurators: configurators);
    }
}
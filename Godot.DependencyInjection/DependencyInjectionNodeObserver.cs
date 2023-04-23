using Godot.DependencyInjection.Attributes;
using Godot.DependencyInjection.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Godot.DependencyInjection;

/// <summary>
/// Represents an observer that handles dependency injection for Godot nodes.
/// </summary>
[IgnoreDependencyInjection]
public abstract partial class DependencyInjectionNodeObserver : Node
{
    private static bool _isInstantiated;
    private readonly InjectionService _injectionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DependencyInjectionNodeObserver"/> class.
    /// </summary>
    public DependencyInjectionNodeObserver()
    {
        DependencyInjectionObserverDuplicationException.ThrowIf(_isInstantiated);

        ServiceCollection services = new();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();

        _injectionService = new InjectionService(serviceProvider);

        _isInstantiated = true;
    }

    /// <summary>
    /// Configures the services to be used in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    public abstract void ConfigureServices(IServiceCollection services);

    /// <summary>
    /// Called when the node is ready.
    /// </summary>
    public override void _Ready()
    {
        var tree = GetTree();
        ProcessInitialNodes(tree);
        tree.NodeAdded += _injectionService.InjectDependencies;
    }

    /// <summary>
    /// Processes the initial nodes in the scene tree.
    /// </summary>
    /// <param name="tree">The scene tree to process.</param>
    private void ProcessInitialNodes(SceneTree tree)
    {
        var queue = new Queue<Node>();
        queue.Enqueue(tree.Root);

        while (queue.TryDequeue(out var element))
        {
            _injectionService.InjectDependencies(element);

            var children = element.GetChildren(true);
            foreach (var child in children)
            {
                queue.Enqueue(child);
            }
        }
    }
}

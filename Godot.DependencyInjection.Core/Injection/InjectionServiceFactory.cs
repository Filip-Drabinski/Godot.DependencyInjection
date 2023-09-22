using Godot.DependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Godot.DependencyInjection.Injection;

public interface INodeWrapper
{
    object RawNode { get; }
    IEnumerable<INodeWrapper> GetChildren();
}

public static class InjectionServiceFactory
{
    public static (InjectionService injectionService, List<INodeWrapper> nodesToInject) Create(INodeWrapper sceneRootNode)
    {
        var (nodesToInject, configurators) = ProcessInitialNodes(sceneRootNode);
        ServiceProvider serviceProvider = BuildServiceProvider(configurators);
        var injectionService = new InjectionService(serviceProvider);
        return (injectionService: injectionService, nodesToInject: nodesToInject);
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

        services.AddSingleton(typeof(IProviderManager<,>), typeof(ProviderManager<,>));

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
    /// <summary>
    /// Processes the initial nodes in the scene tree.
    /// </summary>
    /// <param name="sceneRootNode">root of The scene tree to process.</param>
    private static (List<INodeWrapper> nodesToInject, List<IServicesConfigurator> configurators) ProcessInitialNodes(INodeWrapper sceneRootNode)
    {
        var nodesToInject = new List<INodeWrapper>();
        var configurators = new List<IServicesConfigurator>();
        var queue = new Queue<INodeWrapper>();
        queue.Enqueue(sceneRootNode);

        while (queue.TryDequeue(out var element))
        {
            nodesToInject.Add(element);
            if (element.RawNode is IServicesConfigurator servicesConfigurator)
            {
                configurators.Add(servicesConfigurator);
            }

            var children = element.GetChildren();
            foreach (var child in children)
            {
                queue.Enqueue(child);
            }
        }
        return (nodesToInject: nodesToInject, configurators: configurators);
    }
}
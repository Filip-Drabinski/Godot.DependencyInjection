using Godot.DependencyInjection.Attributes;
using Godot.DependencyInjection.Scanning;
using Godot.DependencyInjection.Scanning.Models;
using System.Reflection;

namespace Godot.DependencyInjection.Injection;

/// <summary>
/// Represents a service that handles dependency injection for nodes.
/// </summary>
internal class InjectionService
{
    public static readonly Dictionary<Type, InjectionMetadata> Metadata;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes static members of the <see cref="InjectionService"/> class.
    /// </summary>
    static InjectionService()
    {
        Metadata = InjectionScanner.CollectMetadata();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="InjectionService"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider for dependency injection.</param>
    public InjectionService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Injects dependencies into the specified node.
    /// </summary>
    /// <param name="node">The node to inject dependencies into.</param>
    public void InjectDependencies(Node node)
    {
        if (node.GetType().GetCustomAttribute<IgnoreDependencyInjectionAttribute>() is not null)
        {
            return;
        }

        var type = node.GetType();
        var memberQueue = new Queue<(Type type, object? instance)>();
        memberQueue.Enqueue((type, instance: node));

        while (memberQueue.TryDequeue(out var element))
        {
            if (element.instance is null
                || !Metadata.TryGetValue(element.type, out var metadata))
            {
                continue;
            }
            metadata.Inject(_serviceProvider, element.instance);
            for (int i = 0; i < metadata.nestedInjections.Length; i++)
            {
                var nestedType = metadata.nestedInjections[i].nestedType;
                var nestedinstance = metadata.nestedInjections[i].memberGetter(element.instance);
                memberQueue.Enqueue((
                    type: nestedType,
                    instance: nestedinstance
                ));
            }
        }
    }
    /// <summary>
    /// Prints <see cref="Metadata"/> serialized to Json using <see cref="GD.Print"/>
    /// </summary>
    private static void DebugPrintMetadata()
    {
        var SerializedItems = Metadata.Select(x => $@"
{{
    ""type"": ""{x.Key.Name}"",
    ""metadata"": {x.Value}
}}");
        var json = $@"[{string.Join(',', SerializedItems)}]";
        GD.Print(json);
    }

}

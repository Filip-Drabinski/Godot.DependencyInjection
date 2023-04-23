using Godot.DependencyInjection.Attributes;
using Godot.DependencyInjection.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Godot.DependencyInjection;


[IgnoreDependencyInjection]
public abstract partial class DependencyInjectionNodeObserver : Node
{
    private static bool _isInstantiated;
    private readonly InjectionService _injectionService;
    public DependencyInjectionNodeObserver()
    {
        DependencyInjectionObserverDuplicationException.ThrowIf(_isInstantiated);

        ServiceCollection services = new();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();

        _injectionService = new InjectionService(serviceProvider);

        _isInstantiated = true;
    }

    public abstract void ConfigureServices(IServiceCollection services);

    public override void _Ready()
    {
        var tree = GetTree();
        ProcessInitialNodes(tree);
        tree.NodeAdded += _injectionService.InjectDependencies;
    }

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
internal class InjectionService
{
    private readonly IServiceProvider _serviceProvider;

    public InjectionService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

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
            if (element.instance is null)
            {
                continue;
            }
            var properties = element.type.GetProperties();
            var fields = element.type.GetFields();

            foreach (var item in properties)
            {
                if (item.GetCustomAttribute<InjectMembersAttribute>() is null)
                {
                    continue;
                }
                var nextItem = (type: item.PropertyType, instance: item.GetValue(element.instance));
                memberQueue.Enqueue(nextItem);
            }

            foreach (var item in fields)
            {
                if (item.GetCustomAttribute<InjectMembersAttribute>() is null)
                {
                    continue;
                }
                var nextItem = (type: item.FieldType, instance: item.GetValue(element.instance));
                memberQueue.Enqueue(nextItem);
            }

            InjectProperties(element.instance, properties);
            InjectFields(element.instance, fields);
            InjectMethods(element.instance, element.type);
        }
    }

    private void InjectProperties(object instance, PropertyInfo[] properties)
    {
        foreach (var item in properties)
        {
            var attribute = item.GetCustomAttribute<InjectAttribute>();
            if (attribute is null)
            {
                continue;
            }
            var service = GetService(attribute.IsRequired, item.PropertyType);
            item.SetValue(instance, service);
        }
    }
    private void InjectFields(object instance, FieldInfo[] fields)
    {
        foreach (var item in fields)
        {
            var attribute = item.GetCustomAttribute<InjectAttribute>();
            if (attribute is null)
            {
                continue;
            }

            var service = GetService(attribute.IsRequired, item.FieldType);
            item.SetValue(instance, service);
        }
    }
    private void InjectMethods(object instance, Type type)
    {
        var methods = type.GetMethods();
        foreach (var item in methods)
        {
            if (item.GetCustomAttribute<InjectAttribute>() is null)
            {
                continue;
            }
            var parameters = item.GetParameters().Select(x => GetServiceForMethod(x)).ToArray();
            item.Invoke(instance, parameters);

        }
    }

    private object? GetServiceForMethod(ParameterInfo parameterInfo)
    {
        var isRequired = parameterInfo.GetCustomAttribute<RequiredAttribute>() is not null;
        return GetService(isRequired, parameterInfo.ParameterType);
    }

    private object? GetService(bool required, Type serviceType)
    {
        var service = required
            ? _serviceProvider.GetRequiredService(serviceType)
            : _serviceProvider.GetService(serviceType);
        return service;
    }
}
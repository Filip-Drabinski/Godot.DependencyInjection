using Godot.DependencyInjection.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Godot.DependencyInjection;

/// <summary>
/// Represents a service that handles dependency injection for nodes.
/// </summary>
internal class InjectionService
{
    public static readonly Dictionary<Type, InjectableScanner.InjectionMetadata> Metadata;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes static members of the <see cref="InjectionService"/> class.
    /// </summary>
    static InjectionService()
    {
        Metadata = InjectableScanner.CollectMetadata();
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
            Metadata[element.type].Inject(_serviceProvider,element.instance);
        }
    }

    /// <summary>
    /// Injects dependencies into the properties of the specified instance.
    /// </summary>
    /// <param name="instance">The instance to inject dependencies into.</param>
    /// <param name="properties">The properties to inject dependencies into.</param>
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

    /// <summary>
    /// Injects dependencies into the fields of the specified instance.
    /// </summary>
    /// <param name="instance">The instance to inject dependencies into.</param>
    /// <param name="fields">The fields to inject dependencies into.</param>
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

    /// <summary>
    /// Injects dependencies into the methods of the specified instance.
    /// </summary>
    /// <param name="instance">The instance to inject dependencies into.</param>
    /// <param name="type">The type of the instance.</param>
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

    /// <summary>
    /// Gets the service for the specified method parameter.
    /// </summary>
    /// <param name="parameterInfo">The parameter information.</param>
    /// <returns>The service instance for the parameter.</returns>
    private object? GetServiceForMethod(ParameterInfo parameterInfo)
    {
        var isRequired = parameterInfo.GetCustomAttribute<RequiredAttribute>() is not null;
        return GetService(isRequired, parameterInfo.ParameterType);
    }

    /// <summary>
    /// Gets the service for the specified type.
    /// </summary>
    /// <param name="required">Whether the service is required.</param>
    /// <param name="serviceType">The type of the service.</param>
    /// <returns>The service instance for the specified type.</returns>
    private object? GetService(bool required, Type serviceType)
    {
        var service = required
            ? _serviceProvider.GetRequiredService(serviceType)
            : _serviceProvider.GetService(serviceType);
        return service;
    }
}
